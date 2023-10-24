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
	public class NodeDomainService: INodeDomainService
	{
		public INodeRepository NodeRepo { get; set; }

		public NodeDomainService(INodeRepository nodeRepo)
		{
			NodeRepo = nodeRepo;
		}
		#region Node
		public Node CreateNewNode()
		{
			Node node = NodeRepo.CreateNode();
			return node;
		}
		public void DeleteNode(Guid nodeId)
		{
			NodeRepo.DeleteNode(nodeId);
		}
		#endregion

		#region Note
		public void WriteNoteOfNode(Guid nodeId, NoteData data)
		{

		}
		public NoteData ReadNoteOfNode(Guid nodeId)
		{
			return NodeRepo.FetchNode(nodeId).ReadNote();
		}
		public void ExpireNoteDereferenceOfNode(Guid nodeId, Guid referenceNodeId)
		{
			Node node = NodeRepo.FetchNode(nodeId);
			node.ExpireNoteDereference(referenceNodeId);
		}
		public void UpdateNoteDereferenceOfNode(Guid nodeId)
		{
			GetNoteDereference(new List<Guid> { nodeId });
		}
		protected string GetNoteDereference(List<Guid> branchVisitedNodeIds)
		{
			string dereference = string.Empty;

			Guid nodeId = branchVisitedNodeIds.Last();
			Node node = NodeRepo.FetchNode(nodeId);
			
			List<NoteSegment> segments = node.ReadNote().Segments;
			foreach (var seg in segments)
			{
				if (seg.Text == null)
				{
					Guid referenceNodeId = (Guid)seg.ReferenceId;
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
							seg.Text = GetNoteDereference(nextBranchVisitedNodeIds);
						}
					}
				}
				dereference += seg.Text;
			}
			node.WriteNote(new NoteData() { Segments = segments });
			return dereference;
		}
		public bool DoesReferencenRecurseInNode(Guid nodeId, Guid referenceNodeId)
		{
			return true;
		}
		#endregion

		#region Link
		public void WriteLinkOfNode(Guid nodeId, Guid linkId, LinkData linkData)
		{

		}
		public LinkData ReadLinkOfNode(Guid nodeId, Guid linkId)
		{
			return NodeRepo.FetchNode(nodeId).ReadLink(linkId);
		}
		public void AddLinkToNode(Guid nodeId, Guid linkId, LinkData linkData)
		{

		}
		public void RemoveLinkToNode(Guid nodeId, Guid linkId)
		{

		}
		#endregion

		#region Reference
		public void WriteReferenceOfNode(Guid nodeId, Guid linkId, ReferenceData linkData)
		{

		}
		public ReferenceData ReadReferenceOfNode(Guid nodeId, Guid linkId)
		{
			return NodeRepo.FetchNode(nodeId).ReadReference(linkId);
		}
		public void AddReferenceToNode(Guid nodeId, Guid linkId, ReferenceData linkData)
		{

		}
		public void RemoveReferenceToNode(Guid nodeId, Guid linkId)
		{

		}
		#endregion

	}
}
