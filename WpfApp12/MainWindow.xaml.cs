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
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace WpfApp12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {            
            InitializeComponent();            
        }

        public int brushSize;

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
       
        private void menuPrint_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(canvas, "My First Print Job");
            }
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
         {
            int width = (int)canvas.ActualWidth;
            int height = (int)canvas.ActualHeight;            

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Picture";
            dlg.DefaultExt = ".jpeg";
            dlg.Filter = "JPEG (.jpeg)|*.jpeg";
            ImageFormat format = ImageFormat.Jpeg;

             Nullable<bool> result = dlg.ShowDialog();
            
             if (result == true)
             {
                SaveImage(canvas, width, height, dlg.FileName);
            }
             
        }

        private void SaveImage(object myCanvas, int width, int height, string filePath)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(canvas);
            double dpi = 96d;
            RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, dpi, dpi, System.Windows.Media.PixelFormats.Default);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(canvas);
                dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }

            rtb.Render(dv);

            BmpBitmapEncoder image = new BmpBitmapEncoder();
            image.Frames.Add(BitmapFrame.Create(rtb));

            using (Stream fs = File.Create(filePath))
            {
                image.Save(fs);
                fs.Close();
            }
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
         {
            MemoryStream memoStream = new MemoryStream();

                           
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Picture";
            dlg.DefaultExt = ".jpeg";
            dlg.Filter = "JPEG (.jpeg)|*.jpeg";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                using (FileStream fs = File.OpenRead(dlg.FileName))
                {
                    fs.CopyTo(memoStream);

                    BitmapImage bmi = new BitmapImage();

                    bmi.BeginInit();

                    bmi.StreamSource = memoStream;

                    bmi.EndInit();

                    ImageBrush brush = new ImageBrush(bmi);

                    canvas.Background = brush;

                    fs.Close();
                }
            }                 
         }

        private void canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {            

            Title = "Hiiri " + e.GetPosition(this);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Ellipse brush1 = new Ellipse();
                if(brushSize>0)
                brush1.Width = brush1.Height = brushSize;
                else
                {
                    brush1.Width = brush1.Height = 20;
                }
                brush1.Fill = Brushes.Black;
                canvas.Children.Add(brush1);
                Canvas.SetLeft(brush1, e.GetPosition(canvas).X);
                Canvas.SetTop(brush1, e.GetPosition(canvas).Y);
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                Ellipse brush2 = new Ellipse();
                if (brushSize > 0)
                    brush2.Width = brush2.Height = brushSize;
                else
                {
                    brush2.Width = brush2.Height = 20;
                }
                brush2.Fill = Brushes.White;
                canvas.Children.Add(brush2);
                Canvas.SetLeft(brush2, e.GetPosition(canvas).X);
                Canvas.SetTop(brush2, e.GetPosition(canvas).Y);
            }
        }

        private void brush_Click(object sender, RoutedEventArgs e)
        {
            Window1 mywindow = new Window1();            

            if (mywindow.ShowDialog() == true)
            {
                System.Windows.MessageBox.Show(mywindow.textBlock.Text);
            }
            brushSize = (int) mywindow.slider1.Value;
            
        }
        private void menuLaskin_Click(object sender, RoutedEventArgs e)
        {
            Form Form = new calculator1.Form1();
            Form.Show();
        }

        private void menuSelain_Click(object sender, RoutedEventArgs e)
        {
            Form form1 = new WpfApp12.Form1();
            form1.Show();
        }
    }
}
