using System;
using System.Runtime.InteropServices;

namespace WinSparkle.Net
{
    class WinSparkle64 : IWinSparkle
    {
        public const string WinSparkleDllName = "WinSparkle64.dll";

        public WinSparkle64()
        {
            ResourcesHelper.LoadLibraryFromResources(WinSparkleDllName);
        }

        /// <summary>
        ///    Starts WinSparkle.
        ///
        /// If WinSparkle is configured to check for updates on startup, proceeds
        ///to perform the check. You should only call this function when your app
        /// is initialized and shows its main window.
        ///
        /// @note This call doesn't block and returns almost immediately. If an
        ///       update is available, the respective UI is shown later from a separate
        ///       thread.
        /// </summary>
        public void Init()
        {
            win_sparkle_init();
        }

        /// <summary>
        ///   Cleans up after WinSparkle.

        ///Should be called by the app when it's shutting down. Cancels any
        ///pending Sparkle operations and shuts down its helper threads.
        /// </summary>
        public void Cleanup()
        {
            win_sparkle_cleanup();
        }

        /// <summary>
        /// Sets URL for the app's appcast.
        /// Only http and https schemes are supported.
        /// If this function isn't called by the app, the URL is obtained from
        /// Windows resource named "FeedURL" of type "APPCAST".
        /// </summary>
        /// <param name="_url">URL of the appcast.</param>
        public void SetAppCastUrl(string _url)
        {
            win_sparkle_set_appcast_url(_url);
        }

        /// <summary>
        ///    Sets application metadata.

        /// Normally, these are taken from VERSIONINFO/StringFileInfo resources,
        ///but if your application doesn't use them for some reason, using this
        ///function is an alternative.
        /// @note @a company_name and @a app_name are used to determine the location
        ///       of WinSparkle settings in registry.
        ///       (HKCU\Software\company_name\app_name\WinSparkle is used.)
        /// </summary>
        /// <param name="_companyName"> Company name of the vendor.</param>
        /// <param name="_appName">Application name. This is both shown to the user and used in HTTP User-Agent header.</param>
        /// <param name="_appVersion">Version of the app, as string (e.g. "1.2" or "1.2rc1")</param>
        public void SetAppDetails(string _companyName, string _appName, string _appVersion)
        {
            win_sparkle_set_app_details(_companyName, _appName, _appVersion);
        }

        /// <summary>
        ///   Set the registry path where settings will be stored.
        /// Normally, these are stored in
        ///"HKCU\Software\company_name\app_name\WinSparkle"
        /// but if your application needs to store the data elsewhere for
        /// some reason, using this function is an alternative.
        /// Note that @a path is relative to HKCU/HKLM root and the root is not part
        /// of it. For example:
        /// @code
        ///  win_sparkle_set_registry_path("Software\\My App\\Updates");
        /// @endcode
        /// </summary>
        /// <param name="_path">Registry path where settings will be stored.</param>
        public void SetRegistryPath(string _path)
        {
            win_sparkle_set_registry_path(_path);
        }

        /// <summary>
        ///      Checks if an update is available, showing progress UI to the user.
        /// Normally, WinSparkle checks for updates on startup and only shows its UI
        /// when it finds an update. If the application disables this behavior, it
        ///  can hook this function to "Check for updates..." menu item.
        ///  When called, background thread is started to check for updates. A small
        ///  window is shown to let the user know the progress. If no update is found,
        ///  the user is told so. If there is an update, the usual "update available"
        ///  window is shown.
        ///   This function returns immediately.
        /// </summary>
        public void CheckUpdateWithUi()
        {
            win_sparkle_check_update_with_ui();
        }

        /// <summary>
        /// Checks if an update is available.
        ///No progress UI is shown to the user when checking. If an update is
        ///available, the usual "update available" window is shown; this function
        ///is *not* completely UI-less.
        ///Use with caution, it usually makes more sense to use the automatic update
        /// checks on interval option or manual check with visible UI.
        ///This function returns immediately.
        /// </summary>
        public void CheckUpdateWithoutUi()
        {
            win_sparkle_check_update_without_ui();
        }

        /// <summary>
        /// The interval in seconds between checks for updates. 
        /// The minimum update interval is 3600 seconds (1 hour).
        /// </summary>
        public TimeSpan UpdateInterval {
            get
            {
                return TimeSpan.FromSeconds(win_sparkle_get_update_check_interval());
            }
            set
            {
                win_sparkle_set_update_check_interval((int)value.TotalSeconds);
            }
        }

        /// <summary>
        /// Gets/sets the automatic update checking state. Defaults to False
        /// </summary>
        public bool AutomaticCheckForUpdates {
            get
            {
                return win_sparkle_get_automatic_check_for_updates() != 0;
            }
            set
            {
                win_sparkle_set_automatic_check_for_updates(value ? 1 : 0);
            }
        }

        [DllImport(WinSparkleDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_init();

        [DllImport(WinSparkleDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_cleanup();

        [DllImport(WinSparkleDllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_set_appcast_url(String _url);

        [DllImport(WinSparkleDllName, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_set_app_details(String _companyName, String _appName, String _appVersion);

        [DllImport(WinSparkleDllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_set_registry_path(String _path);

        [DllImport(WinSparkleDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_check_update_with_ui();

        [DllImport(WinSparkleDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_check_update_without_ui();

        [DllImport(WinSparkleDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_set_update_check_interval(int _interval);

        [DllImport(WinSparkleDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int win_sparkle_get_update_check_interval();

        [DllImport(WinSparkleDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int win_sparkle_get_automatic_check_for_updates();

        [DllImport(WinSparkleDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int win_sparkle_set_automatic_check_for_updates(int _state);
    }
}