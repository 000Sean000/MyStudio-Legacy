using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaking.Domain
{
	public enum ELinkInfoIndex
	{
		ArrowTail, ArrowBody, ArrowHead, UserDefLinkType
	}
	public class LinkDTO
	{
		public Guid Id;
		public Guid SourceNodeId;
		public Guid TargetNodeId;
		public ELinkType LinkType;
		public Dictionary<ELinkInfoIndex, string> LinkInfo;
		public LinkDTO() { }

		public LinkDTO(LinkDTO linkDTO)
		{
			linkDTO = linkDTO.DeepCopy();
			Id = linkDTO.Id;
			SourceNodeId = linkDTO.SourceNodeId;
			TargetNodeId = linkDTO.TargetNodeId;
			LinkType = linkDTO.LinkType;
			LinkInfo = linkDTO.LinkInfo;

		}

		public LinkDTO(Guid id, Guid sourceNodeId, Guid targetNodeId, ELinkType linkType, Dictionary<ELinkInfoIndex, string> linkInfo)
		{
			Id = id;
			SourceNodeId = sourceNodeId;
			TargetNodeId = targetNodeId;
			LinkType = linkType;
			LinkInfo = linkInfo;
		}

		public LinkDTO DeepCopy()
		{
			return new LinkDTO(Id, SourceNodeId, TargetNodeId, LinkType, new Dictionary<ELinkInfoIndex, string>(LinkInfo));
		}
	}

	public enum ELinkType
	{
		RelateTo, RootIn, Aggregate, ComposedOf, Implement, NextIs, ReferTo
	}
	public class Link:LinkDTO
	{


		public LinkDTO ReadData { get; }
		public void InputData(LinkDTO data)
		{

		}
		public LinkDTO OutputData()
		{
			return new LinkDTO();////
		}

	}
}
