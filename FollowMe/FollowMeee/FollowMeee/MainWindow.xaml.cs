using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int counter = 0;
        int speed = 1;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        TextBlock tb = new TextBlock();
        //---------------------------------------------------
        double nextStop_X = 0;
        double nextStop_Y = 0;
        //---------------------------------------------------
        List<Ellipse> rectList = new List<Ellipse>();

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            double calculatedAdded_X = 0;
            double calculatedAdded_Y = 0;

            double currentX = 0;
            double currentY = 0;
            if (rectList.Count != 0)
            {
                currentX = rectList[counter - 1].TransformToAncestor(this).Transform(new Point(0, 0)).X;
                currentY = rectList[counter - 1].TransformToAncestor(this).Transform(new Point(0, 0)).Y;
                removeRect(rectList[counter - 1]);
            }
            // Where is it going next?

            if (nextStop_X != currentX)
            {
                if (nextStop_X > currentX)
                {
                    calculatedAdded_X = speed;
                }
                else
                {
                    calculatedAdded_X = -speed;
                }
            }
            else
            {
                if (nextStop_Y != currentY)
                {
                    if (nextStop_Y > currentY)
                    {
                        calculatedAdded_Y = speed;
                    }
                    else
                    {
                        calculatedAdded_Y = -speed;
                    }
                }
            }

            // Draw it !
            createRect(currentX + calculatedAdded_X,
                        currentY + calculatedAdded_Y);
        }
        void createRect(double x, double y)
        {
            rectList.Insert(counter, new Ellipse());
            Ellipse tmp = new Ellipse();
            tmp = rectList[counter];

            tmp.Fill = Brushes.DarkSlateBlue;
            tmp.Width = 10;
            tmp.Height = 10;
            Canvas.SetLeft(tmp, x);
            Canvas.SetTop(tmp, y);

            Ellipse tmpShadow = new Ellipse();
            tmpShadow.Fill = tmp.Fill;
            tmpShadow.Width = 5;
            tmpShadow.Height = 5;
            Canvas.SetLeft(tmpShadow, x);
            Canvas.SetTop(tmpShadow, y);
            tmpShadow.Opacity = 0.05;
            mainCanvas.Children.Add(tmpShadow);


            mainCanvas.Children.Add(tmp);
            counter++;
        }
        void removeRect(Ellipse pRct)
        {
            mainCanvas.Children.Remove(pRct);
        }
        public MainWindow()
        {
            InitializeComponent();
            tb.Height = 30;
            tb.Width = 100;

            mainCanvas.Children.Add(tb);

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            nextStop_X = e.GetPosition(this).X;
            nextStop_Y = e.GetPosition(this).Y;

            tb.Text = e.GetPosition(this).X.ToString() + " , " + e.GetPosition(this).Y.ToString();
        }
    }
}
