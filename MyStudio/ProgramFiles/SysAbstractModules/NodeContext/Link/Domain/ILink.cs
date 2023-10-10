
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LinkData = SysConfig.ImplementationConfig.LinkData;

namespace SysAbstractModules
{
	public enum ELinkType
	{
		RelateTo, RootIn, Aggregate, ComposedOf, Implement, NextIs, ReferTo
	}
	public abstract class AbstractLink: LinkData
	{
		public abstract void InputData(AbstractLinkData data);
		public abstract AbstractLinkData OutputData();

		public abstract bool ValidateLinkInfoLength();
	}
}
