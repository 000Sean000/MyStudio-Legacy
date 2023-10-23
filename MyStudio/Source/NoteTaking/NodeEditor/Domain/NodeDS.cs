using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;



namespace NoteTaking.Domain
{
	public interface INodeRepository
	{
		public Node CreateNode();
		public Node FetchNode(Guid nodeId);
		public void DeleteNode(Guid nodeId);
	}
	public class NodeDomainService
	{
		public INodeRepository NodeRepo { get; set; }

		public NodeDomainService(INodeRepository nodeRepo)
		{
			NodeRepo = nodeRepo;
		}
		public Node CreateNewNode()
		{
			Node node = NodeRepo.CreateNode();
			return node;
		}
		public void DeleteNode(Guid nodeId)
		{
			NodeRepo.DeleteNode(nodeId);
		}
		#region Note
		// usage: method(new List<Guid>(){currentNodeId};
		public string UpdateNoteDereference(List<Guid> branchVisitedNodeIds)
		{
			string dereference = string.Empty;

			Guid nodeId = branchVisitedNodeIds.Last();
			Node node = NodeRepo.FetchNode(nodeId);
			
			List<NoteSegment> segments = node.ReadNote().Segments;
			foreach (var seg in segments)
			{
				Guid referenceNodeId = (Guid)seg.ReferenceNodeId; 
				if (referenceNodeId != null && seg.Text == null)
				{
					if (branchVisitedNodeIds.Contains((Guid)referenceNodeId)) 
					{
						throw new Exception("[Recursive Reference!]");
					}
					else
					{
						List<Guid> nextBranchVisitedNodeIds = new List<Guid>(branchVisitedNodeIds);
						nextBranchVisitedNodeIds.Add(referenceNodeId);
						seg.Text = UpdateNoteDereference(nextBranchVisitedNodeIds);
					}
				}
				dereference += seg.Text;
			}
			node.WriteNoteSegments(segments);
			return dereference;
		}
		public void ExpireNoteDereference(Guid nodeId, Guid referenceNodeId)
		{
			Node node = NodeRepo.FetchNode(nodeId);
			node.ExpireNoteDereference(referenceNodeId);			
		}
		#endregion

		#region Link
		#endregion

	}
}
