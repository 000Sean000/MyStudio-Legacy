using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlueprint
{
	public interface INodeAggregate
	{
		#region
		#endregion
		#region Holding References
		protected INote Note { get; set; }
		protected Dictionary<Guid, ILinkData> OutLinks { set; get; }
		protected Dictionary<Guid, ILinkData> InLinks { set; get; }

		#endregion

		#region Note Value Object
		public void WriteNoteData(INoteData data);
		public IReadOnlyNoteData ReadNoteData();
		#endregion

		#region Link Entities
		public Guid AddLink(ILinkData linkData);
		public void RemoveLink(Guid linkId);
		public void WriteLinkData(Guid linkId,ILinkData linkData);
		public IReadOnlyLinkData ReadLinkData(Guid linkId);
		#endregion
		
	}
}
