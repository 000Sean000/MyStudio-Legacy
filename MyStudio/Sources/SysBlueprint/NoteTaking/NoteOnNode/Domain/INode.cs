/*
 * Node: Entity, Aggregate Root
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysBlueprint
{
	public enum ENodeClass
	{
		Basic, Group, Template, Instance, Database, Options, Selections
	}
	public interface INodeDTO
	{
		public Guid? Id { get; }
		public ENodeClass? NodeClass { get; }
		public string? ImagePath { get; }

		#region Aggregate Members
		public INoteDTO? NoteData { get; }
		public Dictionary<Guid, ILinkDTO>? OutLinks { get; }
		public Dictionary<Guid, ILinkDTO>? InLinks { get; }
		#endregion
	}

	public interface INodeAggregate
	{
		#region
		#endregion

		#region Holding References
		protected INote Note { get; set; }
		protected Dictionary<Guid, ILink> OutLinks { set; get; }
		protected Dictionary<Guid, ILink> InLinks { set; get; }

		#endregion

		#region Note Value Object
		public void WriteNote(INoteDTO data);
		public INoteDTO ReadNote();
		#endregion

		#region Link Entities
		public Guid AddLink(ILinkDTO linkData);
		public void RemoveLink(Guid linkId);
		public void WriteLink(Guid linkId, ILinkDTO linkData);
		public ILinkDTO ReadLink(Guid linkId);
		#endregion

	}
	//implementation: public class Node:NodeAggregate, INode {}
	public interface INode: INodeAggregate
	{
		public Guid? Id { set; get; }
		public ENodeClass? NodeClass { set; get; }
		public string? ImagePath { set; get; }

		public INoteDTO? NoteData { set; get; }
		public Dictionary<Guid, ILinkDTO>? OutLinkData { set; get; }
		public Dictionary<Guid, ILinkDTO>? InLinkData { set; get; }


		public INodeDTO ReadData { get; }
		public void InputData(INoteDTO data);
		public INoteDTO OutputData();

	}
	public interface IGroupNode: INode
	{

	}
}
