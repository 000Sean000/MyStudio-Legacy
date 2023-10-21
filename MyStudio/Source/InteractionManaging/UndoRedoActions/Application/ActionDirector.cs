using System.Collections.Generic;
namespace InteractionManaging.Application
{


	/// <summary>
	/// Record commands in Stacks for Redo & Undo
	/// </summary>
	public class ActionDirector
	{
		protected Stack<IAction> _undoStack = new Stack<IAction>();
		protected Stack<IAction> _redoStack = new Stack<IAction>();

		public void Execute(IAction command)
		{
			command.Execute();
			_undoStack.Push(command);
			_redoStack.Clear(); // Clear redoStack when a new command is executed
		}

		public void Undo()
		{
			if (_undoStack.Count > 0)
			{
				IAction command = _undoStack.Pop();
				command.Undo();
				_redoStack.Push(command);
			}
		}

		public void Redo()
		{
			if (_redoStack.Count > 0)
			{
				IAction command = _redoStack.Pop();
				command.Execute();
				_undoStack.Push(command);
			}
		}
	}

}
