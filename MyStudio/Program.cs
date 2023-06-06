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
        

        enum DaysOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
        [STAThread]
        static void Main()
        {

            language.mode = language.US;
            Logger.WriteLine("hello");
            string deviceName = Environment.MachineName;
            Logger.WriteLine("Device Name: " + deviceName);
            Module.AppManager.init();
            Module.AppManager.test();
            Logger.WriteLine((int)DayOfWeek.Monday);
            Logger.WriteLine(DayOfWeek.Monday.ToString());
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