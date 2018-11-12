using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using VisualDetection.Detectors;
using VisualDetection.Model;
using VisualDetection.Util;

namespace VisualDetection.ViewModel
{
    public class CameraViewModel : ViewModelBase
    {
        #region Constructors
        public CameraViewModel()
        {
            StartStopCaptureButtonContent = GenDefString.StartCaptureButtonString;
            RadioButtonSelected = CameraViewRadioButtons.OriginalImage;
            timer = new Stopwatch();
        }
        #endregion

        #region Private Properties
        private BitmapSource currentFrame;
        private RelayCommand startStopCaptureButtonClicked;
        private string startStopCaptureButtonContent;
        private bool radioButtonOriginalImageViewChecked;
        private bool radioButtonGrayScaleImageViewChecked;
        private bool radioButtonDetectedFeaturesImageViewChecked;
        private VideoCapture Capture { get; set; }
        private Dispatcher dispatcher = App.Current.Dispatcher;
        private CameraModel cm = CameraModel.Instance;
        private Stopwatch timer;
        private double? eyeAngle;
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
            Task captureTask = new Task(CaptureCameraFrame);
            if (StartStopCaptureButtonContent == GenDefString.StopCaptureButtonString)
            {
                Capture.Stop();
                Capture.Dispose();
                StartStopCaptureButtonContent = GenDefString.StartCaptureButtonString;
            }
            else
            {
                Capture = new VideoCapture(cm.generalOptions.SelectedCameraIndex);
                Capture.Start();
                captureTask.Start();
                StartStopCaptureButtonContent = GenDefString.StopCaptureButtonString;
            }
        }

        /// <summary>
        /// Capture current frame from selected camera and save it to cameramodel
        /// </summary>
        private void CaptureCameraFrame()
        {
            if (Capture.Grab()) Capture.Retrieve(cm.CameraViewMat);
            timer.Restart();
            CvInvoke.CvtColor(cm.CameraViewMat, cm.CameraViewGrayScaleMat, ColorConversion.Bgr2Gray);
            if (cm.generalOptions.UseEqualizeHist)
            {
                CvInvoke.EqualizeHist(cm.CameraViewGrayScaleMat, cm.CameraViewGrayScaleMat);
            }
            eyeAngle = CascadeClassifierClass.Detect(RadioButtonDetectedFeaturesImageViewChecked);

            timer.Stop();
            dispatcher.Invoke(() => SetOutputFromCapturedFrame(), DispatcherPriority.Normal);
            
            if (StartStopCaptureButtonContent == GenDefString.StopCaptureButtonString)
            {
                Thread.Sleep(cm.generalOptions.IdleAfterFrameCalculationMS);
                CaptureCameraFrame();
            }
            else
            {
                dispatcher.Invoke(() => SetDefaultImageToCameraOutput(), DispatcherPriority.Normal);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the image output corresponding to the selected RadioButton
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void SetOutputFromCapturedFrame()
        {
            if (RadioButtonSelected == CameraViewRadioButtons.OriginalImage || RadioButtonSelected == CameraViewRadioButtons.ImageWithDetectedFeaturess)
            {
                CurrentFrame = MiscMethods.MatToBitmapSource(cm.CameraViewMat);
            }
            else
            { 
                CurrentFrame = MiscMethods.MatToBitmapSource(cm.CameraViewGrayScaleMat);
            }

            cm.output.UpdateOutputValues(eyeAngle, (int)timer.ElapsedMilliseconds);
        }


        /// <summary>
        /// sets the default image to the image output
        /// </summary>
        public void SetDefaultImageToCameraOutput()
        {
            CurrentFrame = new BitmapImage(new Uri(GenDefString.DefaultImagePathString, UriKind.Relative));
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Eventhandlers and Delegates
        #endregion
    }
}
 