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
			if (NoteData == null)
			{
				NoteData = new NoteData();
				NoteData.EnsurePropertyNotNull();
			}
			if (OutLinkData == null)
			{
				OutLinkData = new Dictionary<Guid, LinkData>();
			}
			if (InLinkIds == null)
			{
				InLinkIds = new List<Guid>();
			}

		}

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

		#region Note Value Object
		public void WriteNote(NoteData data);
		public NoteData ReadNote();
		#endregion

		#region Link Entities
		public void AddLink(Guid linkId, Node targetNode);
		public void RemoveLink(Guid linkId);
		public void WriteLink(Guid linkId, LinkData linkData);
		public LinkData ReadLink(Guid linkId);
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
		public override void EnsurePropertyNotNull()
		{
			base.EnsurePropertyNotNull();
			InstantiateAggregateMembers();

		}
		protected void InstantiateAggregateMembers()
		{
            if (Note == null)
            {
				Note = new Note(NoteData);
			}
			if (OutLinks == null)
			{
				OutLinks = new Dictionary<Guid, Link>();
			}
			if (OutLinkData != null)
			{
				foreach (var kvp in OutLinkData)
				{
					OutLinks[kvp.Key] = new Link(kvp.Value);
				}
			}
			
		}

		#region Note Value Object
		public void WriteNote(NoteData data)
		{
			Note.PartiaWrite(data);
		}
		public NoteData ReadNote()
		{
			return Note.Read();
		}
		#endregion

		#region Link Entities
		public void AddLink(Guid linkId, Node targetNode)
		{
			OutLinks[linkId] = new Link()
			{
				Id = linkId,
				SourceNodeId = Id, 
				TargetNodeId = targetNode.Id
			};
			targetNode.InLinkIds.Add(linkId);
		}
		public void RemoveLink(Guid linkId, Node targetNode)
		{
			targetNode.InLinkIds.Remove(linkId);
			OutLinks.Remove(linkId);

		}
		public void WriteLink(Guid linkId, LinkData linkData)
		{

		}
		public LinkData ReadLink(Guid linkId)
		{
			return new LinkData();////
		}
		#endregion
		#endregion
	}
}
