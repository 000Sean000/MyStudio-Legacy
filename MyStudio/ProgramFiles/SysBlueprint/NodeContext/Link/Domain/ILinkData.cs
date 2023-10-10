using SysBlueprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlueprint
{
	public enum ELinkInfoIndex
	{
		ArrowTail, ArrowBody, ArrowHead, UserDefLinkType
	}
	public interface IReadOnlyLinkData
	{
		public Guid? Id { get; }
		public Guid? TargetNodeId { get; }
		public ELinkType? LinkType { get; }
		public Dictionary<ELinkInfoIndex, string>? LinkInfo { get; }
	}
	public interface ILinkData
	{
		public Guid? Id { get; set; }
		public Guid? TargetNodeId { get; set; }
		public ELinkType? LinkType { get; set; }
		public Dictionary<ELinkInfoIndex, string>? LinkInfo {  get; set; }
		public IReadOnlyLinkData ReadOnlyClone();
	}
}
