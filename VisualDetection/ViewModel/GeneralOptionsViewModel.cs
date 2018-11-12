using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Media;
using Emgu.CV.Structure;
using VisualDetection.Model;


namespace VisualDetection.ViewModel
{
    public class GeneralOptionsViewModel : ViewModelBase
    {
        #region Constructors
        public GeneralOptionsViewModel()
        {
            LoadDefaultValues();
            CameraModel.Instance.generalOptions = this;
        }

        #endregion

        #region Private Properties
        private int selectedCameraIndex;
        private int idleAfterFrameCalculationMS;
        private int selectedDetectorTypeIndex;
        private Color faceRectColor;
        private Color eyesRectColor;
        #endregion

        #region Public Fields
        /// <summary>
        /// Actually used Color Format for calculation
        /// </summary>
        public MCvScalar FaceRectColorScalar;
        public MCvScalar EyesRectColorScalar;
        public bool UseEqualizeHist { get; set; }
        #endregion

        #region Public Prooperties
        /// <summary>
        /// The list of all available cameras on the system. 
        /// </summary>
        public List<string> AvailableCameras { get; set; }

        /// <summary>
        /// The list of available detector types 
        /// </summary>
        public List<string> DetectorTypeList { get; set; }

        /// <summary>
        /// The index of the currently selected camera
        /// </summary>
        public int SelectedCameraIndex
        {
            get
            {
                return selectedCameraIndex;
            }
            set
            {
                if (selectedCameraIndex != value)
                {
                    selectedCameraIndex = value;
                    SetProperty(ref selectedCameraIndex, value);
                }
            }
        }

        /// <summary>
        /// The index of the currently selected detector type
        /// </summary>
        public int SelectedDetectorTypeIndex
        {
            get
            {
                return selectedDetectorTypeIndex;
            }
            set
            {
                if (selectedDetectorTypeIndex != value)
                {
                    selectedDetectorTypeIndex = value;
                    SetProperty(ref selectedDetectorTypeIndex, value);
                }
            }
        }

        /// <summary>
        /// Time the programm waits after each frame
        /// </summary>
        public int IdleAfterFrameCalculationMS
        {
            get
            {
                return idleAfterFrameCalculationMS;
            }
            set
            {
                if (idleAfterFrameCalculationMS != value)
                {
                    idleAfterFrameCalculationMS = value;
                    SetProperty(ref idleAfterFrameCalculationMS, value);
                }
            }
        }

        /// <summary>
        /// The color for the detected face
        /// </summary>
        public Color FaceRectColor
        {
            get
            {
                return faceRectColor;
            }
            set
            {
                if (faceRectColor != value)
                {
                    faceRectColor = value;
                    FaceRectColorScalar = new MCvScalar(value.B, value.G, value.R, value.A);
                    SetProperty(ref faceRectColor, value);
                }
            }
        }

        /// <summary>
        /// The color for the detected eyes
        /// </summary>
        public Color EyesRectColor
        {
            get
            {
                return eyesRectColor;
            }
            set
            {
                if (eyesRectColor != value)
                {
                    eyesRectColor = value;
                    EyesRectColorScalar = new MCvScalar(value.B, value.G, value.R, value.A);
                    SetProperty(ref eyesRectColor, value);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// uses WMI to get all available cameras and add them to the list
        /// </summary>
        private void GetAvailableCameraList()
        {
            AvailableCameras = new List<string>();
            string wmiQuery = string.Format("SELECT * FROM Win32_PnPSignedDriver");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);
            ManagementObjectCollection retObjectCollection = searcher.Get();

            foreach (var WmiObject in retObjectCollection)
            {
                    if (WmiObject["DeviceClass"] != null && WmiObject["DeviceClass"].ToString().Equals("IMAGE"))
                    {
                        AvailableCameras.Add(WmiObject["DeviceName"].ToString());
                    }
            }
            SelectedCameraIndex = 0;
        }

        /// <summary>
        /// Loads the default values for this class
        /// </summary>
        private void LoadDefaultValues()
        {
            GetAvailableCameraList();
            DetectorTypeList = new List<string>() { "Cascade Detector" };
            SelectedDetectorTypeIndex = 0;
            FaceRectColor = Colors.Red;
            EyesRectColor = Colors.Blue;
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
