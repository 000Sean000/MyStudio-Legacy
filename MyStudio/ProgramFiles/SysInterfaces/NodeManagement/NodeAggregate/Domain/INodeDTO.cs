using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInterface
{
	public interface INodeDTO // DTO: Data Transfer Object
	{
		public Guid Id { set; get; }
		public ENodeClass NodeClass { set; get; }
		public ENoteComposition NoteComposition { set; get; }
		public ENoteImportance NoteImportance { set; get; }
		public List<Guid> OutLinkId { set; get; }
		public List<Guid> InLinkId { set; get; }
		public List<INoteSegment> noteSegments { set; get; }
	}
}
