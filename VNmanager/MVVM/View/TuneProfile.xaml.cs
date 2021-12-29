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
    /// Interaction logic for TuneProfile.xaml
    /// </summary>
    public partial class TuneProfile : UserControl
    {
        public TuneProfile()
        {
            InitializeComponent();
            this.DataContext = App.Mvvm;

            App.Mvvm.X1 = App.Mvvm.XT;
            App.Mvvm.Y1 = App.Mvvm.YT;
            
            App.Mvvm.Width1 = App.Mvvm.WidthT;
            App.Mvvm.Height1 = App.Mvvm.HeightT;

            App.Mvvm.X2 = App.Mvvm.XP;
            App.Mvvm.Y2 = App.Mvvm.YP;

            App.Mvvm.Width2 = App.Mvvm.WidthP;
            App.Mvvm.Height2 = App.Mvvm.HeightP;

            Console.WriteLine(App.Mvvm.XP);
            Console.WriteLine(App.Mvvm.YP);
            Console.WriteLine(App.Mvvm.WidthP);
            Console.WriteLine(App.Mvvm.HeightP);

            //Title image
            Canvas canvas = new Canvas();
            canvas = Picture;
            Canvas.SetTop(canvas, 0);
            Canvas.SetLeft(canvas, 0);
            canvas.PreviewMouseDown += UserCTRL_PreviewMouseDown;

            //Page image
            Canvas canvas2 = new Canvas();
            canvas2 = Picture2;
            Canvas.SetTop(canvas2, 0);
            Canvas.SetLeft(canvas2, 0);
            canvas2.PreviewMouseDown += UserCTRL_PreviewMouseDown2;

        }

        #region Title Image

        UIElement dragObject = null;
        Point offset;

        private void UserCTRL_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.dragObject = sender as UIElement;
            this.offset = e.GetPosition(this.Picture);
            this.offset.Y -= Canvas.GetTop(this.dragObject);
            this.offset.X -= Canvas.GetLeft(this.dragObject);
            this.Picture.CaptureMouse();
        }

        private void Picture_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.dragObject == null)
                return;
            var position = e.GetPosition(sender as IInputElement);

            Console.WriteLine("X: "+ (position.X - this.offset.X));
            Console.WriteLine("Y: "+ (position.Y - this.offset.Y));

            App.Mvvm.X1 = (position.X - this.offset.X);
            App.Mvvm.Y1 = (position.Y - this.offset.Y);
        }

        private void Picture_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.dragObject = null;
            this.Picture.ReleaseMouseCapture();
        }

        #endregion

        #region Page Image

        UIElement dragObject2 = null;
        Point offset2;

        private void UserCTRL_PreviewMouseDown2(object sender, MouseButtonEventArgs e)
        {
            this.dragObject2 = sender as UIElement;
            this.offset2 = e.GetPosition(this.Picture2);
            this.offset2.Y -= Canvas.GetTop(this.dragObject2);
            this.offset2.X -= Canvas.GetLeft(this.dragObject2);
            this.Picture2.CaptureMouse();
        }

        private void Picture_PreviewMouseMove2(object sender, MouseEventArgs e)
        {
            if (this.dragObject2 == null)
                return;
            var position = e.GetPosition(sender as IInputElement);

            Console.WriteLine("X: " + (position.X - this.offset2.X));
            Console.WriteLine("Y: " + (position.Y - this.offset2.Y));

            App.Mvvm.X2 = (position.X - this.offset2.X);
            App.Mvvm.Y2 = (position.Y - this.offset2.Y);
        }

        private void Picture_PreviewMouseUp2(object sender, MouseButtonEventArgs e)
        {
            this.dragObject2 = null;
            this.Picture2.ReleaseMouseCapture();
        }
        #endregion


    }
}
