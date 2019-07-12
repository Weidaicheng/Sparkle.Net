using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WinSparkleDotNet
{
    internal static class ResourcesHelper
    {
        private static Assembly m_thisAssembly = Assembly.GetExecutingAssembly();

        static ResourcesHelper()
        {
        }
        
        public static void LoadLibraryFromResources(string _libraryName)
        {
            var dllHandle = LoadLibrary(_libraryName);
            if (dllHandle != IntPtr.Zero)
            {
                return;
            }
            var dllBytes = ResourcesHelper.GetResourcesBytes(_libraryName);
            if (dllBytes == null)
            {
                throw new ApplicationException(string.Format("Could not extract library '{0}' from resources!", _libraryName));
            }
            var dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _libraryName);
            File.WriteAllBytes(dllPath, dllBytes);
            dllHandle = LoadLibrary(dllPath);
            if (dllHandle == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error()).Message;
                throw new ApplicationException(string.Format("Could not load library '{0}' ! Error: {1}", _libraryName, error));
            }
        }

        /// <summary>
        ///     Loads resource bytes by name
        /// </summary>
        /// <param name="_resourceName"></param>
        /// <returns></returns>
        static byte[] GetResourcesBytes(string _resourceName)
        {
            if (String.IsNullOrEmpty(_resourceName))
            {
                throw new ArgumentNullException("_resourceName");
            }

            var assemblyName = typeof(ResourcesHelper).Assembly.GetName().Name;

            if (assemblyName != null && !_resourceName.StartsWith(assemblyName + "."))
            {
                _resourceName = String.Format("{0}.{1}", assemblyName, _resourceName);
            }

            var resources = m_thisAssembly.GetManifestResourceNames();
            using (var resourceStream = m_thisAssembly.GetManifestResourceStream(_resourceName))
            {
                if (resourceStream == null)
                {
                    throw new ApplicationException(
                        string.Format(
                            "Could not find resource '{0}'. Resources list({1}):\r\n\t{2}",
                            _resourceName,
                            resources.Length,
                            String.Join("\r\n\t", resources)));
                }
                return new BinaryReader(resourceStream).ReadBytes((int)resourceStream.Length);
            }
        }

        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string _lpFileName);
    }
}