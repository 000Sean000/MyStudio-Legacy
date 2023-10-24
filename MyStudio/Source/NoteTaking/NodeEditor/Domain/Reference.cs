using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Resources.ResXFileRef;

namespace NoteTaking.Domain
{
	public delegate string RefConverter(string text, object? arg = null);
	public enum EReferenceType
	{
		Direct
	}
	public static class RefConfig
	{
		public static Dictionary<EReferenceType, RefConverter> Converters { get;}
		static RefConfig()
		{
			Converters = new Dictionary<EReferenceType, RefConverter>();
			Converters[EReferenceType.Direct] = DirectConvert;
		}
		public static string DirectConvert(string text, object? arg = null)
		{
			return text;
		}
	}
	public class ReferenceData
	{
		public Guid? Id { get; set; }
		public Guid? SourceNodeId { get; set; } // will be used by target node to check back
		public Guid? TargetNodeId { get; set; }
		public EReferenceType? ReferenceType { get; set; }


		public ReferenceData() { }

		public ReferenceData(ReferenceData? referenceData)
		{
			if (referenceData != null)
			{
				Write(referenceData);
			}
		}

		public void PartialWrite(ReferenceData referenceData)
		{
			///referenceData = referenceData.DeepCopy();

			if (referenceData.Id != null)
			{
				Id = referenceData.Id;
			}
			if (referenceData.SourceNodeId != null)
			{
				SourceNodeId = referenceData.SourceNodeId;
			}
			if (referenceData.TargetNodeId != null)
			{
				TargetNodeId = referenceData.TargetNodeId;
			}
			if (referenceData.ReferenceType != null)
			{
				ReferenceType = referenceData.ReferenceType;
			}
		}
		protected void Write(ReferenceData referenceData)
		{
			///referenceData = referenceData.DeepCopy();

			Id = referenceData.Id;
			ReferenceType = referenceData.ReferenceType;

		}


		public ReferenceData Read()
		{
			return DeepCopy();
		}
		public ReferenceData DeepCopy()
		{
			ReferenceData referenceData = new ReferenceData();

			referenceData.Id = Id;
			referenceData.SourceNodeId = SourceNodeId;
			referenceData.TargetNodeId = TargetNodeId;
			referenceData.ReferenceType = ReferenceType;
			return referenceData;
		}
	}

	public class Reference: ReferenceData
	{

		public RefConverter? Converter { get; set; }

		public Reference() : base()
		{
			EnsurePropertyNotNull();
		}
		public Reference(ReferenceData? referenceData) : base(referenceData)
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
			if (ReferenceType == null)
			{
				ReferenceType = default(EReferenceType);
			}
		}

	}
}
