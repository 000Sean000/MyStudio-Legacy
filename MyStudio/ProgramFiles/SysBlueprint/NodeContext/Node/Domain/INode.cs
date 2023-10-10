using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysBlueprint
{
	

	//implementation: public class Node:NodeAggregate, INode {}
	public interface INode:INodeData, INodeAggregate
	{
		public void InputData(INoteData data);
		public INoteData OutputData();

	}
	public interface IGroupNode: INode
	{

	}
}
