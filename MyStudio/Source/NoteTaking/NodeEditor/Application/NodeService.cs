using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BasicService;
using Microsoft.Extensions.DependencyInjection;


using NoteTaking.Domain;

namespace NoteTaking.Application
{
	public interface INodeRepository
	{
		public Guid CreateNode();
		public Node FetchNode(Guid nodeId);
		public void DeleteNode(Guid nodeId);
	}
	public class NodeService
	{
		public INodeRepository NodeRepo { get; set; }

		public NodeService(IServiceProvider serviceProvider)
		{
			NodeRepo = serviceProvider.GetRequiredService<INodeRepository>();
		}
		public Guid CreateNewNode()
		{
			Guid nodeId = NodeRepo.CreateNode();
			return nodeId;
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
			node.WriteNote(new NoteData() { Segments = segments}); // update Node.Note.Segments
			return dereference;
		}
		public void ExpireNoteDereference(Guid nodeId)
		{

		}
		#endregion

		#region Link
		#endregion

	}
}
