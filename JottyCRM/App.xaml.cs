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
using JottyCRM.DatabaseContext;
using JottyCRM.DatabaseContext.ContractorContext;
using JottyCRM.Models;
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
            services.AddSingleton<ContractorsStore>();
            services.AddSingleton<LeadsStore>();
            services.AddSingleton<SellsStore>();
            services.AddSingleton<WindowNavigationStore>();
            
            services.AddSingleton<AmbientDbContextLocator>();
            services.AddSingleton<DbContextScopeFactory>();
            
            services.AddSingleton<IUserRepository>(s => new UserRepository(s.GetRequiredService<AmbientDbContextLocator>()));
            services.AddSingleton<IUserService>(s => new UserService(
                s.GetRequiredService<DbContextScopeFactory>(),
                s.GetRequiredService<IUserRepository>()
            ));
            
            services.AddSingleton<IContractorRepository>(s => new ContractorRepository(s.GetRequiredService<AmbientDbContextLocator>()));
            services.AddSingleton<IContractorService>(s => new ContractorService(
                s.GetRequiredService<DbContextScopeFactory>(),
                s.GetRequiredService<IContractorRepository>()
            ));
            
            services.AddSingleton<ILeadRepository>(s => new LeadRepository(s.GetRequiredService<AmbientDbContextLocator>()));
            services.AddSingleton<ILeadService>(s => new LeadService(
                s.GetRequiredService<DbContextScopeFactory>(),
                s.GetRequiredService<ILeadRepository>()
            ));

            services.AddSingleton<ISellRepository>(s =>
                new SellRepository(s.GetRequiredService<AmbientDbContextLocator>())); 
            services.AddSingleton<ISellService>(s => new SellService(
                s.GetRequiredService<DbContextScopeFactory>(),
                s.GetRequiredService<ISellRepository>()));
            
            services.AddSingleton<CloseWindowNavigationService>();

            services.AddTransient<WelcomeViewModel>(s => new WelcomeViewModel(
                CreateLoginNavigationService(s),
                CreateRegistrationNavigationService(s)));
            
            services.AddSingleton<INavigationService>(CreateWelcomeNavigationService);
            
            services.AddTransient<LoginViewModel>(CreateLoginViewModel);

            services.AddTransient<RegistrationViewModel>(CreateRegistrationViewModel);

            services.AddTransient<CreateContractorViewModel>(CreateCreateContractorViewModel);
            services.AddTransient<CreateLeadViewModel>(CreateCreateLeadViewModel);
            services.AddTransient<CreateSellViewModel>(CreateCreateSellViewModel);


            services.AddTransient<HomeViewModel>(s => new HomeViewModel(
                s.GetRequiredService<UserStore>()));

            services.AddTransient<ContractorsViewModel>(s => new ContractorsViewModel(
                s.GetRequiredService<IContractorService>(),
                s.GetRequiredService<UserStore>(),
                s.GetRequiredService<ContractorsStore>(),
                CreateCreateContractorNavigationService(s)));
            services.AddTransient<LeadsViewModel>(s => new LeadsViewModel(
                s.GetRequiredService<ILeadService>(),
                s.GetRequiredService<UserStore>(),
                s.GetRequiredService<LeadsStore>(),
                CreateCreateLeadNavigationService(s)));
            services.AddTransient<SellsViewModel>(s => new SellsViewModel(
                s.GetRequiredService<ISellService>(),
                s.GetRequiredService<UserStore>(),
                s.GetRequiredService<SellsStore>(),
                CreateCreateSellNavigationService(s)));
            
            services.AddTransient<NavbarViewModel>(CreateNavbarViewModel);

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

        private INavigationService CreateCreateContractorNavigationService(IServiceProvider serviceProvider)
        {
            return new WindowNavigationService<CreateContractorViewModel>(
                serviceProvider.GetRequiredService<WindowNavigationStore>(),
                serviceProvider.GetRequiredService<CreateContractorViewModel>);
        }
        
        private INavigationService CreateCreateLeadNavigationService(IServiceProvider serviceProvider)
        {
            return new WindowNavigationService<CreateLeadViewModel>(
                serviceProvider.GetRequiredService<WindowNavigationStore>(),
                serviceProvider.GetRequiredService<CreateLeadViewModel>);
        }
        
        private INavigationService CreateCreateSellNavigationService(IServiceProvider serviceProvider)
        {
            return new WindowNavigationService<CreateSellViewModel>(
                serviceProvider.GetRequiredService<WindowNavigationStore>(),
                serviceProvider.GetRequiredService<CreateSellViewModel>);
        }

        private CreateContractorViewModel CreateCreateContractorViewModel(IServiceProvider serviceProvider)
        {
            CompositeNavigationService createContractorNavigationService = new CompositeNavigationService(
                serviceProvider.GetRequiredService<CloseWindowNavigationService>(),
                CreateContractorsNavigationService(serviceProvider));

            CloseWindowNavigationService closeNavigationService = new CloseWindowNavigationService(
                serviceProvider.GetRequiredService<WindowNavigationStore>());

            return new CreateContractorViewModel(serviceProvider.GetRequiredService<UserStore>(),
                serviceProvider.GetRequiredService<IContractorService>(),
                createContractorNavigationService,
                closeNavigationService);
        }
        
        private CreateLeadViewModel CreateCreateLeadViewModel(IServiceProvider serviceProvider)
        {
            CompositeNavigationService createLeadNavigationService = new CompositeNavigationService(
                serviceProvider.GetRequiredService<CloseWindowNavigationService>(),
                CreateLeadsNavigationService(serviceProvider));

            CloseWindowNavigationService closeNavigationService = new CloseWindowNavigationService(
                serviceProvider.GetRequiredService<WindowNavigationStore>());

            return new CreateLeadViewModel(serviceProvider.GetRequiredService<UserStore>(),
                serviceProvider.GetRequiredService<ILeadService>(),
                createLeadNavigationService,
                closeNavigationService);
        }
        
        private CreateSellViewModel CreateCreateSellViewModel(IServiceProvider serviceProvider)
        {
            CompositeNavigationService createSellNavigationService = new CompositeNavigationService(
                serviceProvider.GetRequiredService<CloseWindowNavigationService>(),
                CreateSellsNavigationService(serviceProvider));

            CloseWindowNavigationService closeNavigationService = new CloseWindowNavigationService(
                serviceProvider.GetRequiredService<WindowNavigationStore>());

            return new CreateSellViewModel(serviceProvider.GetRequiredService<UserStore>(),
                serviceProvider.GetRequiredService<ISellService>(),
                createSellNavigationService,
                closeNavigationService);
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
            return new WithNavbarNavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<HomeViewModel>,
                serviceProvider.GetRequiredService<NavbarViewModel>
            );
        }

        private INavigationService CreateContractorsNavigationService(IServiceProvider serviceProvider)
        {
            return new WithNavbarNavigationService<ContractorsViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<ContractorsViewModel>(),
                () => serviceProvider.GetRequiredService<NavbarViewModel>()
                );
        }
        
        private INavigationService CreateLeadsNavigationService(IServiceProvider serviceProvider)
        {
            return new WithNavbarNavigationService<LeadsViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<LeadsViewModel>(),
                () => serviceProvider.GetRequiredService<NavbarViewModel>()
            );
        }
        
        private INavigationService CreateSellsNavigationService(IServiceProvider serviceProvider)
        {
            return new WithNavbarNavigationService<SellsViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<SellsViewModel>(),
                () => serviceProvider.GetRequiredService<NavbarViewModel>()
            );
        }

        private NavbarViewModel CreateNavbarViewModel(IServiceProvider serviceProvider)
        {
            return new NavbarViewModel(null,
                CreateContractorsNavigationService(serviceProvider),
                CreateLeadsNavigationService(serviceProvider),
                CreateSellsNavigationService(serviceProvider)
            );
        }

        private void CreateDatabaseTables()
        {
            using (var ctx = new ApplicationDatabaseContext())
            {
                ctx.Database.CreateIfNotExists();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            CreateDatabaseTables();
            
            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow?.Show();
            
            base.OnStartup(e);
        }
    }
}
