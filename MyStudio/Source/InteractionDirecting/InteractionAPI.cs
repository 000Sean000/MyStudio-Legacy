using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InteractionDirecting.Application;
using Microsoft.Extensions.DependencyInjection;

#region Dependency
//using BasicService.API;

#endregion

namespace InteractionDirecting.API
{
	// Open Host Service
	public interface IHosts
	{
		#region Event Bus
		public void EBusSubscribe<TEvent>(Action<TEvent> handler);
		public void EBusUnsubscribe<TEvent>(Action<TEvent> handler);
		public void EBusPublish<TEvent>(TEvent eventToPublish);
		#endregion
		#region CQRS

		#endregion
		#region Undo-Redo-Director
		public void NoteExecute(ICommandWithUndo command);
		public void NoteUndo();
		public void NoteRedo();
		#endregion
	}
	public class Host:IHosts
	{
		protected EventBus EBus {  get; set; }
		protected CommandQueryBus CQBus {  get; set; }
		protected UndoRedoDirector Director {  get; set; }

		public Host(IServiceProvider serviceProvider)
		{

			EBus = (EventBus)serviceProvider.GetServices<EventBus>();
			CQBus = (CommandQueryBus)serviceProvider.GetServices<CommandQueryBus>(); ;
			Director = (UndoRedoDirector)serviceProvider.GetServices<UndoRedoDirector>(); ;
			

			
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
	#region Undo/Redo Director
	
	#endregion
}
