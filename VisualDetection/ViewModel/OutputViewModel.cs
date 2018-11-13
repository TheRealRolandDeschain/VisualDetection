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
            SetDefaultOutputValues();

        }
        #endregion

        #region Private Properties
        private double eyeAngle;
        private int frameCalculationTime;
        private int positiveFrames;
        private int numberOfFramesWithNoFace;
        private bool faceDetected;
        private bool leftTriggerActive;
        private bool rightTriggerActive;
        private Brush faceDetectedIndicator;
        private Brush leftTriggerActiveIndicator;
        private Brush rightTriggerActiveIndicator;
        private CameraModel cm = CameraModel.Instance;
        #endregion

        #region Public Properties

        /// <summary>
        /// The angle between the eyes showing the tilt of the head
        /// </summary>
        public double EyeAngle
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
        /// Shows how many frames there have been without detecting a face. Detecting a face will reset the value. 
        /// </summary>
        public int NumberOfFramesWithNoFace
        {
            get { return numberOfFramesWithNoFace; }
            set
            {
                if (numberOfFramesWithNoFace != value)
                {
                    SetProperty(ref numberOfFramesWithNoFace, value);
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
        /// Shows if a the left trigger was activated
        /// </summary>
        public bool LeftTriggerActive
        {
            get { return leftTriggerActive; }
            set
            {
                if (leftTriggerActive != value)
                {
                    OnTriggerStatusChanged();
                    LeftTriggerActiveIndicator = UpdateIndicator(value);
                    SetProperty(ref leftTriggerActive, value);
                }
            }
        }

        /// <summary>
        /// Shows if a the right trigger was activated
        /// </summary>
        public bool RightTriggerActive
        {
            get { return rightTriggerActive; }
            set
            {
                if (rightTriggerActive != value)
                {
                    OnTriggerStatusChanged();
                    RightTriggerActiveIndicator = UpdateIndicator(value);
                    SetProperty(ref rightTriggerActive, value);
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
        /// Shows if a the left trigger was activated
        /// </summary>
        public Brush LeftTriggerActiveIndicator
        {
            get { return leftTriggerActiveIndicator; }
            set
            {
                if (leftTriggerActiveIndicator != value)
                {
                    SetProperty(ref leftTriggerActiveIndicator, value);
                }
            }
        }

        /// <summary>
        /// Shows if a the Right trigger was activated
        /// </summary>
        public Brush RightTriggerActiveIndicator
        {
            get { return rightTriggerActiveIndicator; }
            set
            {
                if (rightTriggerActiveIndicator != value)
                {
                    SetProperty(ref rightTriggerActiveIndicator, value);
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

        /// <summary>
        /// Checks if the Trigger should be activated based on the calculated frames
        /// </summary>
        /// <returns></returns>
        private void CheckTriggerActivation()
        {
            if (NumberOfFramesWithNoFace > cm.outputOptions.NumberOfAllowedUndefinedFrames)
            {
                PositiveFrames = 0;
            }
            else
            {
                if (Math.Abs(EyeAngle) > cm.outputOptions.TriggerAngle)
                {
                    PositiveFrames++;
                    if((EyeAngle > 0) && (PositiveFrames > cm.outputOptions.NumberOfPositiveFramesNeeded))
                    {
                        LeftTriggerActive = true;
                    }
                    else if ((EyeAngle < 0) && (PositiveFrames > cm.outputOptions.NumberOfPositiveFramesNeeded))
                    {
                        RightTriggerActive = true;
                    }
                }
                else
                {
                    LeftTriggerActive = false;
                    RightTriggerActive = false;
                    PositiveFrames = 0;
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets all displayed values back to default
        /// </summary>
        public void SetDefaultOutputValues()
        {
            FaceDetectedIndicator = Brushes.Red;
            LeftTriggerActiveIndicator = Brushes.Red;
            RightTriggerActiveIndicator = Brushes.Red;
            EyeAngle = 0;
            FrameCalculationTime = 0;
            PositiveFrames = 0;
            NumberOfFramesWithNoFace = 0;
            FaceDetected = false;
            LeftTriggerActive = false;
            RightTriggerActive = false;
        }

        /// <summary>
        /// Updates the output values calculated from the current frame
        /// </summary>
        public void UpdateOutputValues(double? eyeAngle, int calculationTime)
        {
            FrameCalculationTime = calculationTime;
            if (eyeAngle == null)
            {
                EyeAngle = 0;
                FaceDetected = false;
                NumberOfFramesWithNoFace++;
            }
            else
            {
                FaceDetected = true;
                EyeAngle = Math.Round((double)eyeAngle, 2);
                NumberOfFramesWithNoFace = 0;
            }
            CheckTriggerActivation();
        }
        #endregion

        #region Protected Methods
        protected virtual void OnTriggerStatusChanged()
        {
            if(TriggerStatusChanged != null)
            {
                TriggerStatusChanged(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Public Delegates and Events
        /// <summary>
        /// Event for status changes of left or right trigger
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public delegate void TriggerStatusChangedEventHandler(object source, EventArgs e);
        public event TriggerStatusChangedEventHandler TriggerStatusChanged;
        #endregion
    }
}
