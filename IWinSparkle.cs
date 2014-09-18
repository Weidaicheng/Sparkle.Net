using System;

namespace Sparkle.Net
{
    internal interface IWinSparkle
    {
        /// <summary>
        ///   Initializes WinSparkle engine
        /// </summary>
        void Init();

        /// <summary>
        ///   Cleanups WinSparkle resources
        /// </summary>
        void Cleanup();

        /// <summary>
        ///   Sets app cast URL 
        /// </summary>
        /// <param name="_url"></param>
        void SetAppCastUrl(string _url);

        /// <summary>
        ///   Initializes application details with supplied values
        /// </summary>
        /// <param name="_companyName"></param>
        /// <param name="_appName"></param>
        /// <param name="_appVersion"></param>
        void SetAppDetails(String _companyName, String _appName, String _appVersion);

        /// <summary>
        ///   Root registry path, used for storing application update state
        /// </summary>
        /// <param name="_path"></param>
        void SetRegistryPath(String _path);

        /// <summary>
        ///   SHows application update Ui
        /// </summary>
        void CheckUpdateWithUi();
    }
}