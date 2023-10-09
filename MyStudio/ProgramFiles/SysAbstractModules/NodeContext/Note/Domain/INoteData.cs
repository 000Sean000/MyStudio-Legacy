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
	public interface INoteData
	{
		public ENoteComposition? Composition { get; set; }
		public ENoteImportance? Importance { get; set; }
		public List<INoteSegment>? Segments { get; set; }
	}
}
