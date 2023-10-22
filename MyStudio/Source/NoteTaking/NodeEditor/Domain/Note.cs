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
		public List<NoteSegment>? Segments { get; set; }
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
		}
		public NoteData() { } 
		public NoteData(NoteData? noteData)
		{
			if (noteData != null)
			{
				Write(noteData);
			}
		}
		public void PartiaWrite(NoteData noteData)
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
		public string? Text;
		public Guid? ReferenceNodeId;
		public NoteSegment(Guid referenceNodeId)
		{
			ReferenceNodeId = referenceNodeId;
		}
		public NoteSegment(string text)
		{
			Text = text;
		}
		
		public NoteSegment DeepCopy()
		{
			NoteSegment segment;
			if (ReferenceNodeId != null)
			{
				segment =  new NoteSegment((Guid)ReferenceNodeId);
			}
			else if (Text != null)
			{
				segment =  new NoteSegment(Text);
			}
			else // this will not happen
			{
				segment =  new NoteSegment("[Impossible Condition]");
			}
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
