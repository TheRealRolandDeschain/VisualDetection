using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using VisualDetection.Util;
using VisualDetection.Model;
using Emgu.CV;

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
            face = new CascadeClassifier();
            eye = new CascadeClassifier();
            CameraModel.Instance.cascadeOptions = this;
        }
        #endregion

        #region Private Properties
        private RelayCommand loadHaarCascadeFaceClicked;
        private RelayCommand loadHaarCascadeEyeClicked;
        private string haarCascadeFacePathString;
        private string haarCascadeEyePathString;
        private bool validFaceClassifierLoaded;
        private bool validEyeClassifierLoaded;
        #endregion

        #region Public Properties
        public CascadeClassifier face { get; set; }
        public CascadeClassifier eye { get; set; }


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
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = GenDefString.HaarCascadeOfdTitle,
                DefaultExt = GenDefString.HaarCascadeOfdDefaultExt,
                Filter = GenDefString.HaarCascadeOfdFilter,
                Multiselect = false
            };
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    HaarCascadeFacePathString = Path.GetFullPath(ofd.FileName);
                    FileStorage fs = new FileStorage(HaarCascadeFacePathString, FileStorage.Mode.Read);
                    FileNode fn = fs.GetFirstTopLevelNode();
                    validFaceClassifierLoaded = face.Read(fn);
                }
                catch (Exception e)
                {
                    HaarCascadeFacePathString = GenDefString.HaarCascadePathStringEmpty;
                    validFaceClassifierLoaded = false;
                    MessageBox.Show(GenDefString.InvalidCascadeClassifierXMLLoaded + "\n error: " + e.Message);
                }
            }
        }

        /// <summary>
        /// Load a XML file for eye detection
        /// </summary>
        private void LoadHaarCascadeEye()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = GenDefString.HaarCascadeOfdTitle,
                DefaultExt = GenDefString.HaarCascadeOfdDefaultExt,
                Filter = GenDefString.HaarCascadeOfdFilter,
                Multiselect = false
            };
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    HaarCascadeEyePathString = Path.GetFullPath(ofd.FileName);
                    FileStorage fs = new FileStorage(HaarCascadeEyePathString, FileStorage.Mode.Read);
                    FileNode fn = fs.GetFirstTopLevelNode();
                    validEyeClassifierLoaded = face.Read(fn);
                }
                catch (Exception e)
                {
                    HaarCascadeEyePathString = GenDefString.HaarCascadePathStringEmpty;
                    validEyeClassifierLoaded = false;
                    MessageBox.Show(GenDefString.InvalidCascadeClassifierXMLLoaded + "\n error: " + e.Message);
                }
            }
        }
        #endregion

        #region Public Methods
        #endregion

    }
}
