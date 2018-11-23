using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VisualDetection.ViewModel;
using Timer = System.Timers.Timer;

namespace VisualDetection.Model
{
    public class SimulatedMouseModel
    {
        #region Constructor
        public SimulatedMouseModel(SimulateMouseButtonsOptionsViewModel SimulateMouseButtonsOptionsViewModelIn)
        {
            buttonPressTimerLeft = new Timer();
            buttonPressTimerLeft.Elapsed += OnLeftTimmerTicked;
            coolDownTimerLeft = new Stopwatch();
            coolDownTimerLeft.Start();
            buttonPressTimerRight = new Timer();
            buttonPressTimerRight.Elapsed += OnRightTimmerTicked;
            coolDownTimerRight = new Stopwatch();
            coolDownTimerRight.Start();
            buttonPressTimerMiddle = new Timer();
            buttonPressTimerMiddle.Elapsed += OnMiddleTimmerTicked;
            coolDownTimerMiddle = new Stopwatch();
            coolDownTimerMiddle.Start();
            buttonPressTimerWheel = new Timer();
            buttonPressTimerWheel.Elapsed += OnWheelTimmerTicked;
            coolDownTimerWheel = new Stopwatch();
            coolDownTimerWheel.Start();
            simulatedMBViewModel = SimulateMouseButtonsOptionsViewModelIn;
        }
        #endregion

        #region Private Properties
        private SimulateMouseButtonsOptionsViewModel simulatedMBViewModel;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_WHEEL = 0x0800;
        private Timer buttonPressTimerLeft;
        private Stopwatch coolDownTimerLeft;
        private Timer buttonPressTimerRight;
        private Stopwatch coolDownTimerRight;
        private Timer buttonPressTimerMiddle;
        private Stopwatch coolDownTimerMiddle;
        private Timer buttonPressTimerWheel;
        private Stopwatch coolDownTimerWheel;
        private bool currentMouseWheelDirection;
        #endregion

        #region Public Properties
        public bool RightButtonPressed { get; set; } = false;
        public bool LeftButtonPressed { get; set; } = false;
        public bool MiddleMouseButtonPressed { get; set; } = false;
        public int CoolDownTime { get; set; }

        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the timers after a option has changed
        /// </summary>
        public void UpdateTimers()
        {
            //Checking if 0 is a quick and dirty fix, should be changed!
            var buttonPressTime = (simulatedMBViewModel.ButtonPressTime != 0) ? simulatedMBViewModel.ButtonPressTime : 1;
            var mouseWheelSpeed = (simulatedMBViewModel.MouseWheelSpeedTime != 0) ? simulatedMBViewModel.MouseWheelSpeedTime : 1;
            buttonPressTimerLeft.Interval = buttonPressTime;
            buttonPressTimerRight.Interval = buttonPressTime;
            buttonPressTimerMiddle.Interval = buttonPressTime;
            buttonPressTimerWheel.Interval = mouseWheelSpeed;
        }

        /// <summary>
        /// Method that releases the virtual mouse buttons
        /// </summary>
        /// <param name="releaseLeft"></param>
        /// <param name="releasRight"></param>
        /// <param name="releaseMiddle"></param>
        public void ReleaseMouseButtons(bool releaseLeft, bool releasRight, bool releaseMiddle)
        {
            uint MouseButtons = 0;
            if (releaseLeft && LeftButtonPressed)
            {
                MouseButtons |= MOUSEEVENTF_LEFTUP;
                LeftButtonPressed = false;
                coolDownTimerLeft.Restart();
                buttonPressTimerLeft.Stop();
            }
            if (releasRight && RightButtonPressed)
            {
                MouseButtons |= MOUSEEVENTF_RIGHTUP;
                RightButtonPressed = false;
                coolDownTimerRight.Restart();
                buttonPressTimerRight.Stop();
            }
            if (releaseMiddle && MiddleMouseButtonPressed)
            {
                MouseButtons |= MOUSEEVENTF_MIDDLEUP;
                MiddleMouseButtonPressed = false;
                coolDownTimerMiddle.Restart();
                buttonPressTimerMiddle.Stop();
            }
            if (MouseButtons != 0)
            {
                mouse_event(MouseButtons, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
            }
        }

        /// <summary>
        /// Method that presses the virtual mouse buttons
        /// </summary>
        /// <param name="pressLeft"></param>
        /// <param name="pressRight"></param>
        /// <param name="pressMiddle"></param>
        public void PressMouseButtons(bool pressLeft, bool pressRight, bool pressMiddle)
        {
            var toggleSwitch = simulatedMBViewModel.UseAsToggleSwitchEnabled;
            var setCoolDown = simulatedMBViewModel.CoolDownTime;
            uint MouseButtons = 0;
            if (pressLeft && !LeftButtonPressed && (coolDownTimerLeft.ElapsedMilliseconds >= setCoolDown))
            {
                MouseButtons |= MOUSEEVENTF_LEFTDOWN;
                LeftButtonPressed = true;
            }
            if (pressRight && !RightButtonPressed && (coolDownTimerRight.ElapsedMilliseconds >= setCoolDown))
            {
                MouseButtons |= MOUSEEVENTF_RIGHTDOWN;
                RightButtonPressed = true;
            }
            if (pressMiddle && !MiddleMouseButtonPressed && (coolDownTimerMiddle.ElapsedMilliseconds >= setCoolDown))
            {
                MouseButtons |= MOUSEEVENTF_MIDDLEDOWN;
                MiddleMouseButtonPressed = true;
            }
            if (MouseButtons != 0)
            {
                mouse_event(MouseButtons, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
            }

            if (toggleSwitch)
            {
                ReleaseMouseButtons(!pressLeft, !pressRight, !pressMiddle);
            }
            else if (pressLeft)
            {
                buttonPressTimerLeft.Start();
            }
            else if (pressRight)
            {
                buttonPressTimerRight.Start();
            }
            else if (pressMiddle)
            {
                buttonPressTimerMiddle.Start();
            }
        }

        /// <summary>
        /// Method that releases the virtual mouse buttons
        /// </summary>
        /// <param name="turnUpdown">specifies the direction true = up, false = down</param>
        /// <param name="turnMovement">specifies the indentes of the move</param>
        public void TurnMouseWheel(bool turnUpdown)
        {
            currentMouseWheelDirection = turnUpdown;
            var turnMovement = simulatedMBViewModel.MouseWheelIndent;
            var toggle = simulatedMBViewModel.UseAsToggleSwitchEnabled;
            var setCoolDown = simulatedMBViewModel.CoolDownTime;
            if (toggle || coolDownTimerWheel.ElapsedMilliseconds > setCoolDown)
            {
                int turns = turnMovement * 120;
                if (!currentMouseWheelDirection) turns *= -1;
                mouse_event(MOUSEEVENTF_WHEEL, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, turns, 0);
            }
            if (toggle)
            {
                buttonPressTimerWheel.Start();
                coolDownTimerWheel.Stop();
            }
            else
            {
                coolDownTimerWheel.Restart();
            }
        }

        /// <summary>
        /// Stops the recoursive spinning of the wheel
        /// </summary>
        public void StopMouseWheel()
        {
            buttonPressTimerWheel.Stop();
            coolDownTimerWheel.Restart();
        }



        /// <summary>
        /// Elapsed left button press time
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnLeftTimmerTicked(object source, EventArgs e)
        {
            ReleaseMouseButtons(true, false, false);
        }

        /// <summary>
        /// Elapsed right button press time
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnRightTimmerTicked(object source, EventArgs e)
        {
            ReleaseMouseButtons(false, true, false);
        }

        /// <summary>
        /// Elapsed Middle button press time
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnMiddleTimmerTicked(object source, EventArgs e)
        {
            ReleaseMouseButtons(false, false, true);
        }

        /// <summary>
        /// Elapsed wheel turn time
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnWheelTimmerTicked(object source, EventArgs e)
        {
            TurnMouseWheel(currentMouseWheelDirection);
        }


        #endregion

        #region Setup for MouseClick Simulation
        // Taken from https://stackoverflow.com/questions/2416748/how-do-you-simulate-mouse-click-in-c
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, int cButtons, uint dwExtraInfo);
        #endregion
    }
}
