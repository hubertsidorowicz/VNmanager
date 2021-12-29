using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace VNmanager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Instances
        
        private static MainViewModel _mvvm;
        public static MainViewModel Mvvm 
        {
            get
            {
                return _mvvm;
            }
        }

        private static MainWindow _mainWindow;
        public static MainWindow MainWindowI
        {
            get
            {
                return _mainWindow;
            }
        }

        private static AddWindow _addWindow;
        public static AddWindow AddWindow
        {
            get
            {
                return _addWindow;
            }
        }

        private static NavigationViewModel _nvm;
        public static NavigationViewModel Nvm
        {
            get
            {
                return _nvm;
            }
        }
        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _mainWindow = new MainWindow();
            _mvvm = new MainViewModel();
            _nvm = new NavigationViewModel();
            _addWindow = new AddWindow();
            _mainWindow.DataContext = _mvvm;
            _mainWindow.Show();
        }
    }
}
