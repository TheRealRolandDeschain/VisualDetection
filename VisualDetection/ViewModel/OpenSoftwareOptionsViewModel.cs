using System;
using VisualDetection.Util;
using VisualDetection.Interfaces;
using VisualDetection.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows;
using System.Linq;

namespace VisualDetection.ViewModel
{
    public class OpenSoftwareOptionsViewModel : ViewModelBase, IOutputOption
    {
        #region Constructor
        public OpenSoftwareOptionsViewModel()
        {
            SetDefaultValues();
        }
        #endregion

        #region Private Properties
        private int indexOfSelectedProcess;
        private ICommand addProcess;
        private ICommand deleteProcess;
        private ICommand clearAllProcesses;
        private Stopwatch coolDownTimerStopWatch;
        private uint coolDownTimer;
        #endregion

        #region Public Properties

        /// <summary>
        /// The index of the currently selected index
        /// </summary>
        public ObservableCollection<ProcessModel> ListOfProcesses { get; set; }

        /// <summary>
        /// The Add button for was clicked
        /// </summary>
        public ICommand AddProcess
        {
            get
            {
                return addProcess ?? (addProcess = new RelayCommand(command => AddProcessToList()));
            }
        }

        /// <summary>
        /// The Add button for was clicked
        /// </summary>
        public ICommand DeleteProcess
        {
            get
            {
                return deleteProcess ?? (deleteProcess = new RelayCommand(command => DeleteProcessFromList()));
            }
        }

        /// <summary>
        /// The Add button for was clicked
        /// </summary>
        public ICommand ClearAllProcesses
        {
            get
            {
                return clearAllProcesses ?? (clearAllProcesses = new RelayCommand(command => ClearAllProcessesFromList()));
            }
        }

        /// <summary>
        /// Title of this Option
        /// </summary>
        public string OptionTitle { get; private set; }

        /// <summary>
        /// The index of the currently selected index
        /// </summary>
        public int IndexOfSelectedProcess
        {
            get { return indexOfSelectedProcess; }
            set
            {
                if (indexOfSelectedProcess != value)
                {
                    SetProperty(ref indexOfSelectedProcess, value);
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
            OptionTitle = GenDefString.OpenExternalSoftwareTitle;
            ListOfProcesses = new ObservableCollection<ProcessModel>();
            CoolDownTimer = GenDefInt.DefaultCoolDownTime;
            coolDownTimerStopWatch = new Stopwatch();
        }

        /// <summary>
        /// Opens a dialog for the user to choose a new programm, 
        /// creates the process and adds it to the list 
        /// </summary>
        private void AddProcessToList()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = GenDefString.OpenFileDialogDefaultTitle,
                Multiselect = false,
                InitialDirectory = GenDefString.OpenFileDialogInitialDirectory,
                CheckFileExists = true,
                CheckPathExists = true
            };
            if (ofd.ShowDialog() == true)
            {
                if (!String.IsNullOrEmpty(ofd.FileName))
                {
                    if (ListOfProcesses.Any(p => p.ProcessPath == ofd.FileName))
                    {
                        MessageBox.Show(GenDefString.ProcessAlreadyInList);
                    }
                    else
                    {
                        ListOfProcesses.Add(new ProcessModel(ofd.FileName));
                    }
                }
                IndexOfSelectedProcess = ListOfProcesses.Count - 1;
            }
        }

        /// <summary>
        /// Deletes the currently selected Process from teh list
        /// </summary>
        private void DeleteProcessFromList()
        {
            ListOfProcesses.RemoveAt(IndexOfSelectedProcess);
        }

        /// <summary>
        /// Clears the list of processes
        /// </summary>
        private void ClearAllProcessesFromList()
        {
            ListOfProcesses.Clear();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Trigger Status changed Event was raised
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnTriggerStatusChanged(object source, EventArgs e)
        {
            var triggers = (source as OutputViewModel);

            if (coolDownTimerStopWatch.IsRunning && coolDownTimerStopWatch.ElapsedMilliseconds < CoolDownTimer) return;

            foreach (var process in ListOfProcesses)
            {
                if ((triggers.RightTriggerActive && process.IsTriggeredRight) ||
                    (triggers.LeftTriggerActive && process.IsTriggeredLeft))
                {
                    process.StartNewInstance();
                }
                else if (process.IsTriggeredOpenClose)
                {
                    if (triggers.LeftTriggerActive)
                    {
                        process.StartNewInstance();
                    }
                    else if (triggers.RightTriggerActive)
                    {
                        process.StopLastInstance();
                    }
                }
            }
            coolDownTimerStopWatch.Restart();
        }
        #endregion
    }
}
