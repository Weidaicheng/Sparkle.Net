using System;

namespace WinSparkle.Net
{
    internal interface IWinSparkle
    {
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
        void Init();

        /// <summary>
        ///   Cleans up after WinSparkle.

        ///Should be called by the app when it's shutting down. Cancels any
        ///pending Sparkle operations and shuts down its helper threads.
        /// </summary>
        void Cleanup();

        /// <summary>
        /// Sets URL for the app's appcast.
        /// Only http and https schemes are supported.
        /// If this function isn't called by the app, the URL is obtained from
        /// Windows resource named "FeedURL" of type "APPCAST".
        /// </summary>
        /// <param name="_url">URL of the appcast.</param>
        void SetAppCastUrl(string _url);

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
        void SetAppDetails(String _companyName, String _appName, String _appVersion);

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
        void SetRegistryPath(String _path);

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
        void CheckUpdateWithUi();

        /// <summary>
        /// Checks if an update is available.
        ///No progress UI is shown to the user when checking. If an update is
        ///available, the usual "update available" window is shown; this function
        ///is *not* completely UI-less.
        ///Use with caution, it usually makes more sense to use the automatic update
        /// checks on interval option or manual check with visible UI.
        ///This function returns immediately.
        /// </summary>
        void CheckUpdateWithoutUi();

        /// <summary>
        /// The interval in seconds between checks for updates. 
        /// The minimum update interval is 3600 seconds (1 hour).
        /// </summary>
        TimeSpan UpdateInterval { get; set; }

        /// <summary>
        /// Gets/sets the automatic update checking state. Defaults to False
        /// </summary>
        bool AutomaticCheckForUpdates { get; set; }
    }
}