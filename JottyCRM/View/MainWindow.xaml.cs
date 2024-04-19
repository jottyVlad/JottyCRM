using JottyCRM.repositories;
using JottyCRM.services;
using MahApps.Metro.Controls;
using Mehdime.Entity;

namespace JottyCRM.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
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
