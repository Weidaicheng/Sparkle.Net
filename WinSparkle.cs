using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sparkle.Net
{
    public class WinSparkle : IWinSparkle
    {
        private IWinSparkle m_instance;

        public WinSparkle()
        {
            if (IntPtr.Size == 8)
            {
                // x64
                m_instance = new WinSparkle64();
            } else
            {
                // x86
                m_instance = new WinSparkle32();
            }
        }

        /// <summary>
        ///   Initializes WinSparkle engine
        /// </summary>
        public void Init()
        {
            m_instance.Init();
        }

        /// <summary>
        ///   Cleanups WinSparkle resources
        /// </summary>
        public void Cleanup()
        {
            m_instance.Cleanup();
        }

        /// <summary>
        ///   Sets app cast URL 
        /// </summary>
        /// <param name="_url"></param>
        public void SetAppCastUrl(string _url)
        {
            m_instance.SetAppCastUrl(_url);
        }

        /// <summary>
        ///   Initializes application details with supplied values
        /// </summary>
        /// <param name="_companyName"></param>
        /// <param name="_appName"></param>
        /// <param name="_appVersion"></param>
        public void SetAppDetails(string _companyName, string _appName, string _appVersion)
        {
            m_instance.SetAppDetails(_companyName, _appName, _appVersion);
        }

        /// <summary>
        ///   Root registry path, used for storing application update state
        /// </summary>
        /// <param name="_path"></param>
        public void SetRegistryPath(string _path)
        {
            m_instance.SetRegistryPath(_path);
        }

        /// <summary>
        ///   SHows application update Ui
        /// </summary>
        public void CheckUpdateWithUi()
        {
            m_instance.CheckUpdateWithUi();
        }
    }
}
