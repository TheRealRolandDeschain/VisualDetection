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

namespace VisualDetection.View
{
    /// <summary>
    /// Interaction logic for OutputView.xaml
    /// </summary>
    public partial class OutputView : UserControl
    {
        public OutputView()
        {
            InitializeComponent();
        }


        private void Test_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Test.Background = Brushes.Red;
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Test.Background = Brushes.Blue;
            }
            else if (e.MiddleButton == MouseButtonState.Pressed)
            {
                Test.Background = Brushes.Green;
            }
        }

        private void Test_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Test.Background = Brushes.White;
        }

        private void Test_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(e.Delta > 0)
            {
                Test.Background = Brushes.Yellow;
            }
            else
            {
                Test.Background = Brushes.Brown;
            }
        }
    }
}
