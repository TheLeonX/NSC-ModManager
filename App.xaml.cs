using ModernWpf;
using NodeNetwork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using Xceed.Wpf.AvalonDock.Themes;

namespace NSC_ModManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }


    public partial class App : Application
    {

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32", SetLastError = true)]
        static extern bool FreeLibrary(IntPtr hModule);

        public App()
        {
            InitializeComponent();
        }

        private static bool IsDllPresent(string dllName)
        {
            IntPtr h = IntPtr.Zero;
            try
            {
                h = LoadLibrary(dllName);
                return h != IntPtr.Zero;
            } finally
            {
                if (h != IntPtr.Zero)
                {
                    FreeLibrary(h);
                }
            }
        }

        /// <summary>
        /// Попытаться запустить локальный инсталлятор vcredist_x86.exe с повышением прав.
        /// Возвращает true, если инсталлятор был запущен и завершился с кодом 0.
        /// </summary>
        private static bool TryRunBundledInstaller(string installerFileName, int timeoutMilliseconds = 120000)
        {
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, installerFileName);
            if (!File.Exists(exePath)) return false;

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = "/q /norestart",
                    UseShellExecute = true,
                    Verb = "runas", // запрос UAC, если требуется
                    CreateNoWindow = true
                };

                using (var p = Process.Start(psi))
                {
                    if (p == null) return false;
                    bool exited = p.WaitForExit(timeoutMilliseconds);
                    if (!exited)
                    {
                        try { p.Kill(); } catch { }
                        return false;
                    }
                    return p.ExitCode == 0;
                }
            } catch
            {
                return false;
            }
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            const string requiredDll = "MSVCP100.dll";
            const string installerName = "vcredist_x86.exe";

            // Проверяем наличие DLL
            if (!IsDllPresent(requiredDll))
            {
                // Попробовать тихо запустить bundled installer (если он рядом)
                bool installerRun = TryRunBundledInstaller(installerName);

                // После установки проверяем снова
                if (!IsDllPresent(requiredDll))
                {
                    string msg;
                    if (installerRun)
                    {
                        msg = "Microsoft Visual C++ 2010 Redistributable was run, but the required library MSVCP100.dll was not found.\n\nInstall the Redistributable manually or place a correct vcredist_x86.exe next to the application.";
                    } else
                    {
                        msg = "Microsoft Visual C++ 2010 Redistributable (x86) is required. Place \"vcredist_x86.exe\" in the application folder or install it manually and restart the program.";
                    }


                    ModernWpf.MessageBox.Show(msg, "Missing drivers", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Завершаем приложение
                    Current?.Shutdown();
                    return;
                }
            }

            // Всё в порядке — продолжаем инициализацию
            NNViewRegistrar.RegisterSplat();
        }
    }
}