using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInterface
{
	public interface INoteDTO
	{
		public Guid Id { set; get; }
		public ENoteComposition Composition { get; set; }
		public ENoteImportance Importance { get; set; }
		public List<INoteSegment> Segments { get; set; }
	}
}
