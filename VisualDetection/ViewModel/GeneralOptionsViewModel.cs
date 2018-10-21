using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        #endregion

        #region Public Prooperties
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
        #endregion

        #region Private Methods
        public void GetAvailableCameraList()
        {
            AvailableCameras = new List<string> { "Camera 1", "Camera 2", "Camera 3", "Camera 4", "Camera 5", "Camera 6", "Camera 7", "Camera 8", "Camera 9" };
            SelectedCameraIndex = 0;
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
