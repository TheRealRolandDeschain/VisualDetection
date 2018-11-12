using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualDetection.Model;
using VisualDetection.Util;

namespace VisualDetection.ViewModel
{
    public class OutputOptionsViewModel : ViewModelBase
    {

        #region Constructors
        public OutputOptionsViewModel()
        {
            CameraModel.Instance.outputOptions = this;
            InitializeOutputOptions();
        }
        #endregion

        #region Private Properties
        private int availableOptionsListSelectedIndex;
        private double triggerAngle;
        private int numberOfPositiveFramesNeeded;
        #endregion

        #region Public Properties
        /// <summary>
        /// Options for simulating left and right mouse buttons
        /// </summary>
        public SimulateMouseButtonsOptionsViewModel SimulateMouseButtonsOptions { get; set; }

        /// <summary>
        /// Options for simulating left and right mouse buttons
        /// </summary>
        public SimulateKeyPressOptionsViewModel SimulateKeyPressOptions { get; set; }

        /// <summary>
        /// Options for simulating left and right mouse buttons
        /// </summary>
        public SimulateKeyPressSequenceOptionsViewModel SimulateKeyPressSequenceOptions { get; set; }

        /// <summary>
        /// Options for simulating left and right mouse buttons
        /// </summary>
        public OpenSoftwareOptionsViewModel OpenSoftwareOptions { get; set; }

        /// <summary>
        /// Options for simulating left and right mouse buttons
        /// </summary>
        public CallWindowsStandardFunctionsOptionsViewModel CallWindowsStandardFunctionsOptions { get; set; }

        /// <summary>
        /// The list of available output options
        /// </summary>
        public List<string> AvailableOptionsList { get; set; }

        /// <summary>
        /// The index of the currently selected output option
        /// </summary>
        public int AvailableOptionsListSelectedIndex
        {
            get
            {
                return availableOptionsListSelectedIndex;
            }
            set
            {
                if (availableOptionsListSelectedIndex != value)
                {
                    availableOptionsListSelectedIndex = value;
                    SetProperty(ref availableOptionsListSelectedIndex, value);
                }
            }
        }

        /// <summary>
        /// The index of the currently selected output option
        /// </summary>
        public double TriggerAngle
        {
            get
            {
                return triggerAngle;
            }
            set
            {
                if (triggerAngle != value)
                {
                    triggerAngle = value;
                    SetProperty(ref triggerAngle, value);
                }
            }
        }

        /// <summary>
        /// The index of the currently selected output option
        /// </summary>
        public int NumberOfPositiveFramesNeeded
        {
            get
            {
                return numberOfPositiveFramesNeeded;
            }
            set
            {
                if (numberOfPositiveFramesNeeded != value)
                {
                    numberOfPositiveFramesNeeded = value;
                    SetProperty(ref numberOfPositiveFramesNeeded, value);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates all necessary properties and sets their values
        /// </summary>
        private void InitializeOutputOptions()
        {
            AvailableOptionsList = GenDefList.StandardOutputOptionsList.ToList();
            AvailableOptionsListSelectedIndex = GenDefInt.DefaultOutpuOptionSelectedIndex;
            TriggerAngle = GenDefInt.DefaultTriggerAngle;

            SimulateMouseButtonsOptions = new SimulateMouseButtonsOptionsViewModel();
            SimulateKeyPressOptions = new SimulateKeyPressOptionsViewModel();
            SimulateKeyPressSequenceOptions = new SimulateKeyPressSequenceOptionsViewModel();
            OpenSoftwareOptions = new OpenSoftwareOptionsViewModel();
            CallWindowsStandardFunctionsOptions = new CallWindowsStandardFunctionsOptionsViewModel();
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
