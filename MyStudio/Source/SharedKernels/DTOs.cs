
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region Dependency
using Enums;
#endregion

// variable name should be the same for mapping
namespace DTOs
{
	public class NodeDTO
	{

		public Guid? Id { get; set; }
		public ENodeClass? NodeClass { get; set; }
		public string? ImagePath { get; set; }

		#region Aggregate Members
		public NoteDTO? NoteData { get; protected set; }
		public Dictionary<Guid, LinkDTO>? OutLinkData { get; set; }
		public Dictionary<Guid, Guid>? InLinkNodeIdPairs { get; set; }
		// dictionary of (linkId, nodeId) pairs;
		// node may be multiple linked, should not be key of dictionary
		public Dictionary<Guid, ReferenceDTO>? OutReferenceData { get; set; }
		public Dictionary<Guid, Guid>? InReferenceNodeIdPairs { get; set; }
		// dictionary of (referenceId, NodeId) pairs;
		// node may be multiple linked, should not be key of dictionary
		#endregion
	}
	public class NoteSegmentDTO
	{
		public string? Text { get; set; }
		public Guid? ReferenceId { get; set; }
	}
	public class NoteDTO
	{
		public ENoteComposition? Composition { get; set; }
		public ENoteImportance? Importance { get; set; }
		public List<NoteSegmentDTO>? Segments { get; set; }
	}
	public class LinkDTO
	{
		public Guid? Id { get; set; }
		public Guid? SourceNodeId { get; set; } // will be used by target node to check back
		public Guid? TargetNodeId { get; set; }
		public ELinkType? LinkType { get; set; }
		public Dictionary<ELinkInfoIndex, string>? LinkInfo { get; set; }
	}
	public class ReferenceDTO
	{
		public Guid? Id { get; set; }
		public Guid? SourceNodeId { get; set; } // will be used by target node to check back
		public Guid? TargetNodeId { get; set; }
		public EDereferencerType? DereferencerType { get; set; }

	}
}