using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
#region Dependency
using NoteTaking.Domain;
using InteractionDirecting.API;
#endregion
namespace NoteTaking.Application
{
	public class NodeApplicationService
	{
		protected readonly IServiceProvider _serviceProvider;
		protected INodeRepository _nodeRepository;
		protected NodeDomainService _nodeDomainService;
		protected InteractionDirecting.API.IAPI _interactionAPI;
		public NodeApplicationService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
			_nodeRepository = serviceProvider.GetService<INodeRepository>();
			_nodeDomainService = new NodeDomainService(_nodeRepository);

			_interactionAPI = serviceProvider.GetService<InteractionDirecting.API.IAPI>();

		}





	}
}
