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
        public void CheckForUpdates()
        {
            using (var mgr = UpdateManager.GitHubUpdateManager(GenDefString.RepositoryLink))
            {
                try
                {
                    mgr.Result.UpdateApp();
                    updaterMessage = GenDefString.NewUpdateFound;
                    dispatcher.Invoke(() => SetUpdaterMessage(), DispatcherPriority.Normal);
                }
                catch(Exception e)
                {
                    var asdf = e;
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
