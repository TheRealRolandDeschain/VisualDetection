using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using VisualDetection.Model;
using VisualDetection.Util;
using VisualDetection.Detectors;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Linq.Expressions;
using System.Reflection;

namespace VisualDetection.ViewModel
{
    public class CameraViewModel : ViewModelBase
    {

        #region Constructors
        public CameraViewModel()
        {
            StartStopCaptureButtonContent = GenDefString.StartCaptureButtonString;
            RadioButtonSelected = CameraViewRadioButtons.OriginalImage;
        }
        #endregion

        #region Private Properties
        private BitmapSource currentFrame;
        private RelayCommand startStopCaptureButtonClicked;
        private string startStopCaptureButtonContent;
        private bool radioButtonOriginalImageViewChecked;
        private bool radioButtonGrayScaleImageViewChecked;
        private bool radioButtonDetectedFeaturesImageViewChecked;
        #endregion


        #region Private Fields
        private VideoCapture Capture { get; set; }
        #endregion

        #region Public Fields
        #endregion


        #region Public Properties
        /// <summary>
        /// The output Image
        /// </summary>
        public BitmapSource CurrentFrame
        {
            get { return currentFrame; }
            set
            {
                if (currentFrame != value)
                {
                    SetProperty(ref currentFrame, value);
                }
            }
        }

        /// <summary>
        /// The StartStopCapturebutton was clicked
        /// </summary>
        public ICommand StartStopCaptureButtonClicked
        {
            get
            {
                return startStopCaptureButtonClicked ?? (startStopCaptureButtonClicked = new RelayCommand(command => StartStopCapture()));
            }
        }

        /// <summary>
        /// The Content of the StartStopCaptureButton
        /// </summary>
        public string StartStopCaptureButtonContent
        {
            get { return startStopCaptureButtonContent; }
            set
            {
                if (startStopCaptureButtonContent != value)
                {
                    SetProperty(ref startStopCaptureButtonContent, value);
                }
            }
        }

        /// <summary>
        /// The view for original image was checked
        /// </summary>
        public bool RadioButtonOriginalImageViewChecked
        {
            get { return radioButtonOriginalImageViewChecked; }
            set
            {
                if (radioButtonOriginalImageViewChecked != value)
                {
                    SetProperty(ref radioButtonOriginalImageViewChecked, value);
                }
            }
        }

        /// <summary>
        /// The view for grayscale image was checked
        /// </summary>
        public bool RadioButtonGrayScaleImageViewChecked
        {
            get { return radioButtonGrayScaleImageViewChecked; }
            set
            {
                if (radioButtonGrayScaleImageViewChecked != value)
                {
                    SetProperty(ref radioButtonGrayScaleImageViewChecked, value);
                }
            }
        }


        /// <summary>
        /// The view for image with detected features was checked
        /// </summary>
        public bool RadioButtonDetectedFeaturesImageViewChecked
        {
            get { return radioButtonDetectedFeaturesImageViewChecked; }
            set
            {
                if (radioButtonDetectedFeaturesImageViewChecked != value)
                {
                    SetProperty(ref radioButtonDetectedFeaturesImageViewChecked, value);
                }
            }
        }


        /// <summary>
        /// Determines which RadioButton is selected, working as a quasi-converter to enum
        /// </summary>
        public CameraViewRadioButtons RadioButtonSelected
        {
            get
            {
                if (RadioButtonOriginalImageViewChecked) return CameraViewRadioButtons.OriginalImage;
                else if (RadioButtonGrayScaleImageViewChecked) return CameraViewRadioButtons.GrayScaleImage;
                else return CameraViewRadioButtons.ImageWithDetectedFeaturess;
            }
            set
            {
                if (value == CameraViewRadioButtons.OriginalImage) RadioButtonOriginalImageViewChecked = true;
                else if (value == CameraViewRadioButtons.GrayScaleImage) RadioButtonGrayScaleImageViewChecked = true;
                else RadioButtonDetectedFeaturesImageViewChecked = true;
            }
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// start capture and processing with the selected settings
        /// </summary>
        private void StartStopCapture()
        {
            Capture = new VideoCapture();
            Task capturetask = new Task(CaptureCameraFrame);
            if (StartStopCaptureButtonContent == GenDefString.StartCaptureButtonString)
            {
                StartStopCaptureButtonContent = GenDefString.StopCaptureButtonString;
                capturetask.Start();
            }
            else
            {
                StartStopCaptureButtonContent = GenDefString.StartCaptureButtonString;
                CurrentFrame = new BitmapImage(new Uri("..\\Icons\\DefaultImage.png", UriKind.Relative));
                Capture.Dispose();
            }
        }

        /// <summary>
        /// Capture current frame from selected camera and save it to cameramodel
        /// </summary>
        public void CaptureCameraFrame()
        {
            CameraModel.Instance.CameraViewMat = Capture.QueryFrame();
            CameraModel.Instance.CameraViewGrayScaleMat = MiscMethods.MatToGrayscale(CameraModel.Instance.CameraViewMat);
            SURFDetector.CalculateSURFFeatures();
            CaptureCameraFrame();
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
