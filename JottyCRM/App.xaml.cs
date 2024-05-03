using JottyCRM.View;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using JottyCRM.Core;
using JottyCRM.repositories;
using JottyCRM.Services;
using JottyCRM.Stores;
using JottyCRM.ViewModel;
using Mehdime.Entity;
using MainWindow = JottyCRM.View.MainWindow;

namespace JottyCRM
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider; 
        
        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<NavigationStore>();
            services.AddSingleton<UserStore>();
            services.AddSingleton<WindowNavigationStore>();
            
            services.AddSingleton<AmbientDbContextLocator>();
            services.AddSingleton<DbContextScopeFactory>();
            
            services.AddSingleton<IUserRepository>(s => new UserRepository(s.GetRequiredService<AmbientDbContextLocator>()));
            services.AddSingleton<IUserService>(s => new UserService(
                s.GetRequiredService<DbContextScopeFactory>(),
                s.GetRequiredService<IUserRepository>()
            ));
            
            services.AddSingleton<CloseWindowNavigationService>();

            services.AddTransient<WelcomeViewModel>(s => new WelcomeViewModel(
                CreateLoginNavigationService(s),
                CreateRegistrationNavigationService(s)));
            
            services.AddSingleton<INavigationService>(CreateWelcomeNavigationService);
            
            services.AddTransient<LoginViewModel>(CreateLoginViewModel);

            services.AddTransient<RegistrationViewModel>(CreateRegistrationViewModel);

            services.AddTransient<HomeViewModel>(s => new HomeViewModel(
                s.GetRequiredService<UserStore>()));

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        private INavigationService CreateWelcomeNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<WelcomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<WelcomeViewModel>
            );
        }
        
        private INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
        {
            return new WindowNavigationService<LoginViewModel>(
                serviceProvider.GetRequiredService<WindowNavigationStore>(),
                serviceProvider.GetRequiredService<LoginViewModel>);
        }

        private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
        {
            CompositeNavigationService loginNavigationService = new CompositeNavigationService(
                serviceProvider.GetRequiredService<CloseWindowNavigationService>(),
                CreateHomeNavigationService(serviceProvider));

            CloseWindowNavigationService closeNavigationService = new CloseWindowNavigationService(
                serviceProvider.GetRequiredService<WindowNavigationStore>()
            );

            return new LoginViewModel(
                serviceProvider.GetRequiredService<UserStore>(),
                serviceProvider.GetRequiredService<IUserService>(),
                loginNavigationService,
                closeNavigationService
                );
        }

        private INavigationService CreateRegistrationNavigationService(IServiceProvider serviceProvider)
        {
            return new WindowNavigationService<RegistrationViewModel>(
                serviceProvider.GetRequiredService<WindowNavigationStore>(),
                serviceProvider.GetRequiredService<RegistrationViewModel>);
        }

        private RegistrationViewModel CreateRegistrationViewModel(IServiceProvider serviceProvider)
        {
            CompositeNavigationService registrationNavigationService = new CompositeNavigationService(
                serviceProvider.GetRequiredService<CloseWindowNavigationService>(),
                CreateHomeNavigationService(serviceProvider));

            CloseWindowNavigationService closeNavigationService = new CloseWindowNavigationService(
                serviceProvider.GetRequiredService<WindowNavigationStore>()
            );

            return new RegistrationViewModel(
                serviceProvider.GetRequiredService<UserStore>(),
                serviceProvider.GetRequiredService<IUserService>(),
                registrationNavigationService,
                closeNavigationService
            );
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<HomeViewModel>
            );
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow?.Show();
            
            base.OnStartup(e);
        }
    }
}
