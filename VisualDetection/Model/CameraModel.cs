using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualDetection.Model
{
    public sealed class CameraModel
    {
        #region Constructors
        private CameraModel()
        {

        }
        #endregion

        #region Singleton
        private static volatile CameraModel instance;
        private static object syncRoot = new object();
        public static CameraModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CameraModel();
                    }
                }

                return instance;
            }
        }
        #endregion

        #region Private Properties
        #endregion

        #region Public Properties
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion

    }
}
