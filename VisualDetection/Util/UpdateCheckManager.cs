using Squirrel;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using VisualDetection.ViewModel;

namespace VisualDetection.Util
{
    public class UpdateCheckManager
    {
        #region Constructor
        public UpdateCheckManager(OutputViewModel outputIn)
        {
            outputvm = outputIn;
        }
        #endregion

        #region Private Properties
        private Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        private OutputViewModel outputvm;
        private string updaterMessage;
        #endregion

        #region Public Methods
        /// <summary>
        /// Checks for available Updates and reports back to the user
        /// </summary>
        /// <returns></returns>
        public async Task CheckForUpdatesAsync()
        {
            using (var mgr = UpdateManager.GitHubUpdateManager(GenDefString.RepositoryLink))
            {
                try
                {
                    var IsUpdateAvailable = await Task.Run(() => mgr.Result.CheckForUpdate().Result);
                    if (IsUpdateAvailable.CurrentlyInstalledVersion.SHA1 != IsUpdateAvailable.FutureReleaseEntry.SHA1)
                    {
                        await mgr.Result.UpdateApp();
                        UpdateManager.RestartApp();
                    }
                    updaterMessage = GenDefString.NewUpdateFound;
                    dispatcher.Invoke(() => SetUpdaterMessage(), DispatcherPriority.Normal);
                }
                catch
                {
                    updaterMessage = GenDefString.UpdateFailed;
                    dispatcher.Invoke(() => SetUpdaterMessage(), DispatcherPriority.Normal);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the message output for the updater status
        /// </summary>
        private void SetUpdaterMessage()
        {
            outputvm.UpdateMessage = updaterMessage;
        }
        #endregion
    }
}
