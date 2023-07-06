using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PKG
{
	// BUG:　new control point update latency
/*
 * Remember to set property "AutoSize" of the control to False!!!!!
 */
	public class ResizableControl
	{
		#region Fields
		public enum Edge
		{
			N, S, W, E, NW, NE, SW, SE, None
		}
		protected Control _control;
		protected Control originalParent;
		protected Cursor _originalCursor;
		protected Edge _edge = Edge.None;
		protected bool isResizing = false;
		protected Point _cursorPoint0; 
		protected int widthDiff;
		protected int heightDiff;
		public Action<Control> reAddControlAction;

		protected int margin; // Margin or sensitivity at the boundary
		protected Rectangle boundary;
		protected Rectangle northRegion;
		protected Rectangle southRegion;
		protected Rectangle westRegion;
		protected Rectangle eastRegion;
		protected Rectangle topLeftRegion;
		protected Rectangle topRightRegion;
		protected Rectangle bottomLeftRegion;
		protected Rectangle bottomRightRegion;
		#endregion

		public bool _mouseDown = false;
		public ResizableControl(Control control, Action<Control> reAddControlAction_ = null, int margin_ = 10)
		{
			_control = control;
			originalParent = control.Parent;
			reAddControlAction = reAddControlAction_;
			_originalCursor = new Cursor(control.Cursor.Handle);
			margin = margin_;

			DefineBoundary();
			_control.MouseDown += MouseDown;
			_control.MouseUp += MouseUp;
			_control.MouseMove += MouseMove;
			_control.MouseLeave += MouseLeave;
		}
		public void DefineBoundary()
		{
			boundary = _control.ClientRectangle;
			// Define the regions for each side of the boundary
			northRegion = new Rectangle(boundary.Left + margin, boundary.Top, boundary.Width - (2 * margin), margin);
			southRegion = new Rectangle(boundary.Left + margin, boundary.Bottom - margin, boundary.Width - (2 * margin), margin);
			westRegion = new Rectangle(boundary.Left, boundary.Top + margin, margin, boundary.Height - (2 * margin));
			eastRegion = new Rectangle(boundary.Right - margin, boundary.Top + margin, margin, boundary.Height - (2 * margin));
			topLeftRegion = new Rectangle(boundary.Left, boundary.Top, margin, margin);
			topRightRegion = new Rectangle(boundary.Right - margin, boundary.Top, margin, margin);
			bottomLeftRegion = new Rectangle(boundary.Left, boundary.Bottom - margin, margin, margin);
			bottomRightRegion = new Rectangle(boundary.Right - margin, boundary.Bottom - margin, margin, margin);
		}
		public void RecoverCursor()
		{
			_control.Cursor = new Cursor(_originalCursor.Handle);
		}
		protected void MouseDown(object sender, MouseEventArgs e)
		{
			// Check if the mouse is near the boundary of the container
			if (IsCursorNearBoundary())
			{
				isResizing = true;
				_cursorPoint0 = new Point(e.X, e.Y);
			}
			///_control.Text = _cursorPoint0.ToString() +'\n'+ e.Location.ToString();

		}
		protected void MouseMove(object sender, MouseEventArgs e)
		{
			if (isResizing)
			{
				// Calculate the size difference based on the mouse movement
				widthDiff = e.X - _cursorPoint0.X;
				heightDiff = e.Y - _cursorPoint0.Y;
				
				// Adjust the size of the resizable block control
				if (_edge == Edge.N)
				{
					_control.Location = new Point(_control.Location.X, _control.Location.Y + heightDiff);
					_control.Height -= heightDiff;
				}
				else if (_edge == Edge.NW)
				{
					_control.Location = new Point(_control.Location.X + widthDiff, _control.Location.Y + heightDiff);
					_control.Width -= widthDiff;
					_control.Height -= heightDiff;
				}
				else if (_edge == Edge.W)
				{
					_control.Location = new Point(_control.Location.X + widthDiff, _control.Location.Y);
					_control.Width -= widthDiff;
				}
				else if (_edge == Edge.SW)
				{
					_control.Location = new Point(_control.Location.X + widthDiff, _control.Location.Y);
					_control.Width -= widthDiff;
					_control.Height += heightDiff;
					_cursorPoint0 = new Point(_cursorPoint0.X, e.Y);
				}
				else if (_edge == Edge.S)
				{
					_control.Height += heightDiff;
					_cursorPoint0 = new Point(_cursorPoint0.X, e.Y);
				}
				else if (_edge == Edge.SE)
				{
					_control.Width += widthDiff;
					_control.Height += heightDiff;
					_cursorPoint0 = new Point(e.X, e.Y);
				}
				else if (_edge == Edge.E)
				{
					_control.Width += widthDiff;
					_cursorPoint0 = new Point(e.X, _cursorPoint0.Y);
				}
				else if (_edge == Edge.NE)
				{
					_control.Location = new Point(_control.Location.X, _control.Location.Y + heightDiff);
					_control.Width += widthDiff;
					_control.Height -= heightDiff;
					_cursorPoint0 = new Point(e.X, _cursorPoint0.Y);
				}
				//_control.Text = "mouse:" + e.Location.ToString() + " | control:" + _control.Location.ToString();
				_control.Text = _cursorPoint0.ToString() + '\n' + e.Location.ToString();
				/*
				 * adding the resized control back to the container after updating its size and position.
				 * 
				 */
				if (reAddControlAction != null)
				{
					reAddControlAction(_control);
				}
			}
			else
			{
				ChangeCursorAtBoundary(e.Location);
			}
		}

		protected void MouseUp(object sender, MouseEventArgs e)
		{
			isResizing = false;
			RecoverCursor();
			DefineBoundary();
			_mouseDown = false;
		}

		protected void MouseLeave(object sender, EventArgs e)
		{
			RecoverCursor();
		}
		protected void ChangeCursorAtBoundary(Point mousePosition)
		{
			if (northRegion.Contains(mousePosition))
			{
				// Mouse cursor is at the north side
				_control.Cursor = Cursors.SizeNS;
				_edge = Edge.N;
			}
			else if (southRegion.Contains(mousePosition))
			{
				// Mouse cursor is at the south side
				_control.Cursor = Cursors.SizeNS;
				_edge = Edge.S;
			}
			else if (westRegion.Contains(mousePosition))
			{
				// Mouse cursor is at the west side
				_control.Cursor = Cursors.SizeWE;
				_edge = Edge.W;
			}
			else if (eastRegion.Contains(mousePosition))
			{
				// Mouse cursor is at the east side
				_control.Cursor = Cursors.SizeWE;
				_edge = Edge.E;
			}
			else if (topLeftRegion.Contains(mousePosition))
			{
				// Mouse cursor is at the top-left corner
				_control.Cursor = Cursors.SizeNWSE;
				_edge = Edge.NW;
			}
			else if (topRightRegion.Contains(mousePosition))
			{
				// Mouse cursor is at the top-right corner
				_control.Cursor = Cursors.SizeNESW;
				_edge = Edge.NE;
			}
			else if (bottomLeftRegion.Contains(mousePosition))
			{
				// Mouse cursor is at the bottom-left corner
				_control.Cursor = Cursors.SizeNESW;
				_edge = Edge.SW;
			}
			else if (bottomRightRegion.Contains(mousePosition))
			{
				// Mouse cursor is at the bottom-right corner
				_control.Cursor = Cursors.SizeNWSE;
				_edge = Edge.SE;
			}
			else
			{
				_control.Cursor = Cursors.Hand;
				_edge = Edge.None;
			}
		}
		protected bool IsCursorNearBoundary()
		{
			if (_edge != Edge.None)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/* keep
		 protected void MouseDown(object sender, MouseEventArgs e)
		{
			// Check if the mouse is near the boundary of the container
			if (IsCursorNearBoundary() && !isResizing)
			//if (IsCursorNearBoundary())
			{
				isResizing = true;
				//_cursorPoint0 = e.Location;
				_cursorPoint0 = new Point(e.X, e.Y);
				_control.Location = new Point(_control.Location.X, _control.Location.Y);
				Width0 = _control.Width;
				Height0 = _control.Height;
			}
			_mouseDown = true;
			//_control.Text = "mouse:" + e.Location.ToString() + " | control:" + _control.Location.ToString();
			_control.Text = _cursorPoint0.ToString() +'\n'+ e.Location.ToString();

		}

		protected void MouseMove(object sender, MouseEventArgs e)
		{
			
			if (isResizing)
			{
				// Calculate the size difference based on the mouse movement

				int widthDiff = e.X - _cursorPoint0.X;
				int heightDiff = e.Y - _cursorPoint0.Y;
				
				// Adjust the size of the resizable block control
				if (_edge == Edge.N)
				{
					_control.Location = new Point(_control.Location.X, _control.Location.Y + heightDiff);
					_control.Height = Height0 - heightDiff;
				}
				else if (_edge == Edge.NW)
				{
					_control.Location = new Point(_control.Location.X + widthDiff, _control.Location.Y + heightDiff);
					_control.Width = Width0 - widthDiff;
					_control.Height = Height0 - heightDiff;
				}
				else if (_edge == Edge.W)
				{
					_control.Location = new Point(_control.Location.X + widthDiff, _control.Location.Y);
					_control.Width = Width0 - widthDiff;
				}
				else if (_edge == Edge.SW)
				{
					_control.Location = new Point(_control.Location.X + widthDiff, _control.Location.Y);
					_control.Width = Width0 - widthDiff;
					_control.Height = Height0 + heightDiff;
				}
				else if (_edge == Edge.S)
				{
					_control.Height = Height0 + heightDiff;
				}
				else if (_edge == Edge.SE)
				{
					_control.Width = Width0 + widthDiff;
					_control.Height = Height0 + heightDiff;
				}
				else if (_edge == Edge.E)
				{
					_control.Width = Width0 + widthDiff;
				}
				else if (_edge == Edge.NE)
				{
					_control.Location = new Point(_control.Location.X, _control.Location.Y + heightDiff);
					_control.Width = Width0 + widthDiff;
					_control.Height = Height0 + heightDiff;
				}
				//_control.Text = "mouse:" + e.Location.ToString() + " | control:" + _control.Location.ToString();
				_control.Text = _cursorPoint0.ToString() + '\n' + e.Location.ToString();

			}
			else
			{
				if (_mouseDown)
				{
					int widthDiff = e.X - _cursorPoint0.X;
					int heightDiff = e.Y - _cursorPoint0.Y;
					_control.Text = e.Location.ToString();
					// Adjust the size of the resizable block control
					_control.Location = new Point(_control.Location.X + widthDiff, _control.Location.Y + heightDiff);

				}
				// only update cursor type when the boundary is not changing
				ChangeCursorAtBoundary(e.Location);
			}
		}

		protected void MouseUp(object sender, MouseEventArgs e)
		{
			isResizing = false;
			RecoverCursor();
			DefineBoundary();
			_mouseDown = false;
		}

		 */
	}

	public class DraggableCanvas
	{
		protected Point _dragStartPoint;
		protected Point _canvasStartPoint;
		protected bool _isDragging;
		protected bool _readyToDraw = false;
		protected float _sensitivity;

		public Panel _panel;
		public Label canvasX = new Label();
		public Label canvasY = new Label();
		public Label canvasX0 = new Label();
		public Label canvasY0 = new Label();
		public Label mouseX = new Label();
		public Label mouseY = new Label();
		public Label mouseX0 = new Label();
		public Label mouseY0 = new Label();
		public DraggableCanvas(Panel panel, float sensitivity = 1)
		{
			_panel = panel;
			_sensitivity = sensitivity;
			_panel.MouseDown += MouseDown;
			_panel.MouseUp += MouseUp;
			_panel.MouseMove += MouseMove;
		}
		public void MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				_dragStartPoint = new Point(e.X, e.Y);
				_isDragging = true;


				Point currentPosition = _panel.AutoScrollPosition;
				_canvasStartPoint = new Point(currentPosition.X, currentPosition.Y);
				canvasX0.Text = currentPosition.X.ToString();
				canvasY0.Text = currentPosition.Y.ToString();
				mouseX.Text = e.X.ToString();
				mouseY.Text = e.Y.ToString();
				mouseX0.Text = _dragStartPoint.X.ToString();
				mouseY0.Text = _dragStartPoint.Y.ToString();
			}
		}

		public void MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				_isDragging = false;
				/*
				Point currentPosition = _panel.AutoScrollPosition;
				_panel.AutoScrollPosition = new Point(
					-_canvasStartPoint.X - (int)((e.X - _dragStartPoint.X) * 1),
					-_canvasStartPoint.Y - (int)((e.Y - _dragStartPoint.Y) * 1));
				currentPosition = _panel.AutoScrollPosition; // read the AutoScrollPosition again
				canvasX.Text = currentPosition.X.ToString();
				canvasY.Text = currentPosition.Y.ToString();
				*/
			}
		}

		public void MouseMove(object sender, MouseEventArgs e)
		{
			if (_isDragging)
			{

				_panel.AutoScrollPosition = new Point(
					-_canvasStartPoint.X - (int)((e.X - _dragStartPoint.X) * _sensitivity),
					-_canvasStartPoint.Y - (int)((e.Y - _dragStartPoint.Y) * _sensitivity));
				Point currentPosition = _panel.AutoScrollPosition;
				canvasX.Text = currentPosition.X.ToString();
				canvasY.Text = currentPosition.Y.ToString();
				mouseX.Text = e.X.ToString();
				mouseY.Text = e.Y.ToString();


			}
		}
	}

}
