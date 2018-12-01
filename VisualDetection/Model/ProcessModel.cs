using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            processPath = path;
            instances = new List<Process>();
            NrOfInstancesRunning = 3;
        }
        #endregion

        #region Private Properties
        private string processName;
        private uint nrOfInstancesRunning;
        private string processPath;
        private List<Process> instances;
        private bool isTriggeredLeft;
        private bool isTriggeredRight;
        private bool isTriggeredOpenClose;
        #endregion

        #region Public Properties
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
        public uint NrOfInstancesRunning
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
                    SetProperty(ref isTriggeredOpenClose, value);
                }
            }
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion
    }
}
