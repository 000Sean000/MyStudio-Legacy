using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysAbstractModules
{
	
	public interface INote: INoteData
	{
		public void InputData(INoteData data);
		public INoteData OutputData();
	}
}
