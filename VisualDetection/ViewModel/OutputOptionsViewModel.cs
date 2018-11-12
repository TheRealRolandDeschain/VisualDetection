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
        private int numberOfAllowedUndefinedFrames;
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
                    SetProperty(ref triggerAngle, value);
                }
            }
        }

        /// <summary>
        /// The index of positive frames needed to activate the trigger 
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
                    SetProperty(ref numberOfPositiveFramesNeeded, value);
                }
            }
        }

        /// <summary>
        /// The number of allowed frames with no face detected before the number for positive frames is reset. 
        /// </summary>
        public int NumberOfAllowedUndefinedFrames
        {
            get
            {
                return numberOfAllowedUndefinedFrames;
            }
            set
            {
                if (numberOfAllowedUndefinedFrames != value)
                {
                    SetProperty(ref numberOfAllowedUndefinedFrames, value);
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
            AvailableOptionsList = GenDefList.StandardOutputOptionsList;
            AvailableOptionsListSelectedIndex = GenDefInt.DefaultOutpuOptionSelectedIndex;
            TriggerAngle = GenDefInt.DefaultTriggerAngle;
            NumberOfPositiveFramesNeeded = GenDefInt.DefaultNrOfPositiveFramesNeeded;
            NumberOfAllowedUndefinedFrames = GenDefInt.DefaultNrOfUndefinedFramesAllowed;

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
