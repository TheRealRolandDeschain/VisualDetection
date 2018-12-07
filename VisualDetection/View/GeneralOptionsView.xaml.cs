using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VisualDetection.Util;

namespace VisualDetection.View
{
    /// <summary>
    /// Interaction logic for GeneralOptionsView.xaml
    /// </summary>
    public partial class GeneralOptionsView : UserControl
    {
        public GeneralOptionsView()
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
