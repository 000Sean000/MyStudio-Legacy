using System.Diagnostics;
using Module;
using PKG;
namespace MyStudio
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            language.mode = language.US;
            Logger.WriteLine("hello");
            string deviceName = Environment.MachineName;
            Logger.WriteLine("Device Name: " + deviceName);
            Module.AppManager.init();
            
        }
        static void Main2()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}