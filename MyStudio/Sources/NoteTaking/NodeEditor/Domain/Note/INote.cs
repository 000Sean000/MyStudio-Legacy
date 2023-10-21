/*
 * Note: Value Object
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace INoteTaking
{
	public enum ENoteComposition
	{
		OnlyText, OnlySingleRef, Mixed
	}
	public enum ENoteImportance
	{
		ContextualLabel, EssentialData
	}
	public interface INoteDTO
	{
		public ENoteComposition? Composition { get; }
		public ENoteImportance? Importance { get; }
		public List<INoteSegment>? Segments { get; }
	}

	public interface INoteSegment
	{
		public string? Text { get; set; }
		public Guid? ReferenceNodeId { get; set; }
	}
	public interface INote
	{
		public ENoteComposition? Composition { get; set; }
		public ENoteImportance? Importance { get; set; }
		public List<INoteSegment>? Segments { get; set; }

		public INoteDTO ReadData { get; }
		public void InputData(INoteDTO data);
		public INoteDTO OutputData();
	}
	public interface INoteService
	{

		public string SegmentToString(INoteSegment segment);
		public INoteSegment ReferenceToSegment(Guid targetNodeId);
	}
}
