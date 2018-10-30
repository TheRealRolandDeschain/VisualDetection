using System;
using System.Collections.Generic;
using System.Management;
using VisualDetection.Model;


namespace VisualDetection.ViewModel
{
    public class GeneralOptionsViewModel : ViewModelBase
    {
        #region Constructors
        public GeneralOptionsViewModel()
        {
            GetAvailableCameraList();
            DetectorTypeList = new List<string>() { "Cascade Detector" };
            SelectedDetectorTypeIndex = 0;
        }

        #endregion

        #region Singleton
        private static volatile GeneralOptionsViewModel instance;
        private static object syncRoot = new object();
        /// <summary>
        /// threadsave singleton
        /// </summary>
        public static GeneralOptionsViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GeneralOptionsViewModel();
                    }
                }

                return instance;
            }
        }

        #endregion
        #region Private Properties
        private int selectedCameraIndex;
        private int idleAfterFrameCalculationMS;
        private int selectedDetectorTypeIndex;
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
                selectedCameraIndex = value;
                SetProperty(ref selectedCameraIndex, value);
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
                selectedDetectorTypeIndex = value;
                SetProperty(ref selectedDetectorTypeIndex, value);
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
                idleAfterFrameCalculationMS = value;
                SetProperty(ref idleAfterFrameCalculationMS, value);
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
        #endregion

        #region Public Methods
        #endregion
    }
}
