using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysAbstractModules
{
	
	public abstract class AbstractNote: AbstractNoteData
	{
		public abstract void InputData(AbstractNoteData data);
		public abstract AbstractNoteData OutputData();
	}
}
