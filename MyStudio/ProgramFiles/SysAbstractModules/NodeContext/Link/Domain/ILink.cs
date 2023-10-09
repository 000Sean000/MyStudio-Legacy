
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAbstractModules
{
	public enum ELinkType
	{
		RelateTo, RootIn, Aggregate, ComposedOf, Implement, NextIs, ReferTo
	}
	public interface ILink: ILinkData
	{
		public void InputData(ILinkData data);
		public ILinkData OutputData();

		public bool ValidateLinkInfoLength();
	}
}
