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
using VisualDetection.Model;
using VisualDetection.Util;

namespace VisualDetection.View
{
    /// <summary>
    /// Interaction logic for CallWindowsStandardFunctionsOptionsView.xaml
    /// </summary>
    public partial class CallWindowsStandardFunctionsOptionsView : UserControl
    {
        public CallWindowsStandardFunctionsOptionsView()
        {
            InitializeComponent();
            DataContext = CameraModel.Instance.OutputOptions.AvailableOptionsList.First(option => option.OptionTitle == GenDefString.CallWindowsStandardFunctionTitle);
        }
    }
}
