using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INoteTaking
{
	public interface IUIManager
	{
		public Form StartupScreen {  get; set; }
		public Form MainWindow { get; set; }
	}
}
