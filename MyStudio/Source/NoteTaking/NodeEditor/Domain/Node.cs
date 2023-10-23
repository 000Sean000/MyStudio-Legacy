using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaking.Domain
{
	public enum ENodeClass
	{
		Basic, Group, Template, Instance, Database, Options, Selections
	}
	public class NodeData
	{
		
		public Guid? Id { get; set; }
		public ENodeClass? NodeClass { get; set; }
		public string? ImagePath { get; set; }

		#region Aggregate Members
		public NoteData? NoteData { get; set; }
		public Dictionary<Guid, LinkData>? OutLinkData { get; set; }
		public List<Guid>? InLinkIds { get; set; }
		#endregion

		

		public NodeData() { }
		public NodeData(NodeData nodeData)
		{
			Write(nodeData);
		}

		public void PartialWrite(NodeData nodeData)
		{
			if (nodeData.Id != null)
			{
				Id = nodeData.Id;
			}
			if (nodeData.NodeClass != null)
			{
				NodeClass = nodeData.NodeClass;
			}
			if (nodeData.ImagePath != null)
			{
				ImagePath = nodeData.ImagePath;
			}

			if (nodeData.NoteData != null)
			{
				NoteData = nodeData.NoteData;
			}
			if (nodeData.OutLinkData != null)
			{
				OutLinkData = nodeData.OutLinkData;
			}
			if (nodeData.InLinkIds != null)
			{
				InLinkIds = nodeData.InLinkIds;
			}
		}
		public void Write(NodeData nodeData)
		{
			nodeData = nodeData.DeepCopy();
			Id = nodeData.Id;
			NodeClass = nodeData.NodeClass;
			ImagePath = nodeData.ImagePath;

			NoteData = nodeData.NoteData;
			OutLinkData = nodeData.OutLinkData;
			InLinkIds = nodeData.InLinkIds;
		}
		public NodeData Read()
		{
			return DeepCopy();
		}
		public NodeData DeepCopy()
		{
			NodeData nodeData = new NodeData();

			nodeData.Id = Id;
			nodeData.NodeClass = NodeClass;
			nodeData.ImagePath = ImagePath;
			nodeData.NoteData = NoteData?.DeepCopy();
			if (OutLinkData ==  null)
			{
				nodeData.OutLinkData = null;
			}
			else
			{
				nodeData.OutLinkData = new Dictionary<Guid, LinkData>();
				foreach (var kvp in OutLinkData)
				{
					nodeData.OutLinkData[kvp.Key] = kvp.Value.DeepCopy();
				}
			}
			if (InLinkIds == null)
			{
				nodeData.InLinkIds = null;
			}
			else
			{
				nodeData.InLinkIds = new List<Guid>(InLinkIds);
			}

			return nodeData;
		}
	}

	public interface INodeAggregate
	{
		#region
		#endregion

		#region Note
		public void WriteNote(NoteData data);
		public NoteData ReadNote();
		public void WriteNoteSegments(List<NoteSegment> segments);
		public void ExpireNoteDereference(Guid referenceNodeId); // convenient method, not necessary solution
		#endregion

		#region Link
		public void WriteLink(Guid linkId, LinkData data);
		public LinkData ReadLink(Guid linkId);
		public void AddLink(Guid linkId, LinkData linkData);
		public void RemoveLink(Guid linkId);
		#endregion

	}
	public interface INodeDomainService
	{
		#region Note 
		public string UpdateNoteDereference(List<Guid> branchVisitedNodeIds);
		#endregion

		#region Link
		public void AddLink(Guid linkId, LinkData linkData, Node targetNode);
		public void RemoveLink(Guid linkId, Node targetNode);
		#endregion
	}
	//implementation: public class Node:NodeAggregate, INode {}
	public class Node : NodeData, INodeAggregate
	{

		#region Node Aggregate
		#region Holding References
		protected Note? Note { get; set; }
		protected Dictionary<Guid, Link>? OutLinks { set; get; }

		#endregion
		public Node(NodeData nodeData):base(nodeData) 
		{
			EnsurePropertyNotNull();
			
		}
		public virtual void EnsurePropertyNotNull()
		{
			if (Id == null)
			{
				Id = default(Guid);
			}
			if (NodeClass == null)
			{
				NodeClass = default(ENodeClass);
			}
			if (ImagePath == null)
			{
				ImagePath = "";
			}

			InstantiateAggregateMembers();

		}
		protected void InstantiateAggregateMembers()
		{
			if (Note == null)
			{
				Note = new Note(NoteData);
				Note.EnsurePropertyNotNull();
			}
			if (OutLinks == null)
			{
				OutLinks = new Dictionary<Guid, Link>();
				foreach (var kvp in OutLinkData)
				{
					OutLinks[kvp.Key] = new Link(kvp.Value);
					OutLinks[kvp.Key].EnsurePropertyNotNull();
				}
			}
			

			
		}

		#region Note 
		public void ExpireNoteDereference(Guid referenceNodeId)
		{
			Note.ExpireDereference(referenceNodeId);
		}
		public void WriteNoteSegments(List<NoteSegment> segments)
		{
			Note.WriteSegments(segments);
		}
		public void WriteNote(NoteData data)
		{
			Note.PartialWrite(data);
		}
		public NoteData ReadNote()
		{
			return Note.Read();
		}
		#endregion

		#region Link
		public void WriteLink(Guid linkId, LinkData data)
		{
			OutLinks[linkId].PartialWrite(data);
		}
		public LinkData ReadLink(Guid linkId)
		{
			return OutLinks[linkId].Read();
		}
		public void AddLink(Guid linkId, LinkData linkData)
		{
			Link link = new Link((Guid)Id, (Guid)linkData.Id);
			link.PartialWrite(linkData);
			OutLinks[linkId] = link;
			
		}
		public void RemoveLink(Guid linkId, Node targetNode)
		{
			
			targetNode.InLinkIds.Remove(linkId);
			OutLinks.Remove(linkId);

		}
		public void WriteLink(Guid linkId, LinkData linkData)
		{
			OutLinks[linkId].PartialWrite(linkData);
		}
		public LinkData ReadLink(Guid linkId)
		{
			return OutLinks[linkId].Read();
		}
		#endregion
		#endregion
	}
}
