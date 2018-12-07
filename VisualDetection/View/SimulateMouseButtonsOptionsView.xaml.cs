using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VisualDetection.Util;

namespace VisualDetection.View
{
    /// <summary>
    /// Interaction logic for SimulateMouseButtonsOptionsView.xaml
    /// </summary>
    public partial class SimulateMouseButtonsOptionsView : UserControl
    {
        public SimulateMouseButtonsOptionsView()
        {
            InitializeComponent();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextboxValueUpdater.UpdateValue(sender);
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextboxValueUpdater.UpdateValue(sender);
            }
        }
    }
}
