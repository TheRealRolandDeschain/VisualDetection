using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualDetection.Model;
using VisualDetection.Util;
using VisualDetection.Interfaces;
using System.Windows;

namespace VisualDetection.ViewModel
{
    public class OutputOptionsViewModel : ViewModelBase
    {

        #region Constructors
        public OutputOptionsViewModel()
        {
            CameraModel.Instance.OutputOptions = this;
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
        /// The list of available output options
        /// </summary>
        public List<IOutputOption> AvailableOptionsList { get; set; }

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
                    HandleTriggerStatusEventSubscribers();
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
            AvailableOptionsList = new List<IOutputOption>()
            {
                new SimulateMouseButtonsOptionsViewModel(),
                new SimulateKeyPressSequenceOptionsViewModel(),
                new OpenSoftwareOptionsViewModel(),
            };
            AvailableOptionsListSelectedIndex = GenDefInt.DefaultOutpuOptionSelectedIndex;
            TriggerAngle = GenDefInt.DefaultTriggerAngle;
            NumberOfPositiveFramesNeeded = GenDefInt.DefaultNrOfPositiveFramesNeeded;
            NumberOfAllowedUndefinedFrames = GenDefInt.DefaultNrOfUndefinedFramesAllowed;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This Method handles which one of the output options viewmodels is subscribed to 
        /// TriggerStatusChangedEvent and thus handling which option is active
        /// </summary>
        public void HandleTriggerStatusEventSubscribers()
        {
            for (int i = 0; i < AvailableOptionsList.Count; i++)
            {
                if (i == AvailableOptionsListSelectedIndex)
                {
                    CameraModel.Instance.Output.TriggerStatusChanged += AvailableOptionsList[i].OnTriggerOnTriggerStatusChanged;
                }
                else
                {
                    CameraModel.Instance.Output.TriggerStatusChanged -= AvailableOptionsList[i].OnTriggerOnTriggerStatusChanged;
                }
            }
        }
        #endregion
    }
}
