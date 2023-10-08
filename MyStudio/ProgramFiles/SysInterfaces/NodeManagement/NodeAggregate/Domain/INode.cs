using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysInterface
{
	public enum ENodeClass
	{
		Basic, Group, Template, Instance, Database, Options, Selections
	}

	//implementation: public class Node:NodeAggregate, INode {}
	public interface INode:INodeDTO, INodeAggregate
	{

		public INode Clone();
		public void Delete();

	}
}
