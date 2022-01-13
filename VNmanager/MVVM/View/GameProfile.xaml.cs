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
    /// Interaction logic for GameProfile.xaml
    /// </summary>
    public partial class GameProfile : UserControl
    {
        public GameProfile()
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

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;

            var bc = new BrushConverter();
            grid.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#2a283b");
            grid.Background.Opacity = 0.5;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;

            var bc = new BrushConverter();
            grid.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#272537");
            grid.Background.Opacity = 0.5;
        }

        private void Stack_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel stackpanel = sender as StackPanel;

            var bc = new BrushConverter();
            stackpanel.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#2a283b");
            stackpanel.Background.Opacity = 0.5;
        }

        private void Stack_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel stackpanel = sender as StackPanel;

            var bc = new BrushConverter();
            stackpanel.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#272537");
            stackpanel.Background.Opacity = 0.5;
        }
    }
}
