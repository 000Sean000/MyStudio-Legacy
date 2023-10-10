using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlueprint
{
	public interface INodeFactory
	{
		INode CreateNode(Guid nodeId);
		INode ReconstituteNode(INodeData nodeData);
		IGroupNode CreateGroupNode(Guid nodeId, List<INode>? memberNodes = null);
		
		
	}
}
