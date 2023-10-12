using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INoteTaking;
/* 
 * Configure class version to use by alias
 * Then transfer this configuration by a class wrapper
 */
using NodeToUse = SysImplementation.NodeVer1;

namespace SysConfig
{
	public class ImplementationConfig
	{
		public class Node:NodeToUse { }
		// other module to config...
	}
}
