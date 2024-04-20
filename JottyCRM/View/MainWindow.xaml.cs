using System.Windows;
using JottyCRM.repositories;
using JottyCRM.services;
using Mehdime.Entity;

namespace JottyCRM.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*public MainWindow()
        {
            InitializeComponent();
        }*/
        /*
        private MetroWindow _mainWindowService;
        */
        public MainWindow()
        {
            InitializeComponent();
            
            var dbContextScopeFactory = new DbContextScopeFactory();
            var ambientDbContextLocator = new AmbientDbContextLocator();
            var userRepository = new UserRepository(ambientDbContextLocator);

            var userService = new UserService(dbContextScopeFactory, userRepository);
            
            /*
            MainFrame.Content = new MainFramePage(MainFrame, userService);
        */
        }
    }
}
