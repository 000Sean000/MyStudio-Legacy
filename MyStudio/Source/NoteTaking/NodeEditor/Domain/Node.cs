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
		public List<Guid>? ReferringToNodeIds { get; protected set; }
		public List<Guid>? ReferredByNodeIds { get; protected set; }

		#region Aggregate Members
		public NoteData? NoteData { get; protected set; }
		public Dictionary<Guid, LinkData>? OutLinkData { get; protected set; }
		public List<Guid>? InLinkIds { get; protected set; }
		#endregion

		

		public NodeData() { }
		public NodeData(NodeData nodeData)
		{
			Write(nodeData);
		}

		public void PartialWrite(NodeData nodeData)
		{
			///nodeData = nodeData.DeepCopy();
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
		protected void Write(NodeData nodeData)
		{
			///nodeData = nodeData.DeepCopy();
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
		public void ExpireNoteDereference(Guid referenceNodeId); // convenient method, not necessary solution
		#endregion

		#region Link
		public void WriteLink(Guid linkId, LinkData linkData);
		public LinkData ReadLink(Guid linkId);
		public void AddLink(Guid linkId, LinkData linkData);
		public void RemoveLink(Guid linkId);
		#endregion

	}
	public interface INodeDomainService
	{
		#region Node
		public Node CreateNewNode();
		public void DeleteNode(Guid nodeId);
		#endregion
		#region Note 
		public void WriteNoteOfNode(Guid nodeId, NoteData data);
		public NoteData ReadNoteOfNode(Guid nodeId);
		public void ExpireNoteDereferenceOfNode(Guid nodeId, Guid referenceNodeId);
		public void UpdateNoteDereferenceOfNode(Guid nodeId);
		public bool DoesReferencenRecurseInNode(Guid nodeId, Guid referenceNodeId);
		#endregion

		#region Link
		public void WriteLinkOfNode(Guid nodeId, Guid linkId, LinkData linkData);
		public LinkData ReadLinkOfNode(Guid nodeId, Guid linkId);
		public void AddLinkToNode(Guid nodeId, Guid linkId, LinkData linkData);
		public void RemoveLinkToNode(Guid nodeId, Guid linkId);
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
			if (ReferringToNodeIds == null)
			{
				ReferringToNodeIds = new List<Guid>();
			}
			if (ReferredByNodeIds == null)
			{
				ReferredByNodeIds = new List<Guid>();
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

		public void WriteNote(NoteData data)
		{
			NoteData = data;
			Note.PartialWrite(data);
		}
		public NoteData ReadNote()
		{
			return Note.Read();
		}
		#endregion

		#region Link
		public void WriteLink(Guid linkId, LinkData linkData)
		{
			OutLinks[linkId].PartialWrite(linkData);
			OutLinkData[linkId] = OutLinks[linkId].Read(); // get updated Link Data by .Read() due to partial write mechanism
		}
		public LinkData ReadLink(Guid linkId)
		{
			return OutLinks[linkId].Read();
		}
		public void AddLink(Guid linkId, LinkData linkData)
		{
			Link link = new Link(linkData);
			OutLinks[linkId] = link;
			OutLinkData[linkId] = linkData;
		}
		public void RemoveLink(Guid linkId)
		{
			OutLinks.Remove(linkId);
			OutLinkData.Remove(linkId);
		}

		#endregion
		#endregion
	}
}
