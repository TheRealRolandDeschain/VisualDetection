using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VisualDetection.Util;

namespace VisualDetection.Model
{
    public class SimulatedKeyPressModel
    {

        /// <summary>
        /// Returns the corresponding KeyCode from the string
        /// </summary>
        /// <returns></returns>
        public static List<KeyCode> GetKeyCodeFromString(string keys)
        {
            var delimiter = new string[] { " + " };
            var subkeys = keys.Split(delimiter, StringSplitOptions.None);
            List<KeyCode> keycodelist = new List<KeyCode>();

            foreach(string key in subkeys)
            {
                keycodelist.Add((KeyCode)Enum.Parse(typeof(KeyCode), key));
            }
            return keycodelist;
        }

        /// <summary>
        /// Method to simulate a Keystroke 
        /// (taken and modified from https://www.chriswirz.com/software/using-the-windows-api-to-simulate-keyboard-input-in-c-sharp)
        /// </summary>
        /// <param name="keyCode"></param>
        public static void KeyPress(List<KeyCode> keyCodes)
        {
            List<INPUT> inputs = new List<INPUT>();
            foreach (KeyCode keyCode in keyCodes)
            {
                inputs.Add(
                    new INPUT
                    {
                        type = SendInputEventType.InputKeyboard,
                        mkhi = new MOUSEANDKEYBOARDINPUT
                        {
                            ki = new KEYBOARDINPUT
                            {
                                wVk = (ushort)keyCode,
                                wScan = 0,
                                dwFlags = 0, // if nothing, key down
                                time = 0,
                                dwExtraInfo = IntPtr.Zero,
                            }
                        }
                    }
                );
            }
            foreach (KeyCode keyCode in keyCodes)
            {
                inputs.Add(
                    new INPUT
                    {
                        type = SendInputEventType.InputKeyboard,
                        mkhi = new MOUSEANDKEYBOARDINPUT
                        {
                            ki = new KEYBOARDINPUT
                            {
                                wVk = (ushort)keyCode,
                                wScan = 0,
                                dwFlags = 2, // key up
                                time = 0,
                                dwExtraInfo = IntPtr.Zero,
                            }
                        }
                    }
                );
            }
            SendInput((uint)inputs.Count, inputs.ToArray(), Marshal.SizeOf(typeof(INPUT)));
        }


        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);
    }
}
