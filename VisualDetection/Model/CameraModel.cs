using Emgu.CV;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using VisualDetection.ViewModel;

namespace VisualDetection.Model
{
    public sealed class CameraModel
    {
        #region Constructors
        /// <summary>
        /// default constructor
        /// </summary>
        private CameraModel()
        {
            CameraViewMat = new Mat();
            CameraViewGrayScaleMat = new Mat();
        }
        #endregion

        #region Singleton
        private static volatile CameraModel instance;
        private static object syncRoot = new object();
        /// <summary>
        /// threadsave singleton
        /// </summary>
        public static CameraModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CameraModel();
                    }
                }

                return instance;
            }
        }
        #endregion

        #region Private Properties
        #endregion

        #region Public Properties
        public Mat CameraViewMat { get; set; }
        public Mat CameraViewGrayScaleMat { get; set; }
        public CascadeDetectorOptionsViewModel cascadeOptions { get; set; }
        public GeneralOptionsViewModel generalOptions { get; set; }
        public OutputViewModel output { get; set; }
        public OutputOptionsViewModel outputOptions { get; set; }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion
    }
}
