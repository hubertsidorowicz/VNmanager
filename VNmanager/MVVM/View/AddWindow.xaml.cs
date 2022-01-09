using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace VNmanager
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        string pickedGame;
        public AddWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {

            #region File Dialog

            pickedGame = null;
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true) {
                pickedGame = fileDialog.FileName;
            }

            #endregion

            #region Call Add Game Command

            if (pickedGame != null)
            {
                MainViewModel mvvm = App.Mvvm;

                mvvm.title = GetGameTitle(pickedGame);
                mvvm.gameUrl = pickedGame;

                mvvm.AddGameCommand.Execute(pickedGame);
            }

            #endregion
        }
        string GetGameTitle(string url)
        {
            var ext = Path.GetExtension(url);
            string fold = Path.GetFileName(Path.GetDirectoryName(url));

            return fold;
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            }
        }
    }
}
