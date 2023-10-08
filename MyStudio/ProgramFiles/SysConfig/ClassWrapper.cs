using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;
/* 
 * Configure class version to use by alias
 * Then transfer this configuration by a class wrapper
 */
using NodeToUse = DomainLayer.NodeVer1;



namespace SysConfig
{
	public class ClassConfig
	{
		public class Node:NodeToUse { }
		
	}
}
