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
	public interface IReadOnlyNodeData // DTO: Data Transfer Object
	{
		public Guid? Id { get; }
		public ENodeClass? NodeClass { get; }
		public string? ImagePath { get; }

		public AbstractNoteData? NoteData { get; }
		public Dictionary<Guid, AbstractLinkData>? OutLinks { get; }
		public Dictionary<Guid, AbstractLinkData>? InLinks { get; }
	}
	public abstract class AbstractNodeData // DTO: Data Transfer Object
	{
		public virtual Guid? Id { set; get; }
		public virtual ENodeClass? NodeClass { set; get; }
		public virtual string? ImagePath { set; get; }

		public virtual AbstractNoteData? NoteData { set; get; }
		public virtual Dictionary<Guid, AbstractLinkData>? OutLinks { set; get; }
		public virtual Dictionary<Guid, AbstractLinkData>? InLinks { set; get; }
		public abstract IReadOnlyNoteData ReadOnlyClone();
	}
}
