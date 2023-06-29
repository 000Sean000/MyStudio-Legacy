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
        static void Main1()
        {
            Logger.WriteLine("pathes:");
            Logger.WriteLine(pathPKG.GetDirWithBackstep(0));
            Logger.WriteLine(pathPKG.GetDirWithBackstep(1));
            
            language.mode = language.US;
            Logger.WriteLine("hello");
            string deviceName = Environment.MachineName;
            Logger.WriteLine("Device Name: " + deviceName);
            //AppManager.init();
            //AppManager.test();
            DemoUndoRedo.Run();
            
        }
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Ui_Form());
        }
    }
}