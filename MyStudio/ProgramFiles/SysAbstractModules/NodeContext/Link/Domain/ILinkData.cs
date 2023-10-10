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
	public interface IReadOnlyLinkData
	{
		public Guid? Id { get; }
		public Guid? TargetNodeId { get; }
		public ELinkType? LinkType { get; }
		public Dictionary<ELinkInfoIndex, string>? LinkInfo { get;}
	}
	public abstract class AbstractLinkData: IReadOnlyLinkData
	{

		public virtual Guid? Id { get; set; }
		public virtual Guid? TargetNodeId { get; set; }
		public virtual ELinkType? LinkType { get; set; }
		public virtual Dictionary<ELinkInfoIndex, string>? LinkInfo { get; set; }
		public abstract IReadOnlyLinkData ReadOnlyClone();
	}
}
