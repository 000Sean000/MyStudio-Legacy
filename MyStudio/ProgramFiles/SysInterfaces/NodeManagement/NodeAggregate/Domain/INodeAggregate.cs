using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInterface
{
	public interface INodeAggregate
	{
		protected List<ILink> OutLinks { get; set; }
		protected List<ILink> InLinks { get; set; }
		protected List<INode> OutNodes { get; set; }
		protected List<INode> InNodes { get; set; }
		protected INote Note { get; set; }


		public void AddLink(ILink link);
		public void RemoveLink(ILink link);
		public void WriteLinkText(ILink link, ELinkInfoType linkInfoType, string text);
		public string ReadLinkText(ILink link, ELinkInfoType linkInfoType);

		public void WriteNote(List<INoteSegment> segments);
		public List<INoteSegment> ReadNote();
		public void SetNoteComposition(ENoteComposition composition);
		public ENoteComposition GetNoteComposition();
		public void SetNoteImportance(ENoteImportance importance);
		public ENoteImportance GetNoteImportance();
	}
}
