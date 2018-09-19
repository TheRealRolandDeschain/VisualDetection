using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using VisualDetection.Model;

namespace VisualDetection.ViewModel
{
    public class CameraViewModel : ViewModelBase
    {

        #region Constructors
        public CameraViewModel()
        {
            StartVideo();
        }
        #endregion

        #region Private Properties
        private BitmapSource currentFrameOriginal;
        private BitmapSource currentFrameGray;
        #endregion


        #region Private Fields
        private DispatcherTimer Timer { get; set; }
        private VideoCapture Capture { get; set; }
        #endregion

        #region Public Fields
        #endregion


        #region Public Properties
        /// <summary>
        /// The output Image of the Original gFrame
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
        /// The output Image of the GrayScale Frame
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
        #endregion


        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        private void StartVideo()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(10);
            var usedCamera = new CaptureType();
            Capture = new VideoCapture(usedCamera);
            Timer.Tick += new EventHandler((object s, EventArgs a) =>
            {
                CaptureCameraFrame(Capture);
            });
            Timer.Start();
        }

        private void CaptureCameraFrame(VideoCapture camera)
        {
            var cModel = CameraModel.Instance;

            cModel.CameraViewMat = Capture.QueryFrame();
            CurrentFrameOriginal = cModel.MatToBitmapSource(cModel.CameraViewMat);
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
