using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using VisualDetection.Model;

namespace VisualDetection.ViewModel
{
    public class OutputViewModel : ViewModelBase
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public OutputViewModel()
        {
            CameraModel.Instance.output = this;
            FaceDetectedIndicator = Brushes.Red;
            TriggerActiveIndicator = Brushes.Red;
        }
        #endregion

        #region Private Properties
        private double? eyeAngle;
        private int frameCalculationTime;
        private int positiveFrames;
        private bool faceDetected;
        private bool triggerActive;
        private Brush faceDetectedIndicator;
        private Brush triggerActiveIndicator;
        private CameraModel cm = CameraModel.Instance;
        #endregion

        #region Public Properties

        /// <summary>
        /// The angle between the eyes showing the tilt of the head
        /// </summary>
        public double? EyeAngle
        {
            get { return eyeAngle; }
            set
            {
                if (eyeAngle != value)
                {
                    SetProperty(ref eyeAngle, value);
                }
            }
        }

        /// <summary>
        /// The time needed for the calculation of the last frame
        /// </summary>
        public int FrameCalculationTime
        {
            get { return frameCalculationTime; }
            set
            {
                if (frameCalculationTime != value)
                {
                    SetProperty(ref frameCalculationTime, value);
                }
            }
        }

        /// <summary>
        /// Shows how many positive frames have occured (positive frame = frame with calculated eye angle over set limit
        /// </summary>
        public int PositiveFrames
        {
            get { return positiveFrames; }
            set
            {
                if (positiveFrames != value)
                {
                    SetProperty(ref positiveFrames, value);
                }
            }
        }

        /// <summary>
        /// Shows if a face is currently detected
        /// </summary>
        public bool FaceDetected
        {
            get { return faceDetected; }
            set
            {
                if (faceDetected != value)
                {
                    FaceDetectedIndicator = UpdateIndicator(value);
                    SetProperty(ref faceDetected, value);
                }
            }
        }

        /// <summary>
        /// Shows if a the trigger was activated
        /// </summary>
        public bool TriggerActive
        {
            get { return triggerActive; }
            set
            {
                if (triggerActive != value)
                {
                    TriggerActiveIndicator = UpdateIndicator(value);
                    SetProperty(ref triggerActive, value);
                }
            }
        }

        /// <summary>
        /// Indicates if a face is currently detected
        /// </summary>
        public Brush FaceDetectedIndicator
        {
            get { return faceDetectedIndicator; }
            set
            {
                if (faceDetectedIndicator != value)
                {
                    SetProperty(ref faceDetectedIndicator, value);
                }
            }
        }

        /// <summary>
        /// Shows if a the trigger was activated
        /// </summary>
        public Brush TriggerActiveIndicator
        {
            get { return triggerActiveIndicator; }
            set
            {
                if (triggerActiveIndicator != value)
                {
                    SetProperty(ref triggerActiveIndicator, value);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// transforms bool value to green and red color
        /// </summary>
        /// <param name="val"></param>
        private Brush UpdateIndicator(bool val)
        {
            if (val) return Brushes.Green;
            else return Brushes.Red;
        }

        private bool CheckTriggerActivation()
        {
            if (PositiveFrames >= cm.outputOptions.NumberOfPositiveFramesNeeded) return true;
            else return false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Updates the output values calculated from the current frame
        /// </summary>
        public void UpdateOutputValues(double? eyeAngle, int calculationTime)
        {
            FrameCalculationTime = calculationTime;
            if (eyeAngle == null)
            {
                EyeAngle = eyeAngle;
                FaceDetected = false;
                PositiveFrames = 0;
            }
            else
            {
                FaceDetected = true;
                EyeAngle = Math.Round((double)eyeAngle, 2);
                if(Math.Abs((double)EyeAngle) > cm.outputOptions.TriggerAngle)
                {
                    PositiveFrames++;
                }
                else
                {
                    PositiveFrames = 0;
                }
            }
            TriggerActive = CheckTriggerActivation();
        }
        #endregion
    }
}
