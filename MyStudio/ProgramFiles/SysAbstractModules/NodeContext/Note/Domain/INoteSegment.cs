using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAbstractModules
{
	public abstract class AbstractNoteSegment
	{
		public virtual string? Text { get; set; }
		public virtual Guid? ReferenceNodeId { get; set; }
	}

}
