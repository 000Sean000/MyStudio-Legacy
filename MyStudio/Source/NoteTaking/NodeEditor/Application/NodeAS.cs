using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;



#region Dependency
using NoteTaking.Domain;
using InteractionDirecting.API;
using EvntObj;
using DTOs;
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
			_interactionAPI.EBusPublish(nodeWritten);

		}
		#endregion

		#region Note
		public NoteDTO ReadNoteOfNode(Guid nodeId)
		{
			NoteData noteData = _nodeDS.ReadNoteOfNode(nodeId);
			NoteDTO noteDTO = Mapper.Map<NoteDTO>(noteData);
			NoteRead noteRead = new NoteRead() { NodeId = nodeId, NoteDTO = noteDTO };
			_interactionAPI.EBusPublish(noteRead);
			return noteDTO;
		}
		public void WriteNoteOfNode(Guid nodeId, NoteData noteData, Dictionary<Guid, ReferenceData>? newReferenceDataPairs)
		{

		}
		#endregion

		#region Link 

		#endregion
	}
	#region 
	#endregion
	#region
	#endregion
	#region Command with Undo

	public class CreateNewNode : ICommandWithUndo
	{
		public Guid? NodeId { get; set; }
		public void Execute()
		{

		}
		public void Undo()
		{

		}
	}

	#endregion
}
