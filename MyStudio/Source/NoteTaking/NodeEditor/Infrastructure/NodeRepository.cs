using NoteTaking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaking.Infrastructure
{
	public class NodeRepository
	{
		protected Node LoadNode(Guid nodeId)
		{
			return new Node(new NodeData());////
		}
		protected void SaveNode(Guid nodeId)
		{

		}
	}
}
