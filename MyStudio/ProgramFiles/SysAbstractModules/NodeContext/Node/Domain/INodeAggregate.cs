using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAbstractModules
{
	public abstract class AbstractNodeAggregate: AbstractNodeData
	{
		#region
		#endregion
		#region Holding References
		protected virtual AbstractNote Note { get; set; }
		protected virtual Dictionary<Guid, AbstractLinkData> OutLinks { set; get; }
		protected virtual Dictionary<Guid, AbstractLinkData> InLinks { set; get; }

		#endregion

		#region Note Value Object
		public abstract void WriteNoteData(AbstractNoteData data);
		public abstract IReadOnlyNoteData ReadNoteData();
		/*
		public List<AbstractNoteSegment> NoteSegments { get; set; }
		public ENoteComposition NoteComposition { get; set; }
		public ENoteImportance NoteImportance { get; set; }
		*/
		#endregion

		#region Link Entities
		public abstract Guid AddLink(AbstractLinkData linkData);
		public abstract void RemoveLink(Guid linkId);
		public abstract void WriteLinkData(Guid linkId, AbstractLinkData linkData);
		public abstract IReadOnlyLinkData ReadLinkData(Guid linkId);
		/*
		public void WriteLinkType(Guid linkId, ELinkType linkType);
		public ELinkType ReadLinkType(Guid linkId);
		public void WriteLinkText(Guid linkId, ELinkInfoIndex linkInfoType, string text);
		public string ReadLinkText(Guid linkId, ELinkInfoIndex linkInfoType);
		public void AlterLinkTarget(Guid linkId, Guid targetNodeId);
		*/
		#endregion
		
	}
}
