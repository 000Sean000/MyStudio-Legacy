using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaking.Domain
{
	public enum ENoteComposition
	{
		OnlyText, OnlySingleRef, Mixed
	}
	public enum ENoteImportance
	{
		ContextualLabel, EssentialData
	}
	public class NoteDTO
	{
		public ENoteComposition Composition;
		public ENoteImportance Importance;
		public List<NoteSegment> Segments;

		public NoteDTO() { }
		public NoteDTO(NoteDTO noteDTO)
		{
			noteDTO = noteDTO.DeepCopy(); // pure data transfer
			Composition = noteDTO.Composition;
			Importance = noteDTO.Importance;
			Segments = noteDTO.Segments;
		}
		public NoteDTO(ENoteComposition composition, ENoteImportance importance, List<NoteSegment>? segments)
		{
			Composition = composition;
			Importance = importance;
			Segments = segments;
		}
		public NoteDTO DeepCopy()
		{
			List<NoteSegment> segments = new List<NoteSegment>();
			foreach (var segment in Segments)
			{
				Segments.Add(segment.DeepCopy());
			}
			return new NoteDTO(Composition, Importance, segments);
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
	public class Note: NoteDTO
	{
		public Note(NoteDTO noteDTO):base(noteDTO)
		{
			
		}
		public NoteDTO ReadData { get; }
		public void InputData(NoteDTO data)
		{

		}
		public NoteDTO OutputData()
		{
			NoteDTO data = new NoteDTO(Composition, Importance, Segments);
			return data.DeepCopy();////
		}
	}
	public class NoteService
	{

		public string SegmentToString(NoteSegment segment)
		{
			return "";////
		}
		public NoteSegment ReferenceToSegment(Guid targetNodeId)
		{
			
			
			return new NoteSegment("");////
		}
	}
}
