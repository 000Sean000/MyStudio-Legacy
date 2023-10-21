using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INoteTaking
{
	public interface INodeFactory
	{
		INode CreateNode(Guid nodeId);
		INode ReconstituteNode(INodeDTO nodeData);
		IGroupNode CreateGroupNode(Guid nodeId, List<INode>? memberNodes = null);
		
		
	}
}
