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
    /// Interaction logic for SidePanel.xaml
    /// </summary>
    public partial class SidePanel : UserControl
    {
        public BrushConverter bc { get; set; }

        public SidePanel()
        {
            InitializeComponent();
            this.DataContext = App.Mvvm;
            bc = new BrushConverter();
        }

        public void SelectGame(object sender, MouseButtonEventArgs e)
        {
            var mvvm = App.Mvvm;
            var nvm = App.Nvm;
            var game = sender as Grid;
            var n = game.Tag;

            mvvm.ChangeColor.Execute((string)n);
            nvm.ChangeView.Execute(n);
        }

        public void SelectCollection(object sender, MouseButtonEventArgs e)
        { 
            var mvvm = App.Mvvm;
            var nvm = App.Nvm;
            var game = sender as Grid;
            var n = game.Tag;

            mvvm.ChangeColor.Execute((string)n);
            nvm.ChangeView.Execute(n);
        }

        public void DeleteGame(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            string title = menuItem.Tag.ToString();
            App.Mvvm.DeleteGameData.Execute(title);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var l = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Tag;
            Console.WriteLine("Selected Value");
            Console.WriteLine(l);
            App.Mvvm.ChangeGamesSorting.Execute(l);
        }
        private void ColorTile_Loaded(object sender, RoutedEventArgs e)
        {
            var mvvm = App.Mvvm;
            var game = sender as Grid;
            var n = game.Tag;
            if ((string)n == "Collection")
                mvvm.allgrid = game;
            else
            {
                if(mvvm.grids.FirstOrDefault(x => x.Key == (string)n).Value == null)
                    mvvm.grids.Add((string)n, game);
            }
        }
    }

}
