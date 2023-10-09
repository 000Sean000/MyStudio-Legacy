using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysConfig;
using Node = SysConfig.ImplementationConfig.Node;

namespace SysAbstractModules
{
	public abstract class SomeModule
	{
		protected SomeModule() { }
		protected Node node {  get; set; }
		public void SomeMethod()
		{
			Node node2 = new Node();
		}
	}
}
