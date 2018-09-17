using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace VisualDetection.ViewModel
{
    public class CameraViewModel : ViewModelBase
    {
        public CameraViewModel()
        {
            // Define parameters used to create the BitmapSource.
            System.Windows.Media.PixelFormat pf = PixelFormats.Bgr32;
            int width = 200;
            int height = 200;
            int rawStride = (width * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[rawStride * height];

            // Initialize the image with data.
            Random value = new Random();
            value.NextBytes(rawImage);

            // Create a BitmapSource.
            CurrentFrame = BitmapSource.Create(width, height,
                96, 96, pf, null,
                rawImage, rawStride);
            StartVideo();
        }

        private DispatcherTimer Timer { get; set; }

        private VideoCapture Capture { get; set; }

        private BitmapSource currentFrame;
        public BitmapSource CurrentFrame
        {
            get { return currentFrame; }
            set
            {
                if (currentFrame != value)
                {
                    currentFrame = value;
                    SetProperty(ref currentFrame, value);
                }
            }
        }

        private void StartVideo()
        {
            //CurrentFrame = new BitmapImage(new Uri("C:\\Users\\Johannes\\Pictures\\Camera Roll\\asdf.bmp")) as BitmapSource;
            Capture = new VideoCapture();
            Timer = new DispatcherTimer();
            //framerate of 1fps
            int i = 0;
            Timer.Interval = TimeSpan.FromMilliseconds(1000);
            Timer.Tick += new EventHandler((object s, EventArgs a) =>
            {
                //draw the image obtained from camera
                var frame = Capture.QueryFrame().ToImage<Bgr, Byte>();
                CurrentFrame = ToBitmapSource(frame);
            });
            Timer.Start();
        }

        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap
                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }

        /// <summary>
        /// Delete a GDI object
        /// </summary>
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

    }
}
