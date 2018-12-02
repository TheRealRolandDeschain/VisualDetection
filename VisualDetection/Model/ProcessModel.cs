using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using VisualDetection.Util;
using VisualDetection.ViewModel;

namespace VisualDetection.Model
{
    public class ProcessModel : ViewModelBase
    {
        #region Constructor
        //Creates a new process via a path
        public ProcessModel(string path)
        {
            ProcessName = Path.GetFileNameWithoutExtension(path);
            processExtension = Path.GetExtension(path);
            ProcessPath = path;
            instances = new List<Process>();
        }
        #endregion

        #region Private Properties
        private string processName;
        private string processExtension;
        private int nrOfInstancesRunning;
        private List<Process> instances;
        private bool isTriggeredLeft;
        private bool isTriggeredRight;
        private bool isTriggeredOpenClose;
        #endregion

        #region Public Properties
        /// <summary>
        /// The path of the process
        /// </summary>
        public string ProcessPath { get; set; }

        /// <summary>
        /// The name of this Process
        /// </summary>
        public string ProcessName
        {
            get { return processName; }
            set
            {
                if (processName != value)
                {
                    SetProperty(ref processName, value);
                }
            }
        }

        /// <summary>
        /// The number of instances of the current process
        /// that are running
        /// </summary>
        public int NrOfInstancesRunning
        {
            get { return nrOfInstancesRunning; }
            set
            {
                if (nrOfInstancesRunning != value)
                {
                    SetProperty(ref nrOfInstancesRunning, value);
                }
            }
        }

        /// <summary>
        /// Determines if the process is triggerd by the left trigger
        /// that are running
        /// </summary>
        public bool IsTriggeredLeft
        {
            get { return isTriggeredLeft; }
            set
            {
                if (isTriggeredLeft != value)
                {
                    IsTriggeredOpenClose = false;
                    SetProperty(ref isTriggeredLeft, value);
                }
            }
        }

        /// <summary>
        /// Determines if the process is triggerd by the right trigger
        /// that are running
        /// </summary>
        public bool IsTriggeredRight
        {
            get { return isTriggeredRight; }
            set
            {
                if (isTriggeredRight != value)
                {
                    IsTriggeredOpenClose = false;
                    SetProperty(ref isTriggeredRight, value);
                }
            }
        }

        /// <summary>
        /// Determines if the process is triggerd so that it 
        /// opens with the left and closes with the right trigger
        /// that are running
        /// </summary>
        public bool IsTriggeredOpenClose
        {
            get { return isTriggeredOpenClose; }
            set
            {
                if (isTriggeredOpenClose != value)
                {
                    IsTriggeredLeft = false;
                    IsTriggeredRight = false;
                    SetProperty(ref isTriggeredOpenClose, value);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the value for the number of instances (used for GUI update)
        /// </summary>
        private void UpdateNrOfInstances()
        {
            NrOfInstancesRunning = instances.Count;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts a new instance of the current process
        /// </summary>
        public void StartNewInstance()
        {
            try
            {
                Process newInstance;
                if (processExtension == GenDefString.ExecutableExtension)
                {
                    newInstance = new Process();
                    newInstance.StartInfo.UseShellExecute = false;
                    newInstance.StartInfo.FileName = ProcessPath;
                    newInstance.Start();
                }
                else
                {
                    newInstance = Process.Start(ProcessPath);
                }
                newInstance.EnableRaisingEvents = true;
                newInstance.Exited += OnProcessExited;
                instances.Add(newInstance);
                UpdateNrOfInstances();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// stops the last instance int he list
        /// </summary>
        public void StopLastInstance()
        {
            if (instances.Count < 1) return;
            try
            {
                var instanceToDelete = instances.Last();
                instanceToDelete.Exited -= OnProcessExited;
                instanceToDelete.CloseMainWindow();
                instanceToDelete.Close();
                instances.RemoveAt(instances.Count - 1);
                UpdateNrOfInstances();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Trigger Status changed Event was raised
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnProcessExited(object source, EventArgs e)
        {
            var process = source as Process;
            if (process == null) return;
            instances.Remove(process);
            UpdateNrOfInstances();
        }
        #endregion
    }
}
