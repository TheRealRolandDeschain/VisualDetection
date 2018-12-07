using System;
using System.Threading.Tasks;

namespace VisualDetection.Util
{
    public static class UpdateCheckManager
    {
        #region Public Methods
        public static async Task CheckForUpdates(IProgress<string> progressReport)
        {
            //wait for 2 sekonds, so GUI shows up and works.
            progressReport.Report(GenDefString.CheckingForUpdates);
            await Task.Delay(2000);
            progressReport.Report("Updating not yet implemented. ");

            //using (var mgr = UpdateManager.GitHubUpdateManager(GenDefString.RepositoryLink))
            //{
            //    progressReport.Report(GenDefString.CheckingForUpdates);
            //    try
            //    {
            //        var updateinfo = await mgr.Result.CheckForUpdate();
            //        Console.WriteLine("done");
            //    }
            //    catch(Exception e)
            //    {
            //        System.Windows.MessageBox.Show(e.Message);
            //    }
            //}
        }
        #endregion
    }
}
