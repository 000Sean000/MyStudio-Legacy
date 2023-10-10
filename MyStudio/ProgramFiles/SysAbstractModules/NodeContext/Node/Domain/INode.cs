using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysAbstractModules
{
	

	//implementation: public class Node:NodeAggregate, INode {}
	public abstract class AbstractNode: AbstractNodeAggregate
	{
		public abstract void InputData(AbstractNoteData data);
		public abstract AbstractNoteData OutputData();

	}

}
