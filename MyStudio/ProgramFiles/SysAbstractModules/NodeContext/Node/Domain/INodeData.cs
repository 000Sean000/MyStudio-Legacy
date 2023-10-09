using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAbstractModules
{
	public enum ENodeClass
	{
		Basic, Group, Template, Instance, Database, Options, Selections
	}
	public interface INodeData // DTO: Data Transfer Object
	{
		public Guid? Id { set; get; }
		public ENodeClass? NodeClass { set; get; }
		public string? ImagePath { set; get; }

		public INoteData? NoteData { set; get; }
		public Dictionary<Guid, ILinkData>? OutLinks { set; get; }
		public Dictionary<Guid, ILinkData>? InLinks { set; get; }
		


	}
}
