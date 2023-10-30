
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Dependency
using DTOs;
using InteractionDirecting.API;
using NoteTaking.Application;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace NoteTaking.API
{
	public interface IAPI
	{
		public Guid CreateNewNode();
		public Guid DeleteNode(Guid nodeId);
		public NodeDTO ReadNode(Guid nodeId);
		public void WriteNode(Guid nodeId, NodeDTO nodeDTO);
		public NoteDTO ReadNoteOfNode(Guid nodeId);
		public void WriteNoteOfNode(Guid nodeId, NoteDTO noteDTO, Dictionary<Guid, ReferenceDTO> newReferenceDTOPairs);
		public bool DoesReferencenRecurseInNode(Guid nodeId, Guid referenceNodeId);
		public LinkDTO ReadLinkOfNode(Guid nodeId, Guid linkId);
		public void WriteLinkOfNode(Guid nodeId, Guid linkId, LinkDTO linkDTO);
		public void AddLinkToNode(Guid nodeId, Guid linkId, LinkDTO linkDTO);
		public void RemoveLinkFromNode(Guid nodeId, Guid linkId);


	}
	public class API///:IAPI
	{
		protected NodeApplicationService NodeAS {  get; set; }

		public API(IServiceProvider serviceProvider)
		{
			NodeAS = serviceProvider.GetService<NodeApplicationService>();
		}
		



	}

}
