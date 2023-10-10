using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysConfig;
using Node = SysConfig.ImplementationConfig.Node;

namespace SysBlueprint
{
	public abstract class SomeModule
	{
		protected SomeModule() { }
		protected Node node {  get; set; }
		public void SomeMethod()
		{
			INode node2 = new Node();
			node2.ImagePath = "";
		}
	}
}
