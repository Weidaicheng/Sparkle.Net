using System;
using System.Runtime.InteropServices;

namespace Sparkle.Net
{
    class WinSparkle32 : IWinSparkle
    {
        private const string WinSparkleDllName = "WinSparkle32.dll";

        public WinSparkle32()
        {
        }

        /// <summary>
        ///   Initializes WinSparkle engine
        /// </summary>
        public void Init()
        {
            win_sparkle_init();
        }

        /// <summary>
        ///   Cleanups WinSparkle resources
        /// </summary>
        public void Cleanup()
        {
            win_sparkle_cleanup();
        }

        /// <summary>
        ///   Sets app cast URL 
        /// </summary>
        /// <param name="_url"></param>
        public void SetAppCastUrl(string _url)
        {
            win_sparkle_set_appcast_url(_url);
        }

        /// <summary>
        ///   Initializes application details with supplied values
        /// </summary>
        /// <param name="_companyName"></param>
        /// <param name="_appName"></param>
        /// <param name="_appVersion"></param>
        public void SetAppDetails(string _companyName, string _appName, string _appVersion)
        {
            win_sparkle_set_app_details(_companyName,_appName,_appVersion);
        }

        /// <summary>
        ///   Root registry path, used for storing application update state
        /// </summary>
        /// <param name="_path"></param>
        public void SetRegistryPath(string _path)
        {
            win_sparkle_set_registry_path(_path);
        }

        /// <summary>
        ///   SHows application update Ui
        /// </summary>
        public void CheckUpdateWithUi()
        {
            win_sparkle_check_update_with_ui();
        }

        [DllImport(WinSparkleDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_init();
        [DllImport(WinSparkleDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_cleanup();
        [DllImport(WinSparkleDllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_set_appcast_url(String _url);
        [DllImport(WinSparkleDllName, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_set_app_details(String _companyName,
            String _appName,
            String _appVersion);
        [DllImport(WinSparkleDllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_set_registry_path(String _path);
        [DllImport(WinSparkleDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_check_update_with_ui();
    }
}