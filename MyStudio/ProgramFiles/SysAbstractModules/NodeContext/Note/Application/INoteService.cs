using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAbstractModules
{
	public interface INoteService
	{

		public string SegmentToString(AbstractNoteSegment segment);
		public AbstractNoteSegment ReferenceToSegment(Guid targetNodeId);
	}
}
