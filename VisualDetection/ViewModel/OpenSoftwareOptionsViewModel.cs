using System;
using VisualDetection.Util;
using VisualDetection.Interfaces;
using VisualDetection.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VisualDetection.ViewModel
{
    public class OpenSoftwareOptionsViewModel : ViewModelBase, IOutputOption
    {
        #region Constructor
        public OpenSoftwareOptionsViewModel()
        {
            SetDefaultValues();
        }
        #endregion

        #region Private Properties
        private int indexOfSelectedProcess;
        #endregion

        #region Public Properties

        /// <summary>
        /// The index of the currently selected index
        /// </summary>
        public ObservableCollection<ProcessModel> ListOfProcesses { get; set; }

        /// <summary>
        /// Title of this Option
        /// </summary>
        public string OptionTitle { get; private set; }

        /// <summary>
        /// The index of the currently selected index
        /// </summary>
        public int IndexOfSelectedProcess
        {
            get { return indexOfSelectedProcess; }
            set
            {
                if (indexOfSelectedProcess != value)
                {
                    SetProperty(ref indexOfSelectedProcess, value);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the default values
        /// </summary>
        private void SetDefaultValues()
        {
            OptionTitle = GenDefString.OpenExternalSoftwareTitle;
            ListOfProcesses = new ObservableCollection<ProcessModel>();
            ListOfProcesses.Add(new ProcessModel(@"C:\Program Files (x86)\Notepad++\notepad++.exe"));
            ListOfProcesses.Add(new ProcessModel(@"C:\Program Files\Tracker Software\PDF Editor\PDFXEdit.exe"));
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Trigger Status changed Event was raised
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnTriggerStatusChanged(object source, EventArgs e)
        {
        }
        #endregion
    }
}
