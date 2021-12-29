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
using System.Windows.Shapes;

namespace VNmanager
{
    /// <summary>
    /// Interaction logic for GetTitle.xaml
    /// </summary>
    public partial class GetTitle : Window
    {
        public GetTitle()
        {
            InitializeComponent();
            this.DataContext = App.Mvvm;
            input.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Mvvm.newTitle = input.Text;
            App.Mvvm.ChangeGameData.Execute("Title");
            this.Close();
        }
    }
}
