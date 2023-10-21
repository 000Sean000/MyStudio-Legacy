using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BasicService;


using InteractionManaging.Application;

namespace InteractionManaging
{
	// Open Host Service
	public static class OHS 
	{
		
		static OHS()
		{
			EBus = new EventBus();
			CQBus = new CommandQueryBus(BasicService.OHS.ServiceProvider);
			Director = new ActionDirector();
		}
		public static EventBus EBus;
		public static CommandQueryBus CQBus;
		public static ActionDirector Director;

	}
	#region CQRS
	public interface ICommand { }

	public interface IQuery<TResult> { }

	public interface ICommandHandler<TCommand> where TCommand : ICommand
	{
		void Handle(TCommand command);
	}

	public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
	{
		TResult Handle(TQuery query);
	}
	#endregion
	#region Action Director
	public interface IAction // Undo-able Action
	{
		void Execute();
		void Undo();
	}
	#endregion
}
