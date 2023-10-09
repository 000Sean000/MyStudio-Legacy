using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAbstractModules
{
	public interface INodeAggregate
	{
		#region
		#endregion
		#region Holding References
		protected INote Note { get; set; }
		protected Dictionary<Guid, ILinkData> OutLinks { set; get; }
		protected Dictionary<Guid, ILinkData> InLinks { set; get; }

		#endregion

		#region Note Value Object
		public void WriteNoteData(INoteData data);
		public INoteData ReadNoteData();

		public List<INoteSegment> NoteSegments { get; set; }
		public ENoteComposition NoteComposition { get; set; }
		public ENoteImportance NoteImportance { get; set; }
		#endregion

		#region Link Entities
		public Guid AddLink(ILinkData linkData);
		public void RemoveLink(Guid linkId);
		public void WriteLinkData(Guid linkId,ILinkData linkData);
		public ILinkData ReadLinkData(Guid linkId);
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
