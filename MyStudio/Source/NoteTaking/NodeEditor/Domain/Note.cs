using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaking.Domain
{
	public enum ENoteComposition
	{
		Mixed, OnlyText, OnlySingleRef 
	}
	public enum ENoteImportance
	{
		EssentialData, ContextualLabel
	}
	public class NoteData
	{
		public ENoteComposition? Composition { get; set; }
		public ENoteImportance? Importance { get; set; }
		protected List<NoteSegment>? _segments;
		public List<NoteSegment>? Segments 
		{	
			get
			{
				return _segments;
			}
			set
			{
				_segments = value;
				/// use event bus to update ReferedByNodeIds & expire dereference
				List<Guid>? referringToNodeIds = new List<Guid>();
				foreach (var seg in _segments)
				{
					Guid? referenceNodeId = seg.ReferenceNodeId;
					if (referenceNodeId != null)
					{
						referringToNodeIds.Add((Guid)referenceNodeId);
					}
				}
				ReferringToNodeIds = referringToNodeIds;
			}
		}
		public List<Guid>? ReferringToNodeIds { get; set; }
		public List<Guid>? ReferredByNodeIds { get; set; }

		public void EnsurePropertyNotNull()
		{
			if (Composition == null)
			{
				Composition = default(ENoteComposition);
			}
			if (Importance == null)
			{
				Importance = default(ENoteImportance);
			}
			if (Segments == null)
			{
				Segments = new List<NoteSegment>();
				Segments.Add(new NoteSegment(""));
			}
			if (ReferringToNodeIds == null)
			{
				ReferringToNodeIds = new List<Guid>();
			}
			if (ReferredByNodeIds == null)
			{
				ReferredByNodeIds = new List<Guid>();
			}
		}
		public NoteData() { } 
		public NoteData(NoteData? noteData)
		{
			if (noteData != null)
			{
				Write(noteData);
			}
		}
		// Dereference need to be done in application service
		public void ExpireDereference()
		{
			foreach (var seg in Segments)
			{
				if (seg.ReferenceNodeId != null)
				{
					seg.Text = string.Empty;
				}
			}
		}
		public void PartialWrite(NoteData noteData)
		{
			noteData = noteData.DeepCopy(); 
			if (noteData.Composition != null)
			{
				Composition = noteData.Composition;
			}
			if (noteData.Importance != null)
			{
				Importance = noteData.Importance;
			}
			if (noteData.Segments != null)
			{
				Segments = noteData.Segments;
			}
		}
		public void Write(NoteData noteData)
		{

			noteData = noteData.DeepCopy();
			Composition = noteData.Composition;
			Importance = noteData.Importance;
			Segments = noteData.Segments;

			
		}

		public NoteData Read()
		{
			return DeepCopy();
		}
		public NoteData DeepCopy()
		{
			NoteData noteData = new NoteData();

			noteData.Composition = Composition;
			noteData.Importance = Importance;
			if (Segments == null)
			{
				noteData.Segments = null;
			}
			else
			{
				noteData.Segments = new List<NoteSegment>();
				foreach (var segment in Segments)
				{
					noteData.Segments.Add(segment.DeepCopy());
				}
			}
			return noteData;
		}
		
	}

	public class NoteSegment
	{
		public string? Text {  get; set; }
		public Guid? ReferenceNodeId {  get; set; }
		public NoteSegment()
		{

		}
		public NoteSegment(Guid referenceNodeId)
		{
			ReferenceNodeId = referenceNodeId;
		}
		public NoteSegment(string text)
		{
			Text = text;
		}
		public NoteSegment(NoteSegment segment)
		{
			Text = segment.Text;
			ReferenceNodeId = segment.ReferenceNodeId;
		}
		
		public NoteSegment DeepCopy()
		{
			NoteSegment segment = new NoteSegment()
			{
				Text = this.Text,
				ReferenceNodeId = this.ReferenceNodeId
			};
			return segment;
		}
		
	}
	public class Note: NoteData
	{
		public Note() : base()
		{
			EnsurePropertyNotNull();
		}
		public Note(NoteData? noteData):base(noteData)
		{
			EnsurePropertyNotNull();
		}
	}
	public class NoteService
	{

		public string SegmentToString(NoteSegment segment)
		{
			return "";////
		}
	}
}
