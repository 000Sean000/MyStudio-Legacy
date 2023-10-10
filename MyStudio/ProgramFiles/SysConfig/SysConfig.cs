using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysAbstractModules;
/* 
 * Configure class version to use by alias
 * Then transfer this configuration by a class wrapper to other files
 */
using NodeToUse = SysImplementation.NodeVer1;

namespace SysConfig
{
	public class ImplementationConfig
	{
		
		// other module to config...
		public class LinkData { }
		public class Link { }

		public class NoteData { }
		public class Note { }
		public class NoteSegment { }
		public class NoteService { }

		public class NodeData { }
		public class Node : NodeToUse { }
		public class NodeAggregate { }
		public class NodeFactory { }
		public class NodeService { }

	}
}