using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualDetection.Util;
using VisualDetection.Interfaces;
using VisualDetection.Model;

namespace VisualDetection.ViewModel
{
    public class SimulateKeyPressSequenceOptionsViewModel : ViewModelBase, IOutputOption
    {
        #region Constructor
        public SimulateKeyPressSequenceOptionsViewModel()
        {
            SetDefaultValues();
        }
        #endregion

        #region Private Properties
        #endregion

        #region Public Properties
        /// <summary>
        /// Title of this Option
        /// </summary>
        public string OptionTitle { get; private set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the default values
        /// </summary>
        private void SetDefaultValues()
        {
            OptionTitle = GenDefString.SimulateKeyPressSequenceTitle;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Trigger Status changed Event was raised
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnTriggerOnTriggerStatusChanged(object source, EventArgs e)
        {
            System.Windows.MessageBox.Show(OptionTitle);
        }
        #endregion
    }
}
