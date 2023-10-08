using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInterface
{
	public interface INoteService
	{

		public string SegmentToString(INoteSegment segment);
		public INoteSegment ReferenceToSegment(Guid targetNodeId);
	}
}
