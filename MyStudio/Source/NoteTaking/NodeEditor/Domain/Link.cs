using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaking.Domain
{
	public enum ELinkType
	{
		RelateTo, RootIn, Aggregate, ComposedOf, Implement, NextIs, ReferTo
	}
	public enum ELinkInfoIndex
	{
		ArrowTail, ArrowBody, ArrowHead, UserDefLinkType
	}
	public class LinkData
	{
		public Guid? Id { get; set; }
		public Guid? SourceNodeId { get; set; } // will be used by target node to check back
		public Guid? TargetNodeId { get; set; }
		public ELinkType? LinkType { get; set; }
		public Dictionary<ELinkInfoIndex, string>? LinkInfo { get; set; }

		public LinkData() { } 
		
		public LinkData(LinkData? linkData)
		{
			if (linkData != null)
			{
				Write(linkData);
			}
		}

		public void PartialWrite(LinkData linkData)
		{
			///linkData = linkData.DeepCopy();

			if (linkData.Id != null )
			{
				Id = linkData.Id;
			}
			if (linkData.SourceNodeId != null)
			{
				SourceNodeId = linkData.SourceNodeId;
			}
			if (linkData.TargetNodeId != null)
			{
				TargetNodeId = linkData.TargetNodeId;
			}
			if (linkData.LinkType != null ) 
			{  
				LinkType = linkData.LinkType; 
			}
			if (linkData.LinkInfo != null )
			{
				LinkInfo = linkData.LinkInfo;
			}
		}
		protected void Write(LinkData linkData)
		{
			///linkData = linkData.DeepCopy();
			
			Id = linkData.Id;
			LinkType = linkData.LinkType;
			LinkInfo = linkData.LinkInfo;

		}


		public LinkData Read()
		{
			return DeepCopy();
		}
		public LinkData DeepCopy()
		{
			LinkData linkData = new LinkData();

			linkData.Id = Id;
			linkData.SourceNodeId = SourceNodeId;
			linkData.TargetNodeId = TargetNodeId;
			linkData.LinkType = LinkType;
			if (LinkInfo == null)
			{
				linkData.LinkInfo = null;
			}
			else
			{
				linkData.LinkInfo = new Dictionary<ELinkInfoIndex, string>(LinkInfo);
				foreach(var kvp in  LinkInfo)
				{
					linkData.LinkInfo[kvp.Key] = kvp.Value; // shallow copy of immutable type is enough for deep copy
				}
			}
			return linkData;
		}
	}


	public class Link:LinkData
	{
		public Link() : base()
		{
			EnsurePropertyNotNull();
		}
		public Link(LinkData? linkData) : base(linkData)
		{
			EnsurePropertyNotNull();
		}
		public void EnsurePropertyNotNull()
		{
			if (Id == null)
			{
				Id = default(Guid);
			}
			if (SourceNodeId == null)
			{
				SourceNodeId = default(Guid);
			}
			if (TargetNodeId == null)
			{
				TargetNodeId = default(Guid);
			}
			if (LinkType == null)
			{
				LinkType = default(ELinkType);
			}
			if (LinkInfo == null)
			{
				LinkInfo = new Dictionary<ELinkInfoIndex, string>();
				foreach (ELinkInfoIndex index in Enum.GetValues(typeof(ELinkInfoIndex)))
				{
					LinkInfo[index] = string.Empty;
				}
			}
		}



	}
}
