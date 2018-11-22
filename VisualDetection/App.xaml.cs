using Squirrel;
using System;
using System.Windows;

namespace VisualDetection
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CheckForUpdates();
        }

        #region Private Methods
        /// <summary>
        /// Checks for new Updates on StartUp and let's the user choose to restart
        /// in order to get the new update immediately. 
        /// </summary>
        /// <returns></returns>
        private void CheckForUpdates()
        {
            try
            {
                using (var manager = new UpdateManager(@"C:/Temp"))
                {
                    manager.UpdateApp();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(
                    $"No comparable program files found in update path! \n\n Error: {e.Message}"
                    );
            }
        }
        #endregion
    }
}
