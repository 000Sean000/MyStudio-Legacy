using SysAbstractModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAbstractModules
{
	public enum ELinkInfoIndex
	{
		ArrowTail, ArrowBody, ArrowHead, UserDefLinkType
	}
	public interface ILinkData
	{
		public Guid? Id { get; set; }
		public Guid? TargetNodeId { get; set; }
		public ELinkType? LinkType { get; set; }
		public Dictionary<ELinkInfoIndex, string>? LinkInfo {  get; set; }
	}
}
