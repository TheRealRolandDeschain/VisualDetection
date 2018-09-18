using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
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
        private BitmapSource currentFrame;
        #endregion


        #region Public Fields
        private DispatcherTimer Timer { get; set; }
        private VideoCapture Capture { get; set; }
        #endregion


        #region Public Properties
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
        #endregion


        #region Private Methods
        private void StartVideo()
        {
            Capture = new VideoCapture();
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(10);
            Timer.Tick += new EventHandler((object s, EventArgs a) =>
            {
                //draw the image obtained from camera
                var frame = Capture.QueryFrame();
                CurrentFrame = MatToBitmapSource(frame);
            });
            Timer.Start();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get BitmapSource from image matrix for GUI
        /// </summary>
        public static BitmapSource MatToBitmapSource(Mat image)
        {
            using (Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap
                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }
        #endregion

        #region DLL
        /// <summary>
        /// Delete a GDI object
        /// </summary>
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);
        #endregion
    }
}
