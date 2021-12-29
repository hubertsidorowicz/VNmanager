using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for GameCollection.xaml
    /// </summary>
    public partial class GameCollection : UserControl
    {
        public GameCollection()
        {
            InitializeComponent();
            this.DataContext = App.Mvvm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mvvm = App.Mvvm;
            var nvm = App.Nvm;
            var game = sender as Button;
            var n = game.Tag;
            mvvm.ChangeColor.Execute((string)n);
            nvm.ChangeView.Execute((string)n);
        }
    }
}
