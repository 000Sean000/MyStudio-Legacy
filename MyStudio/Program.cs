using PKG;
using Module;
using Ui;

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
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			///Application.Run(new AppMenu());
			///Test.test();
			JsonPKG.DemoJson("C:\\Users\\Sean_Wu\\OneDrive\\MyNotes\\Programs\\ComposingToolPrograms\\MyStudio\\MyStudio\\data.json");
		}
    }
}