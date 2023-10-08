using SysInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInterface
{
	public enum ELinkInfoType
	{
		ArrowTail, ArrowBody, ArrowHead, UserDefLinkType
	}
	public interface ILinkDTO
	{
		public Guid Id { get; set; }
		public Guid TargetNodeId { get; set; }
		public ELinkType LinkType { get; set; }
		public Dictionary<ELinkInfoType, string> LinkInfo {  get; set; }

	}
}
