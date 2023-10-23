using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BasicService;


using InteractionManaging.Application;
using Microsoft.Extensions.DependencyInjection;

namespace InteractionManaging
{
	// Open Host Service
	public class OHS 
	{
		protected EventBus EBus {  get; set; }
		protected CommandQueryBus CQBus {  get; set; }
		protected UndoRedoDirector Director {  get; set; }

		public OHS()
		{
			BasicService.OHS.Services.AddSingleton<EventBus>();
			BasicService.OHS.Services.AddSingleton<CommandQueryBus>();
			BasicService.OHS.Services.AddSingleton<UndoRedoDirector>();

			EBus = (EventBus)BasicService.OHS.ServiceProvider.GetServices<EventBus>();
			CQBus = (CommandQueryBus)BasicService.OHS.ServiceProvider.GetServices<CommandQueryBus>(); ;
			Director = (UndoRedoDirector)BasicService.OHS.ServiceProvider.GetServices<UndoRedoDirector>(); ;


			
		}
		#region Event Bus
		public void EBusSubscribe<TEvent>(Action<TEvent> handler)
		{
			EBus.Subscribe<TEvent>(handler);
		}
		public void EBusUnsubscribe<TEvent>(Action<TEvent> handler)
		{
			EBus.Unsubscribe<TEvent>(handler);
		}
		public void EBusPublish<TEvent>(TEvent eventToPublish)
		{
			EBus.Publish<TEvent>(eventToPublish);
		}
		#endregion
		#region CQRS

		#endregion
		#region Undo-Redo-Director
		public void NoteExecute(ICommandWithUndo command)
		{
			Director.Execute(command);
		}
		public void NoteUndo()
		{
			Director.Undo();
		}
		public void NoteRedo()
		{
			Director.Redo();
		}
		#endregion
		#region
		#endregion


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
	public interface ICommandWithUndo // Undo-able Action
	{
		void Execute();
		void Undo();
	}
	#endregion
}
