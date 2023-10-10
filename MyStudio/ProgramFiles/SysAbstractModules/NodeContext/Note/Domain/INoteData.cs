using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAbstractModules
{
	public enum ENoteComposition
	{
		OnlyText, OnlySingleRef, Mixed
	}
	public enum ENoteImportance
	{
		ContextualLabel, EssentialData
	}
	public interface IReadOnlyNoteData
	{
		public ENoteComposition? Composition { get; }
		public ENoteImportance? Importance { get; }
		public List<AbstractNoteSegment>? Segments { get; }
	}
	public abstract class AbstractNoteData
	{
		public virtual ENoteComposition? Composition { get; set; }
		public virtual ENoteImportance? Importance { get; set; }
		public virtual List<AbstractNoteSegment>? Segments { get; set; }
		public abstract IReadOnlyNoteData ReadOnlyClone();
	}
	
}
