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
using JottyCRM.services;
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
            
            services.AddSingleton<AmbientDbContextLocator>();
            services.AddSingleton<DbContextScopeFactory>();
            
            services.AddSingleton<IUserRepository>(s => new UserRepository(s.GetRequiredService<AmbientDbContextLocator>()));
            services.AddSingleton<IUserService>(s => new UserService(
                s.GetRequiredService<DbContextScopeFactory>(),
                s.GetRequiredService<IUserRepository>()
            ));

            services.AddTransient<WelcomeViewModel>(s => new WelcomeViewModel(
                CreateLoginNavigationService(s),
                CreateRegistrationNavigationService(s)));
            
            services.AddSingleton<INavigationService>(CreateWelcomeNavigationService);
            
            services.AddTransient<LoginViewModel>(s => new LoginViewModel(
                s.GetRequiredService<UserStore>(),
                s.GetRequiredService<IUserService>(),
                CreateHomeNavigationService(s)));

            services.AddTransient<RegistrationViewModel>(s => new RegistrationViewModel(
                s.GetRequiredService<UserStore>(),
                s.GetRequiredService<IUserService>(),
                CreateHomeNavigationService(s)));

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
            return new NavigationService<LoginViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<LoginViewModel>);
        }

        private INavigationService CreateRegistrationNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<RegistrationViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<RegistrationViewModel>);
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
