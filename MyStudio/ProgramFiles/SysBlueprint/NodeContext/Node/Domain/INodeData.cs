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
	public interface IReadOnlyNodeData // DTO: Data Transfer Object
	{
		public Guid? Id { set; }
		public ENodeClass? NodeClass { set; }
		public string? ImagePath { set; }

		public INoteData? NoteData { set; }
		public Dictionary<Guid, ILinkData>? OutLinks { set; }
		public Dictionary<Guid, ILinkData>? InLinks { set; }
	}
	public interface INodeData
	{
		public Guid? Id { set; get; }
		public ENodeClass? NodeClass { set; get; }
		public string? ImagePath { set; get; }

		public INoteData? NoteData { set; get; }
		public Dictionary<Guid, ILinkData>? OutLinkData { set; get; }
		public Dictionary<Guid, ILinkData>? InLinkData { set; get; }
		public IReadOnlyNodeData ReadOnlyClone();
	}
}
