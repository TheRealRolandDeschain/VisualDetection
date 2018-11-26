using System;
using VisualDetection.Interfaces;
using VisualDetection.Util;
using VisualDetection.Model;
using System.Windows.Input;
using System.Collections.Generic;

namespace VisualDetection.ViewModel
{
    public class SimulateKeyPressSequenceOptionsViewModel : ViewModelBase, IOutputOption
    {
        #region Constructor
        public SimulateKeyPressSequenceOptionsViewModel()
        {
            SetDefaultValues();
        }
        #endregion

        #region Private Properties
        private ICommand rightSideGotFocus;
        private string rightSideSequence;
        private string leftSideSequence;
        private List<string> rightSequenceKeyList;
        #endregion

        #region Public Properties
        /// <summary>
        /// Title of this Option
        /// </summary>
        public string OptionTitle { get; private set; }

        /// <summary>
        /// The Model handling virtual keys
        /// </summary>
        public SimulatedKeyPressModel KeyModel { get; set; }

        /// <summary>
        /// KeyDown triggered command for the right side
        /// </summary>
        public ICommand RightSideGotFocus
        {
            get
            {
                return rightSideGotFocus ?? (rightSideGotFocus = new RelayCommand(command => RightSideGotFocusSetup()));
            }
        }

        /// <summary>
        /// The sequence for the right side
        /// </summary>
        public string RightSideSequence
        {
            get { return rightSideSequence; }
            set
            {
                if (rightSideSequence != value)
                {
                    SetProperty(ref rightSideSequence, value);
                }
            }
        }

        /// <summary>
        /// The sequence for the left side
        /// </summary>
        public string LeftSideSequence
        {
            get { return leftSideSequence; }
            set
            {
                if (leftSideSequence != value)
                {
                    SetProperty(ref leftSideSequence, value);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the default values
        /// </summary>
        private void SetDefaultValues()
        {
            OptionTitle = GenDefString.SimulateKeyPressSequenceTitle;
            KeyModel = new SimulatedKeyPressModel();
            RightSideSequence = GenDefString.KeyPressSequenceDefault;
            LeftSideSequence = GenDefString.KeyPressSequenceDefault;
            rightSequenceKeyList = new List<string>();
        }

        /// <summary>
        /// Clears the Textbox on initial
        /// </summary>
        private void RightSideGotFocusSetup()
        {
            if (RightSideSequence == GenDefString.KeyPressSequenceDefault)
            {
                RightSideSequence = "";
            }
        }



        private string WriteSequenceFromList(List<string> list)
        {
            string output = "";
            uint i = 1;
            foreach (string key in list)
            {
                output += "[" + key.ToString() + "] ";
                if ((i % 5) == 0) output += "\n";
                i++;
            }
            return output;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add a Key to the sequence for the right side
        /// </summary>
        public void RightSideAddKey(object source, KeyEventArgs e)
        {
            RightSideSequence = "";
            var win32Key = KeyInterop.VirtualKeyFromKey(e.Key);
            var keyCodeName = Enum.GetName(typeof(KeyCode), win32Key);
            if (keyCodeName != null)
            {
                rightSequenceKeyList.Add(keyCodeName);
            }
            else
            {
                System.Windows.MessageBox.Show("This key is currently not supported. ");
            }
            RightSideSequence = WriteSequenceFromList(rightSequenceKeyList);
        }

        /// <summary>
        /// Add a Key to the sequence for the left side
        /// </summary>
        private void LeftSideAddKey(object source, EventArgs e)
        {

        }

        /// <summary>
        /// Trigger Status changed Event was raised
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnTriggerOnTriggerStatusChanged(object source, EventArgs e)
        {
        }
        #endregion
    }
}
