using System;
using System.Collections.Generic;
using System.Linq;
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

namespace VNmanager
{
    /// <summary>
    /// Interaction logic for GameProfile2.xaml
    /// </summary>
    public partial class GameProfile2 : UserControl
    {
        public GameProfile2()
        {
            InitializeComponent();
            this.DataContext = App.Mvvm;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Nvm.ChangeView.Execute("TuneProfile");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //if(ImageOpt.Visibility == Visibility.Hidden)
            //{
            //    ImageOpt.Visibility = Visibility.Visible;
            //    ImageOpt.IsDropDownOpen = true;
            //    ImageOpt.IsEnabled = true;
            //}
            //else if(ImageOpt.Visibility == Visibility.Visible)
            //{
            //    ImageOpt.Visibility = Visibility.Hidden;
            //    ImageOpt.IsDropDownOpen = false;
            //    ImageOpt.IsEnabled = false;
            //}
        }
    }
}
