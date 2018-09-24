using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
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
        }
        #endregion

        #region Private Properties
        private BitmapSource currentFrameOriginal;
        private BitmapSource currentFrameGray;
        private RelayCommand startStopCaptureButtonClicked;
        private string startStopCaptureButtonContent;
        #endregion


        #region Private Fields
        private DispatcherTimer Timer { get; set; }
        private VideoCapture Capture { get; set; }
        #endregion

        #region Public Fields
        #endregion


        #region Public Properties
        /// <summary>
        /// The output Image of the original frame
        /// </summary>
        public BitmapSource CurrentFrameOriginal
        {
            get { return currentFrameOriginal; }
            set
            {
                if (currentFrameOriginal != value)
                {
                    SetProperty(ref currentFrameOriginal, value);
                }
            }
        }

        /// <summary>
        /// The output Image of the grayScale frame
        /// </summary>
        public BitmapSource CurrentFrameGray
        {
            get { return currentFrameGray; }
            set
            {
                if (currentFrameGray != value)
                {
                    SetProperty(ref currentFrameGray, value);
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
        #endregion


        #region Private Methods
        /// <summary>
        /// start capture and processing with the selected settings
        /// </summary>
        private void StartStopCapture()
        {
            switch(StartStopCaptureButtonContent)
            {
                case GenDefString.StartCaptureButtonString:
                StartStopCaptureButtonContent = GenDefString.StopCaptureButtonString;
                break;
                case GenDefString.StopCaptureButtonString:
                StartStopCaptureButtonContent = GenDefString.StartCaptureButtonString;
                break;
            }
        }

        /// <summary>
        /// Capture current frame from selected camera and save it to cameramodel
        /// </summary>
        /// <param name="camera"></param>
        private void CaptureCameraFrame(VideoCapture camera)
        {
            var cModel = CameraModel.Instance;

            cModel.CameraViewMat = Capture.QueryFrame();
            CurrentFrameOriginal = MiscMethods.MatToBitmapSource(cModel.CameraViewMat);
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
