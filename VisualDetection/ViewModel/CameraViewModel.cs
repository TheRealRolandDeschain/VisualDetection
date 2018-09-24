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
        private DispatcherTimer Timer { get; set; }
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
            if (StartStopCaptureButtonContent == GenDefString.StartCaptureButtonString)
            {
                StartStopCaptureButtonContent = GenDefString.StopCaptureButtonString;
                CaptureCameraFrame();
            }
            else
            {
                StartStopCaptureButtonContent = GenDefString.StartCaptureButtonString;
            }
                
            switch (RadioButtonSelected)
            {
                case CameraViewRadioButtons.OriginalImage:
                    CurrentFrame = MiscMethods.MatToBitmapSource(CameraModel.Instance.CameraViewMat);
                    break;
                case CameraViewRadioButtons.GrayScaleImage:
                    CurrentFrame = MiscMethods.MatToBitmapSource(CameraModel.Instance.CameraViewGrayScaleMat);
                    break;
                case CameraViewRadioButtons.ImageWithDetectedFeaturess:
                    CurrentFrame = MiscMethods.MatToBitmapSource(CameraModel.Instance.CameraViewDetectedFeaturesMat);
                    break;
            }               
        }

        /// <summary>
        /// Capture current frame from selected camera and save it to cameramodel
        /// </summary>
        private void CaptureCameraFrame()
        {
            Capture = new VideoCapture();
            CameraModel.Instance.CameraViewMat = Capture.QueryFrame();
            CameraModel.Instance.CameraViewGrayScaleMat = MiscMethods.MatToGrayscale(CameraModel.Instance.CameraViewMat);
            SURFDetector.CalculateSURFFeatures();
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
