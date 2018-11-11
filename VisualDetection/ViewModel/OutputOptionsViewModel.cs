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
            AvailableOptionsList = GenDefList.StandardOutputOptionsList.ToList();
            AvailableOptionsListSelectedIndex = GenDefInt.DefaultOutpuOptionSelectedIndex;

        }
        #endregion

        #region Private Properties
        private int availableOptionsListSelectedIndex;
        private double triggerAngle;
        private int numberOfPositiveFramesNeeded;
        #endregion

        #region Public Properties
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
        #endregion

        #region Public Methods
        #endregion
    }
}
