
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Dependency
using DTOs;
#endregion
namespace EvntObj // [Event Bus] Event Objects
{
	#region
	#endregion
	#region NoteTaking.NodeApplicationService
	public class NodeCreated
	{
		public Guid NodeId { get; set; }
	}
	public class NodeDeleted
	{
		public Guid NodeId { get; set;}
	}
	public class NodeRead
	{
		public Guid NodeId { get; set; }
		public NodeDTO NodeDTO { get; set; }
	}
	public class NodeWritten
	{
		public Guid NodeId { get; set;}	
		public NodeDTO NodeDTO { get; set; }
	}
	public class NoteRead
	{
		public Guid NodeId { get; set;}
		public NoteDTO NoteDTO { get; set;}
	}
	public class NoteWritten
	{
		public Guid NodeId { get; set;}
		public NoteDTO NoteDTO { get; set;}
	}
	#endregion
	#region
	#endregion
	#region
	#endregion

}
