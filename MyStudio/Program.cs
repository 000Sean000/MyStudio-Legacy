using PKG;
using Module;
using Ui;
using MyStudio.ProgramFiles.Ui;

namespace MyStudio2
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
			Application.Run(new AppMenu());
			//Application.Run(new UiDev());
			//Application.Run(new UiNodeDev());
			//Application.Run(new CanvasDev());
		}
    }
}