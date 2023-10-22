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
		public static EventBus EBus {  get; set; }
		public static CommandQueryBus CQBus {  get; set; }
		public static ActionDirector Director {  get; set; }

		static OHS()
		{
			EBus = new EventBus();
			CQBus = new CommandQueryBus(BasicService.OHS.ServiceProvider);
			Director = new ActionDirector();
		}
		

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
