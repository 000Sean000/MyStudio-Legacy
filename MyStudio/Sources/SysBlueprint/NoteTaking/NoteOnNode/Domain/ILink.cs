
/*
 * Link: Entity
 */

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
	public interface ILinkDTO
	{
		public Guid? Id { get; }
		public Guid? SourceNodeId { get; }
		public Guid? TargetNodeId { get; }
		public ELinkType? LinkType { get; }
		public Dictionary<ELinkInfoIndex, string>? LinkInfo { get; }
	}

	public enum ELinkType
	{
		RelateTo, RootIn, Aggregate, ComposedOf, Implement, NextIs, ReferTo
	}
	public interface ILink
	{

		public Guid? Id { get; set; }
		public Guid? SourceNodeId { get; set; }
		public Guid? TargetNodeId { get; set; }
		public ELinkType? LinkType { get; set; }
		public Dictionary<ELinkInfoIndex, string>? LinkInfo { get; set; }


		public ILinkDTO ReadData {  get; }
		public void InputData(ILinkDTO data);
		public ILinkDTO OutputData();

	}
}
