using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VisualDetection.Util;

namespace VisualDetection.ViewModel
{
    public class CascadeDetectorOptionsViewModel : ViewModelBase
    {
        #region Constructors
        /// <summary>
        /// standard constructor
        /// </summary>
        public CascadeDetectorOptionsViewModel()
        {
            HaarCascadeFacePathString = GenDefString.HaarCascadePathStringEmpty;
            HaarCascadeEyePathString = GenDefString.HaarCascadePathStringEmpty;
        }
        #endregion

        #region Private Properties
        private RelayCommand loadHaarCascadeFaceClicked;
        private RelayCommand loadHaarCascadeEyeClicked;
        private string haarCascadeFacePathString;
        private string haarCascadeEyePathString;
        #endregion

        #region Public Properties
        /// <summary>
        /// The button to load a HaarCascade for face detection was clicked
        /// </summary>
        public ICommand LoadHaarCascadeFaceClicked
        {
            get
            {
                return loadHaarCascadeFaceClicked ?? (loadHaarCascadeFaceClicked = new RelayCommand(command => LoadHaarCascadeFace()));
            }
        }

        /// <summary>
        /// The button to load a HaarCascade for face detection was clicked
        /// </summary>
        public ICommand LoadHaarCascadeEyeClicked
        {
            get
            {
                return loadHaarCascadeEyeClicked ?? (loadHaarCascadeEyeClicked = new RelayCommand(command => LoadHaarCascadeEye()));
            }
        }

        /// <summary>
        /// The content of the textbox for the path of face detection cascade
        /// </summary>
        public string HaarCascadeFacePathString
        {
            get { return haarCascadeFacePathString; }
            set
            {
                if (haarCascadeFacePathString != value)
                {
                    SetProperty(ref haarCascadeFacePathString, value);
                }
            }
        }

        /// <summary>
        /// The content of the textbox for the path of eye detection cascade
        /// </summary>
        public string HaarCascadeEyePathString
        {
            get { return haarCascadeEyePathString; }
            set
            {
                if (haarCascadeEyePathString != value)
                {
                    SetProperty(ref haarCascadeEyePathString, value);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Load a XML file for face detection
        /// </summary>
        private void LoadHaarCascadeFace()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select a valid XML file!";
            ofd.DefaultExt = "xml";
            ofd.Filter = "XML Files|*.xml";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == true)
            {
                HaarCascadeFacePathString = Path.GetFullPath(ofd.FileName);
            }
        }

        /// <summary>
        /// Load a XML file for eye detection
        /// </summary>
        private void LoadHaarCascadeEye()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select a valid XML file!";
            ofd.DefaultExt = "xml";
            ofd.Filter = "XML Files|*.xml";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == true)
            {
                HaarCascadeEyePathString = Path.GetFullPath(ofd.FileName);
            }
        }
        #endregion

        #region Public Methods
        #endregion

    }
}
