using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;



#region Dependency
using NoteTaking.Domain;
using EvntObj;
using DTOs;
using InteractionDirecting.API;

#endregion

namespace NoteTaking.Application
{
	public class NodeProfile: Profile
	{
		public NodeProfile()
		{
			CreateMap<NodeData, NodeDTO>().ReverseMap();
			CreateMap<NoteSegment, NoteSegmentDTO>().ReverseMap();
			CreateMap<NoteData, NoteDTO>().ReverseMap();
			CreateMap<LinkData, LinkDTO>().ReverseMap();
			CreateMap<ReferenceData, ReferenceDTO>().ReverseMap();
		}
	}
	public class NodeApplicationService
	{
		protected readonly IServiceProvider _serviceProvider;
		protected INodeRepository _nodeRepository;
		protected NodeDomainService _nodeDS;
		protected InteractionDirecting.API.IAPI _interactionAPI;

		public IMapper Mapper { get; set; }
		
		public NodeApplicationService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
			_nodeRepository = serviceProvider.GetService<INodeRepository>();
			_nodeDS = new NodeDomainService(_nodeRepository);

			_interactionAPI = serviceProvider.GetService<InteractionDirecting.API.IAPI>();

			var mapperConfig = new MapperConfiguration(cfg => 
			{ 
				cfg.AddProfile<NodeProfile>(); 
			});
			Mapper = mapperConfig.CreateMapper();
		}

		#region Node		
		public Guid CreateNewNode()
		{
			Node node = _nodeDS.CreateNewNode();
			Guid nodeId = (Guid)node.Id;

			NodeCreated nodeCreated = new NodeCreated() { NodeId = nodeId };
			_interactionAPI.EBusPublish<NodeCreated>(nodeCreated);

			return nodeId;
		}
		public void DeleteNode(Guid nodeId)
		{
			_nodeDS.DeleteNode(nodeId);

			NodeDeleted nodeDeleted = new NodeDeleted()	{NodeId = nodeId };
			_interactionAPI.EBusPublish<NodeDeleted>(nodeDeleted);
		}
		public void RecoverNode(NodeDTO nodeDTO)
		{
			NodeData nodeData = Mapper.Map<NodeData>(nodeDTO);
			_nodeDS.RecoverNode(nodeData);
		}
		public NodeDTO ReadNode(Guid nodeId)
		{
			NodeData nodeData = _nodeDS.ReadNode(nodeId);
			NodeDTO nodeDTO = Mapper.Map<NodeDTO>(nodeData);
			NodeRead nodeRead = new NodeRead() { NodeId = nodeId, NodeDTO = nodeDTO};
			_interactionAPI.EBusPublish<NodeRead>(nodeRead);
			return nodeDTO;
		}
		public void WriteNode(Guid nodeId, NodeDTO nodeDTO)
		{
			NodeData nodeData = Mapper.Map<NodeData>(nodeDTO);
			_nodeDS.WriteNode(nodeId, nodeData);
			NodeWritten nodeWritten = new NodeWritten() { NodeId = nodeId, NodeDTO = nodeDTO };
			_interactionAPI.EBusPublish<NodeWritten>(nodeWritten);

		}
		#endregion

		#region Note
		public NoteDTO ReadNoteOfNode(Guid nodeId)
		{
			NoteData noteData = _nodeDS.ReadNoteOfNode(nodeId);
			NoteDTO noteDTO = Mapper.Map<NoteDTO>(noteData);
			NoteRead noteRead = new NoteRead() { NodeId = nodeId, NoteDTO = noteDTO };
			_interactionAPI.EBusPublish<NoteRead>(noteRead);
			return noteDTO;
		}
		public void WriteNoteOfNode(Guid nodeId, NoteDTO noteDTO, Dictionary<Guid, ReferenceDTO> newReferenceDTOPairs)
		{
			NoteData noteData = Mapper.Map<NoteData>(noteDTO);
			Dictionary<Guid, ReferenceData> newReferenceData = Mapper.Map<Dictionary<Guid, ReferenceData>>(newReferenceDTOPairs);
			_nodeDS.WriteNoteOfNode(nodeId, noteData, newReferenceData);
			NoteWritten noteWritten = new NoteWritten() { NodeId = nodeId, NoteDTO = noteDTO, NewReferenceDTOPairs = newReferenceDTOPairs };
			_interactionAPI.EBusPublish<NoteWritten>(noteWritten);

		}
		public bool DoesReferencenRecurseInNode(Guid nodeId, Guid referenceNodeId)
		{
			bool isRecursion = _nodeDS.DoesReferencenRecurseInNode(nodeId, referenceNodeId);
			ReferenceRecurses referenceRecurses = new ReferenceRecurses() { NodeId = nodeId, ReferenceNodeId = referenceNodeId };
			_interactionAPI.EBusPublish<ReferenceRecurses>(referenceRecurses);
			return isRecursion;
		}

		#endregion

		#region Link 
		public LinkDTO ReadLinkOfNode(Guid nodeId, Guid linkId)
		{
			LinkData linkData = _nodeDS.ReadLinkOfNode(nodeId, linkId);
			LinkDTO linkDTO = Mapper.Map<LinkDTO>(linkData);
			LinkRead linkRead = new LinkRead() { NodeId = nodeId, LinkId = linkId, LinkDTO = linkDTO };
			_interactionAPI.EBusPublish<LinkRead>(linkRead);
			return linkDTO;
		}
		public void WriteLinkOfNode(Guid nodeId, Guid linkId, LinkDTO linkDTO)
		{
			LinkData linkData = Mapper.Map<LinkData>(linkDTO);
			_nodeDS.WriteLinkOfNode(nodeId, linkId, linkData);
			LinkWritten linkWritten = new LinkWritten() { NodeId = nodeId, LinkId = linkId, LinkDTO = linkDTO };
			_interactionAPI.EBusPublish<LinkWritten>(linkWritten);
		}
		public void AddLinkToNode(Guid nodeId, Guid linkId, LinkDTO linkDTO)
		{
			LinkData linkData = Mapper.Map<LinkData>(linkDTO);
			_nodeDS.AddLinkToNode(nodeId, linkId, linkData);
			LinkAdded linkAdded = new LinkAdded() { NodeId = nodeId, LinkId = linkId, LinkDTO = linkDTO };
			_interactionAPI.EBusPublish<LinkAdded>(linkAdded);
		}
		public void RemoveLinkFromNode(Guid nodeId, Guid linkId)
		{
			_nodeDS.RemoveLinkFromNode(nodeId, linkId);
			LinkRemoved linkRemoved = new LinkRemoved() { NodeId = nodeId, LinkId = linkId };
			_interactionAPI.EBusPublish<LinkRemoved>(linkRemoved);
		}
		#endregion
	}
	#region 
	#endregion
	#region
	#endregion
	#region Command with Undo

	public class CreateNewNode : ICommandWithUndo
	{
		protected NodeApplicationService NodeAS { get; set; }
		public Guid NodeId { get; set; }
		public CreateNewNode(NodeApplicationService nodeAS)
		{
			NodeAS = nodeAS;
		}
		public void Execute()
		{
			NodeId = NodeAS.CreateNewNode();
		}
		public void Undo()
		{
			NodeAS.DeleteNode(NodeId);
		}
	}
	public class DeleteNode : ICommandWithUndo
	{
		protected NodeApplicationService NodeAS { get; set; }
		public Guid NodeId { get; set;}
		public NodeDTO NodeDTO { get; set; }
		public DeleteNode(NodeApplicationService nodeAS, Guid nodeId)
		{
			NodeAS = nodeAS;
			NodeId = nodeId;
		}
		public void Execute()
		{
			NodeDTO = NodeAS.ReadNode(NodeId);
			NodeAS.DeleteNode(NodeId);
		}
		public void Undo()
		{

		}
	}

	#endregion
}

/* old with mapper

namespace NoteTaking.Application
{
	public class NodeProfile: Profile
	{
		public NodeProfile()
		{
			CreateMap<NodeData, NodeDTO>().ReverseMap();
			CreateMap<NoteSegment, NoteSegmentDTO>().ReverseMap();
			CreateMap<NoteData, NoteDTO>().ReverseMap();
			CreateMap<LinkData, LinkDTO>().ReverseMap();
			CreateMap<ReferenceData, ReferenceDTO>().ReverseMap();
		}
	}
	public class NodeApplicationService
	{
		protected readonly IServiceProvider _serviceProvider;
		protected INodeRepository _nodeRepository;
		protected NodeDomainService _nodeDS;
		protected InteractionDirecting.API.IAPI _interactionAPI;

		public IMapper Mapper { get; set; }
		
		public NodeApplicationService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
			_nodeRepository = serviceProvider.GetService<INodeRepository>();
			_nodeDS = new NodeDomainService(_nodeRepository);

			_interactionAPI = serviceProvider.GetService<InteractionDirecting.API.IAPI>();

			var mapperConfig = new MapperConfiguration(cfg => 
			{ 
				cfg.AddProfile<NodeProfile>(); 
			});
			Mapper = mapperConfig.CreateMapper();
		}

		#region Node		
		public Guid CreateNewNode()
		{
			Node node = _nodeDS.CreateNewNode();
			Guid nodeId = (Guid)node.Id;

			NodeCreated nodeCreated = new NodeCreated() { NodeId = nodeId };
			_interactionAPI.EBusPublish<NodeCreated>(nodeCreated);

			return nodeId;
		}
		public void DeleteNode(Guid nodeId)
		{
			_nodeDS.DeleteNode(nodeId);

			NodeDeleted nodeDeleted = new NodeDeleted()	{NodeId = nodeId };
			_interactionAPI.EBusPublish<NodeDeleted>(nodeDeleted);
		}
		public void RecoverNode(NodeDTO nodeDTO)
		{
			NodeData nodeData = Mapper.Map<NodeData>(nodeDTO);
			_nodeDS.RecoverNode(nodeData);
		}
		public NodeDTO ReadNode(Guid nodeId)
		{
			NodeData nodeData = _nodeDS.ReadNode(nodeId);
			NodeDTO nodeDTO = Mapper.Map<NodeDTO>(nodeData);
			NodeRead nodeRead = new NodeRead() { NodeId = nodeId, NodeDTO = nodeDTO};
			_interactionAPI.EBusPublish<NodeRead>(nodeRead);
			return nodeDTO;
		}
		public void WriteNode(Guid nodeId, NodeDTO nodeDTO)
		{
			NodeData nodeData = Mapper.Map<NodeData>(nodeDTO);
			_nodeDS.WriteNode(nodeId, nodeData);
			NodeWritten nodeWritten = new NodeWritten() { NodeId = nodeId, NodeDTO = nodeDTO };
			_interactionAPI.EBusPublish<NodeWritten>(nodeWritten);

		}
		#endregion

		#region Note
		public NoteDTO ReadNoteOfNode(Guid nodeId)
		{
			NoteData noteData = _nodeDS.ReadNoteOfNode(nodeId);
			NoteDTO noteDTO = Mapper.Map<NoteDTO>(noteData);
			NoteRead noteRead = new NoteRead() { NodeId = nodeId, NoteDTO = noteDTO };
			_interactionAPI.EBusPublish<NoteRead>(noteRead);
			return noteDTO;
		}
		public void WriteNoteOfNode(Guid nodeId, NoteDTO noteDTO, Dictionary<Guid, ReferenceDTO> newReferenceDTOPairs)
		{
			NoteData noteData = Mapper.Map<NoteData>(noteDTO);
			Dictionary<Guid, ReferenceData> newReferenceData = Mapper.Map<Dictionary<Guid, ReferenceData>>(newReferenceDTOPairs);
			_nodeDS.WriteNoteOfNode(nodeId, noteData, newReferenceData);
			NoteWritten noteWritten = new NoteWritten() { NodeId = nodeId, NoteDTO = noteDTO, NewReferenceDTOPairs = newReferenceDTOPairs };
			_interactionAPI.EBusPublish<NoteWritten>(noteWritten);

		}
		public bool DoesReferencenRecurseInNode(Guid nodeId, Guid referenceNodeId)
		{
			bool isRecursion = _nodeDS.DoesReferencenRecurseInNode(nodeId, referenceNodeId);
			ReferenceRecurses referenceRecurses = new ReferenceRecurses() { NodeId = nodeId, ReferenceNodeId = referenceNodeId };
			_interactionAPI.EBusPublish<ReferenceRecurses>(referenceRecurses);
			return isRecursion;
		}

		#endregion

		#region Link 
		public LinkDTO ReadLinkOfNode(Guid nodeId, Guid linkId)
		{
			LinkData linkData = _nodeDS.ReadLinkOfNode(nodeId, linkId);
			LinkDTO linkDTO = Mapper.Map<LinkDTO>(linkData);
			LinkRead linkRead = new LinkRead() { NodeId = nodeId, LinkId = linkId, LinkDTO = linkDTO };
			_interactionAPI.EBusPublish<LinkRead>(linkRead);
			return linkDTO;
		}
		public void WriteLinkOfNode(Guid nodeId, Guid linkId, LinkDTO linkDTO)
		{
			LinkData linkData = Mapper.Map<LinkData>(linkDTO);
			_nodeDS.WriteLinkOfNode(nodeId, linkId, linkData);
			LinkWritten linkWritten = new LinkWritten() { NodeId = nodeId, LinkId = linkId, LinkDTO = linkDTO };
			_interactionAPI.EBusPublish<LinkWritten>(linkWritten);
		}
		public void AddLinkToNode(Guid nodeId, Guid linkId, LinkDTO linkDTO)
		{
			LinkData linkData = Mapper.Map<LinkData>(linkDTO);
			_nodeDS.AddLinkToNode(nodeId, linkId, linkData);
			LinkAdded linkAdded = new LinkAdded() { NodeId = nodeId, LinkId = linkId, LinkDTO = linkDTO };
			_interactionAPI.EBusPublish<LinkAdded>(linkAdded);
		}
		public void RemoveLinkFromNode(Guid nodeId, Guid linkId)
		{
			_nodeDS.RemoveLinkFromNode(nodeId, linkId);
			LinkRemoved linkRemoved = new LinkRemoved() { NodeId = nodeId, LinkId = linkId };
			_interactionAPI.EBusPublish<LinkRemoved>(linkRemoved);
		}
		#endregion
	}
	#region 
	#endregion
	#region
	#endregion
	#region Command with Undo

	public class CreateNewNode : ICommandWithUndo
	{
		protected NodeApplicationService NodeAS { get; set; }
		public Guid NodeId { get; set; }
		public CreateNewNode(NodeApplicationService nodeAS)
		{
			NodeAS = nodeAS;
		}
		public void Execute()
		{
			NodeId = NodeAS.CreateNewNode();
		}
		public void Undo()
		{
			NodeAS.DeleteNode(NodeId);
		}
	}
	public class DeleteNode : ICommandWithUndo
	{
		protected NodeApplicationService NodeAS { get; set; }
		public Guid NodeId { get; set;}
		public NodeDTO NodeDTO { get; set; }
		public DeleteNode(NodeApplicationService nodeAS, Guid nodeId)
		{
			NodeAS = nodeAS;
			NodeId = nodeId;
		}
		public void Execute()
		{
			NodeDTO = NodeAS.ReadNode(NodeId);
			NodeAS.DeleteNode(NodeId);
		}
		public void Undo()
		{

		}
	}

	#endregion
}

 */