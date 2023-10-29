using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

#region Dependency
using BasicService.API;
using InteractionDirecting.API;
using InteractionDirecting.Application;

#endregion

namespace ServiceOrchestrating.Application
{
	public class ServiceRegistrar
	{
		IServiceCollection ServiceCollection { get; set; }
		IServiceProvider ServiceProvider { get; set; }
		public ServiceRegistrar()
		{
			ServiceCollection = BasicService.API.Host.ServiceCollection;
			ServiceProvider = BasicService.API.Host.ServiceProvider;

			#region Interactoin Directing
			ServiceCollection.AddSingleton<InteractionDirecting.API.IAPI, InteractionDirecting.API.OHS>();
			ServiceCollection.AddSingleton<EventBus>();
			ServiceCollection.AddSingleton<CommandQueryBus>();
			ServiceCollection.AddSingleton<UndoRedoDirector>();
			#endregion

			#region
			#endregion

			#region

			#endregion

			#region
			#endregion

			#region
			#endregion

			#region
			#endregion
		}
	}
}
