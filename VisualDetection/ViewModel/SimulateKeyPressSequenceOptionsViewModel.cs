using System;
using VisualDetection.Interfaces;
using VisualDetection.Util;
using VisualDetection.Model;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Controls;

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
        private ICommand rightSidePlusButtonClicked;
        private ICommand leftSidePlusButtonClicked;
        private ICommand rightSideBackSlashButtonClicked;
        private ICommand leftSideBackSlashButtonClicked;
        private ICommand rightSideClearButtonClicked;
        private ICommand leftSideClearButtonClicked;
        private string rightSideSequence;
        private string leftSideSequence;
        private bool setFocusToRightSideTextBox;
        private bool setFocusToLeftSideTextBox;
        private bool isRightPlus;
        private bool isLeftPlus;
        private uint coolDownTimer;
        private List<string> rightSequenceKeyList;
        private List<string> leftSequenceKeyList;
        #endregion

        #region Public Properties
        /// <summary>
        /// Title of this Option
        /// </summary>
        public string OptionTitle { get; private set; }

        /// <summary>
        /// The + button for the right side was clicked
        /// </summary>
        public ICommand RightSidePlusButtonClicked
        {
            get
            {
                return rightSidePlusButtonClicked ?? (rightSidePlusButtonClicked = new RelayCommand(command => SetPlusFlag(true)));
            }
        }

        /// <summary>
        /// The + button for the right side was clicked
        /// </summary>
        public ICommand LeftSidePlusButtonClicked
        {
            get
            {
                return leftSidePlusButtonClicked ?? (leftSidePlusButtonClicked = new RelayCommand(command => SetPlusFlag(false)));
            }
        }

        /// <summary>
        /// The Backslash button for the right side was clicked
        /// </summary>
        public ICommand RightSideBackSlashButtonClicked
        {
            get
            {
                return rightSideBackSlashButtonClicked ?? (rightSideBackSlashButtonClicked = new RelayCommand(command => SetBackSlashOnTextbox(true)));
            }
        }

        /// <summary>
        /// The Backslash button for the right side was clicked
        /// </summary>
        public ICommand LeftSideBackSlashButtonClicked
        {
            get
            {
                return leftSideBackSlashButtonClicked ?? (leftSideBackSlashButtonClicked = new RelayCommand(command => SetBackSlashOnTextbox(false)));
            }
        }

        /// <summary>
        /// The clear button for the right side was clicked
        /// </summary>
        public ICommand RightSideClearButtonClicked
        {
            get
            {
                return rightSideClearButtonClicked ?? (rightSideClearButtonClicked = new RelayCommand(command => ClearTextbox(true)));
            }
        }

        /// <summary>
        /// The clear button for the right side was clicked
        /// </summary>
        public ICommand LeftSideClearButtonClicked
        {
            get
            {
                return leftSideClearButtonClicked ?? (leftSideClearButtonClicked = new RelayCommand(command => ClearTextbox(false)));
            }
        }

        /// <summary>
        /// The Model handling virtual keys
        /// </summary>
        public SimulatedKeyPressModel KeyModel { get; set; }

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

        /// <summary>
        /// Flag to set Focus to the right side textbox
        /// </summary>
        public bool SetFocusToRightSideTextBox
        {
            get { return setFocusToRightSideTextBox; }
            set
            {
                if (setFocusToRightSideTextBox != value)
                {
                    SetProperty(ref setFocusToRightSideTextBox, value);
                }
            }
        }

        /// <summary>
        /// Flag to set Focus to the left side textbox
        /// </summary>
        public bool SetFocusToLeftSideTextBox
        {
            get { return setFocusToLeftSideTextBox; }
            set
            {
                if (setFocusToLeftSideTextBox != value)
                {
                    SetProperty(ref setFocusToLeftSideTextBox, value);
                }
            }
        }

        /// <summary>
        /// The cooldown timer between two activations
        /// </summary>
        public uint CoolDownTimer
        {
            get { return coolDownTimer; }
            set
            {
                if (coolDownTimer != value)
                {
                    SetProperty(ref coolDownTimer, value);
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
            leftSequenceKeyList = new List<string>();
            CoolDownTimer = GenDefInt.DefaultCoolDownTime;
        }

        /// <summary>
        /// gets the textbox string from the list of keys
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string WriteSequenceFromList(List<string> list)
        {
            string output = "";
            for (int i = 0; i < list.Count; i++)
            {
                output += "[" + list[i].ToString() + "]";
                if (i < list.Count - 1)
                {
                    output += " --> ";
                }
                if (((i + 1) % 5) == 0)
                {
                    output += "\n";
                }
            }
            return output;
        }

        /// <summary>
        /// Handles the List of Keys
        /// </summary>
        /// <param name="keylist"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private List<string> HandleKeyList(List<string> keylist, Key key, bool plusActive)
        {
            var win32Key = KeyInterop.VirtualKeyFromKey(key);
            var keyCodeName = Enum.GetName(typeof(KeyCode), win32Key);
            if (keyCodeName != null)
            {
                if (!plusActive)
                {
                    keylist.Add(keyCodeName);
                }
                else
                {
                    keylist[keylist.Count - 1] += " + " + keyCodeName;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("This key is currently not supported. ");
            }
            return keylist;
        }

        /// <summary>
        /// sets the isPlus flag in case the respective
        /// + button is pressed. 
        /// </summary>
        private void SetPlusFlag(bool isRight)
        {
            if (isRight)
            {
                if (rightSequenceKeyList.Count > 0)
                {
                    isRightPlus = true;
                }
                SetFocusToRightSideTextBox = true;
            }
            else
            {
                if (leftSequenceKeyList.Count > 0)
                {
                    isLeftPlus = true;
                }
                SetFocusToLeftSideTextBox = true;
            }
        }

        /// <summary>
        /// deletes the last key from the respective textbox
        /// </summary>
        /// <param name="isRight"></param>
        private void SetBackSlashOnTextbox(bool isRight)
        {
            if(isRight)
            {
                isRightPlus = false;
                rightSequenceKeyList.RemoveAt(rightSequenceKeyList.Count - 1);
                RightSideSequence = WriteSequenceFromList(rightSequenceKeyList);
                SetFocusToRightSideTextBox = true;
            }
            else
            {
                isLeftPlus = false;
                leftSequenceKeyList.RemoveAt(leftSequenceKeyList.Count - 1);
                LeftSideSequence = WriteSequenceFromList(leftSequenceKeyList);
                SetFocusToLeftSideTextBox = true;
            }
        }

        /// <summary>
        /// deletes the complete sequence from the corresponding textbox
        /// </summary>
        /// <param name="isRight"></param>
        private void ClearTextbox(bool isRight)
        {
            if (isRight)
            {
                isRightPlus = false;
                rightSequenceKeyList.Clear();
                RightSideSequence = "";
                SetFocusToRightSideTextBox = true;
            }
            else
            {
                isLeftPlus = false;
                leftSequenceKeyList.Clear();
                LeftSideSequence = "";
                SetFocusToLeftSideTextBox = true;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Clears the Textbox on initial
        /// </summary>
        public void GotFocusSetup(object source, EventArgs e)
        {
            var textbox = source as TextBox;

            switch (textbox.Name)
            {
                case "RightSideTextbox":
                    if (RightSideSequence == GenDefString.KeyPressSequenceDefault)
                    {
                        RightSideSequence = "";
                    }
                    break;
                case "LeftSideTextbox":
                    if (LeftSideSequence == GenDefString.KeyPressSequenceDefault)
                    {
                        LeftSideSequence = "";
                    }
                    break;
            }
        }

        /// <summary>
        /// Handles the loss of focus for the respective textbox
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void TextboxLostFocus(object source, EventArgs e)
        {
            var textbox = source as TextBox;

            switch (textbox.Name)
            {
                case "RightSideTextbox":
                    SetFocusToRightSideTextBox = false;
                    if(rightSequenceKeyList.Count == 0)
                    {
                        RightSideSequence = GenDefString.KeyPressSequenceDefault;
                    }
                    break;
                case "LeftSideTextbox":
                    SetFocusToLeftSideTextBox = false;
                    if (leftSequenceKeyList.Count == 0)
                    {
                        LeftSideSequence = GenDefString.KeyPressSequenceDefault;
                    }
                    break;
            }
        }

        /// <summary>
        /// Add a Key to the sequence for the right side
        /// </summary>
        public void AddKey(object source, KeyEventArgs e)
        {
            var textbox = source as TextBox;

            switch (textbox.Name)
            {
                case "RightSideTextbox":
                    RightSideSequence = "";
                    HandleKeyList(rightSequenceKeyList, e.Key, isRightPlus);
                    RightSideSequence = WriteSequenceFromList(rightSequenceKeyList);
                    isRightPlus = false;
                    break;
                case "LeftSideTextbox":
                    LeftSideSequence = "";
                    HandleKeyList(leftSequenceKeyList, e.Key, isLeftPlus);
                    LeftSideSequence = WriteSequenceFromList(leftSequenceKeyList);
                    isLeftPlus = false;
                    break;
            }


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
