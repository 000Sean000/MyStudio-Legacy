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
	public interface IReadOnlyNodeData 
	{
		public Guid? Id { get; }
		public ENodeClass? NodeClass { get; }
		public string? ImagePath { get; }

		public INoteData? NoteData { get; }
		public Dictionary<Guid, ILinkData>? OutLinks { get; }
		public Dictionary<Guid, ILinkData>? InLinks { get; }
	}
	public interface INodeData
	{
		public Guid? Id { set; get; }
		public ENodeClass? NodeClass { set; get; }
		public string? ImagePath { set; get; }

		public INoteData? NoteData { set; get; }
		public Dictionary<Guid, ILinkData>? OutLinkData { set; get; }
		public Dictionary<Guid, ILinkData>? InLinkData { set; get; }
		
	}
}
