using System;
using System.Collections.Generic;
using System.Management;


namespace VisualDetection.ViewModel
{
    public class GeneralOptionsViewModel : ViewModelBase
    {
        #region Constructors
        public GeneralOptionsViewModel()
        {
            GetAvailableCameraList();
        }

        #endregion

        #region Private Properties
        private List<string> availableCameras;
        private int selectedCameraIndex;
        private int idleAfterFrameCalculationMS;
        #endregion

        #region Public Prooperties
        /// <summary>
        /// The list of all available cameras on the system. 
        /// </summary>
        public List<string> AvailableCameras
        {
            get
            {
                return availableCameras;
            }
            set
            {
                availableCameras = value;
            }
        }

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
        public void GetAvailableCameraList()
        {
            AvailableCameras = new List<string>();
            string wmiQuery = string.Format("SELECT * FROM Win32_PnPSignedDriver");

            Console.WriteLine("Query: {0}", wmiQuery);

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
