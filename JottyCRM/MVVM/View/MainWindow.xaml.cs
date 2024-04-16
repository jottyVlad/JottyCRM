using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JottyCRM.Models;
using JottyCRM.repositories;
using JottyCRM.services;
using MahApps.Metro.Controls;
using Mehdime.Entity;

namespace JottyCRM.MVVM.View
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
