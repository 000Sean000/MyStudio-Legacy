using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysBlueprint
{
	
	public interface INote: INoteData
	{
		public void InputData(INoteData data);
		public INoteData OutputData();
	}
}
