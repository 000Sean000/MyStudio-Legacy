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
	public class NodeDTO
	{
		public NodeDTO()
		{

		}
		public Guid? Id { get; set; }
		public ENodeClass? NodeClass { get; set; }
		public string? ImagePath { get; set; }

		#region Aggregate Members
		public NoteDTO? NoteData { get; set; }
		public Dictionary<Guid, LinkDTO>? OutLinks { get; set; }
		public Dictionary<Guid, LinkDTO>? InLinks { get; set; }
		#endregion
	}

	public interface INodeAggregate
	{
		#region
		#endregion

		#region Note Value Object
		public void WriteNote(NoteDTO data);
		public NoteDTO ReadNote();
		#endregion

		#region Link Entities
		public Guid AddLink(LinkDTO linkData);
		public void RemoveLink(Guid linkId);
		public void WriteLink(Guid linkId, LinkDTO linkData);
		public LinkDTO ReadLink(Guid linkId);
		#endregion

	}
	//implementation: public class Node:NodeAggregate, INode {}
	public class Node : INodeAggregate
	{
		public Guid? Id { set; get; }
		public ENodeClass? NodeClass { set; get; }
		public string? ImagePath { set; get; }

		public NoteDTO? NoteData { set; get; }
		public Dictionary<Guid, LinkDTO>? OutLinkData { set; get; }
		public Dictionary<Guid, LinkDTO>? InLinkData { set; get; }


		public NodeDTO ReadData { get; }
		public void InputData(NoteDTO data)
		{

		}
		public NoteDTO OutputData()
		{
			return new NoteDTO();////
		}
		#region Node Aggregate
		#region Holding References
		protected Note Note { get; set; }
		protected Dictionary<Guid, Link> OutLinks { set; get; }
		protected Dictionary<Guid, Link> InLinks { set; get; }

		#endregion

		#region Note Value Object
		public void WriteNote(NoteDTO data)
		{

		}
		public NoteDTO ReadNote()
		{
			return new NoteDTO();////
		}
		#endregion

		#region Link Entities
		public Guid AddLink(LinkDTO linkData)
		{
			return new Guid();////
		}
		public void RemoveLink(Guid linkId)
		{

		}
		public void WriteLink(Guid linkId, LinkDTO linkData)
		{

		}
		public LinkDTO ReadLink(Guid linkId)
		{
			return new LinkDTO();////
		}
		#endregion
		#endregion
	}
}
