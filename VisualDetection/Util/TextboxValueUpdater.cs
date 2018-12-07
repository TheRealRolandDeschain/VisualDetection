using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace VisualDetection.Util
{
    public static class TextboxValueUpdater
    {

        /// <summary>
        /// Updates the value to the respective binding of the caller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void UpdateValue(object sender)
        {
            var textbox = sender as TextBox;
            if (textbox == null) return;
            BindingExpression be = textbox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }
    }
}
