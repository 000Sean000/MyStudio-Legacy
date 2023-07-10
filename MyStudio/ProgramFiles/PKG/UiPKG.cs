using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;



namespace PKG
{
	public static class UiPKG
	{
		public static FlexControl SetControlResizable(Control control_, int thickness_ = 10) { return new FlexControl(control_, thickness_); }
		public static DraggableCanvas SetCanvasDaggable(Panel panel, float sensitivity = 1) { return new DraggableCanvas(panel, sensitivity); }
		//public static SuperPictureBox SetPictureBoxFitImage(PictureBox pictureBox_) { return new SuperPictureBox(pictureBox_); }
		
	}
	/*
	public class SuperPictureBox
	{
		PictureBox pictureBox;
		float imageRatio;
		Size originalBoxSize;

		public object sizeLock;

		public SuperPictureBox(PictureBox pictureBox_, object sizeLock_ = null)
		{
			pictureBox = pictureBox_;
			sizeLock = sizeLock_;
			if (sizeLock == null) { sizeLock = new object(); }
			imageRatio = ((float)pictureBox.Image.Size.Height / (float)pictureBox.Image.Size.Width);
			originalBoxSize = new Size(pictureBox.Size.Width, pictureBox.Size.Height);
			FitZoomedImage();

			pictureBox.SizeChanged += SizeChanged;
		}
		public void FitZoomedImage()
		{
			Size 

			lock (sizeLock)
			{
				pictureBox.Size = new Size((int)(pictureBox.Size.Width), (int)(pictureBox.Size.Width * imageRatio));
				Debug.WriteLine("box:" + pictureBox.Size.ToString() + "  image:" + pictureBox.Image.Size.ToString());
			}
		}
		public void SizeChanged(object sender, EventArgs e)
		{
			pictureBox.SizeChanged -= SizeChanged;
			FitZoomedImage();
			pictureBox.SizeChanged += SizeChanged;
		}
	}
	*/
	public class ResizablePictureBox: FlexControl
	{
		PictureBox pictureBox;
		float imageRatio;
		public ResizablePictureBox(PictureBox pictureBox_): base(pictureBox_)
		{
			pictureBox = pictureBox_;
			imageRatio = ((float)pictureBox.Image.Size.Height / (float)pictureBox.Image.Size.Width);
			if (pictureBox.Size.Height > pictureBox.Size.Width * imageRatio)
			{
				StretchHeight(); 
			}
			else
			{
				StretchWidth();
			}
			
			
		}
		public void StretchWidth()
		{
			pictureBox.Size = new Size((int)(pictureBox.Size.Height / imageRatio), (int)(pictureBox.Size.Height));
		}
		public void StretchHeight()
		{
			pictureBox.Size = new Size((int)(pictureBox.Size.Width), (int)(pictureBox.Size.Width * imageRatio));
		}
		public void MoveVertically()
		{
			int diff = (int)( pictureBox.Size.Width * imageRatio) - pictureBox.Size.Height;
			pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y - diff);
		}
		public void MoveHorizontally()
		{
			int diff = (int)(pictureBox.Size.Height / imageRatio) - pictureBox.Size.Width;
			pictureBox.Location = new Point(pictureBox.Location.X - diff, pictureBox.Location.Y);
		}
		public void FitZoomedImage(object sender, MouseEventArgs e)
		{
			if (isResizing)
			{
				if (cursorLocation == Border.S || cursorLocation == Border.SE)
				{
					StretchHeight();
				}
				else if (cursorLocation == Border.E)
				{
					StretchWidth();
				}
				else if (cursorLocation == Border.N || cursorLocation == Border.NE)
				{
					MoveVertically();
					StretchWidth();
				}
				else if (cursorLocation == Border.W || cursorLocation == Border.SW)
				{
					MoveHorizontally();
					StretchHeight();
				}
				else if (cursorLocation == Border.NW)
				{
					if (heightDiff != 0 && widthDiff != 0)
					{
						MoveVertically();
						StretchWidth();
					}
					else if (heightDiff != 0 && widthDiff == 0)
					{
						MoveVertically();
						StretchWidth();
					}
					else if (heightDiff == 0 && widthDiff != 0)
					{
						MoveHorizontally();
						StretchHeight();
					}
					else { } // no diff
				}
			}
			//originalBoxSize = new(pictureBox.Size.Width, pictureBox.Size.Height);
			Debug.WriteLine("box:" + pictureBox.Size.ToString() + "  image:" + pictureBox.Image.Size.ToString());
			
		}

		protected  void CalculateDiff2()
		//protected override void CalculateDiff()
		{
			widthDiff = horizontalMove;
			heightDiff = verticalMove;
			// fit image ratio
			Debug.WriteLine("imageRatio: " + imageRatio);
			Debug.WriteLine("Ratio: " + (float)preHeight / (float)preWidth);
			if (cursorLocation == Border.N || cursorLocation == Border.S || cursorLocation == Border.NW || cursorLocation == Border.SE)
			{
				widthDiff = (int)((float)(preHeight + heightDiff) / imageRatio - (float)preWidth);
			}
			else if (cursorLocation == Border.W || cursorLocation == Border.E || cursorLocation == Border.SW || cursorLocation == Border.NE)
			{
				heightDiff = (int)((float)(preWidth + widthDiff) * imageRatio - (float)preHeight);
			}
			/* border near N or W need opposite diff
			 * positive diff => grow
			 * negative diff => shrink
			 */
			if (cursorLocation == Border.NW)
			{
				widthDiff = -widthDiff;
				heightDiff = -heightDiff;
			}
			else if (cursorLocation == Border.N || cursorLocation == Border.NE)
			{
				heightDiff = -heightDiff;
			}
			else if (cursorLocation == Border.W || cursorLocation == Border.SW)
			{
				widthDiff = -widthDiff;
			}
			// change sizing type

			if (cursorLocation == Border.N)
			{
				cursorLocation = Border.NW;
				control.Cursor = Cursors.SizeNWSE;
			}
			else if (cursorLocation == Border.W)
			{
				cursorLocation = Border.SW;
				control.Cursor = Cursors.SizeNESW;
			}
			else if (cursorLocation == Border.S)
			{
				cursorLocation = Border.SE;
				control.Cursor = Cursors.SizeNWSE;
			}
			else if (cursorLocation == Border.E)
			{
				cursorLocation = Border.NE;
				control.Cursor = Cursors.SizeNESW;
			}

			// check basic Size
			if (preHeight + heightDiff >= control.MinimumSize.Height)
			{
				Debug.WriteLine("preHeight: " + preHeight + " heightDiff: " + heightDiff);
				heightDiff = 0;
			}
			if (preWidth + widthDiff <= control.MinimumSize.Width)
			{
				Debug.WriteLine("preWidth: " + preWidth + " widthDiff: " + widthDiff);
				widthDiff = 0;
			}
			Debug.WriteLine("widthDiff: " + widthDiff + "  heightDiff: " + heightDiff);
		}
		
		/*
		protected override void MouseMove(object sender, MouseEventArgs e)
		{
			if (isResizing)
			{
				{
					// Calculate the size difference based on the mouse movement
					preWidth = control.Width;
					preHeight = control.Height;
					horizontalMove = e.X - preCursorPoint.X;
					verticalMove = e.Y - preCursorPoint.Y;
					CalculateDiff();
					ResizeControl(e);
					
				}

			}
			else
			{
				ChangeCursorAtBoundary(e.Location);
			}
		}
		*/
	}
	/*
	 * Remember to set property "AutoSize" of the control to False!!!!!
	 */

	public class FlexControl
	{
		#region Fields
		public enum Border
		{
			N, S, W, E, NW, NE, SW, SE, None
		}
		public Control control;
		//protected Control originalParent;
		protected Cursor originalCursor;
		protected Border cursorLocation = Border.None;
		protected bool isResizing = false;
		protected Point preCursorPoint; // previous cursor point
		protected Point dragingStartPoint;
		protected int verticalMove, horizontalMove; // mouse movement
		protected int preWidth, preHeight; // previous W/H
		protected int widthDiff;
		protected int heightDiff;

		protected int thickness; // border thickness
		protected int basicWidth;
		protected int basicHeight;
		protected bool vertical;
		protected Rectangle border;
		protected Rectangle borderN;
		protected Rectangle borderS;
		protected Rectangle borderW;
		protected Rectangle borderE;
		protected Rectangle borderNW;
		protected Rectangle borderNE;
		protected Rectangle borderSW;
		protected Rectangle borderSE;
		#endregion

		public bool isDragging = false;
		public FlexControl(Control control_, int thickness_ = 10)
		{
			control = control_;
			//originalParent = control_.Parent;
			originalCursor = new Cursor(control_.Cursor.Handle);
			thickness = thickness_;
			basicWidth = 3 * thickness;
			basicHeight = 3 * thickness;
			if (control.MinimumSize.Width > basicWidth)
			{
				basicWidth = control.MinimumSize.Width;
			}
			if (control.MinimumSize.Height > basicHeight)
			{
				basicHeight = control.MinimumSize.Height;
			}
			control.MinimumSize = new Size(basicWidth, basicHeight);
			DefineBoundary();
			control.MouseDown += MouseDown;
			control.MouseUp += MouseUp;
			control.MouseMove += MouseMove;
			control.MouseLeave += MouseLeave;
		}
		public void DefineBoundary()
		{
			border = control.ClientRectangle;
			// Define the regions for each side of the border
			borderN = new Rectangle(border.Left + thickness, border.Top, border.Width - (2 * thickness), thickness);
			borderS = new Rectangle(border.Left + thickness, border.Bottom - thickness, border.Width - (2 * thickness), thickness);
			borderW = new Rectangle(border.Left, border.Top + thickness, thickness, border.Height - (2 * thickness));
			borderE = new Rectangle(border.Right - thickness, border.Top + thickness, thickness, border.Height - (2 * thickness));
			borderNW = new Rectangle(border.Left, border.Top, thickness, thickness);
			borderNE = new Rectangle(border.Right - thickness, border.Top, thickness, thickness);
			borderSW = new Rectangle(border.Left, border.Bottom - thickness, thickness, thickness);
			borderSE = new Rectangle(border.Right - thickness, border.Bottom - thickness, thickness, thickness);
		}
		public void RecoverCursor()
		{
			control.Cursor = new Cursor(originalCursor.Handle);
		}

		protected virtual void ResizeControl(MouseEventArgs e)
		{
			// Adjust the size of the resizable block control_
			if (cursorLocation == Border.N)
			{
				control.Location = new Point(control.Location.X, control.Location.Y - heightDiff);
				control.Height += heightDiff;
			}
			else if (cursorLocation == Border.NW)
			{
				control.Location = new Point(control.Location.X - widthDiff, control.Location.Y - heightDiff);
				control.Width += widthDiff;
				control.Height += heightDiff;
			}
			else if (cursorLocation == Border.W)
			{
				control.Location = new Point(control.Location.X - widthDiff, control.Location.Y);
				control.Width += widthDiff;
			}
			else if (cursorLocation == Border.SW)
			{
				control.Location = new Point(control.Location.X - widthDiff, control.Location.Y);
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(preCursorPoint.X, e.Y);
			}
			else if (cursorLocation == Border.S)
			{
				control.Height += heightDiff;
				preCursorPoint = new Point(preCursorPoint.X, e.Y);
			}
			else if (cursorLocation == Border.SE)
			{
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(e.X, e.Y);
			}
			else if (cursorLocation == Border.E)
			{
				control.Width += widthDiff;
				preCursorPoint = new Point(e.X, preCursorPoint.Y);
			}
			else if (cursorLocation == Border.NE)
			{
				control.Location = new Point(control.Location.X, control.Location.Y - heightDiff);
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(e.X, preCursorPoint.Y);
			}
			//Thread.Sleep(1000);
			Debug.WriteLine("Resize to " + control.Size.ToString() +" at " + control.Location.ToString());
		}
		protected virtual void CalculateDiff()
		{
			widthDiff = horizontalMove;
			heightDiff = verticalMove;
			/* border near N or W need opposite diff
			 * positive diff => grow
			 * negative diff => shrink
			 */
			if (cursorLocation == Border.NW)
			{
				widthDiff = -widthDiff;
				heightDiff = -heightDiff;
			}
			else if (cursorLocation == Border.N || cursorLocation == Border.NE) 
			{ 
				heightDiff = -heightDiff; 
			}
			else if (cursorLocation == Border.W || cursorLocation == Border.SW) 
			{ 
				widthDiff = -widthDiff; 
			}
			// check basic Size
			if (preHeight + heightDiff <= control.MinimumSize.Height)
			{
				heightDiff = 0;
			}
			if (preWidth + widthDiff <= control.MinimumSize.Width)
			{
				widthDiff = 0;
			}
			Debug.WriteLine("widthDiff: " + widthDiff+ "  heightDiff: " + heightDiff);
		}

		protected virtual void DragControl(int horizontalMove, int verticalMove)
		{
			control.Location = new Point(control.Location.X + horizontalMove, control.Location.Y + verticalMove);
		}
		protected virtual void MouseMove(object sender, MouseEventArgs e)
		{
			if (isResizing)
			{
				// Calculate the size difference based on the mouse movement
				preWidth = control.Width;
				preHeight = control.Height;
				horizontalMove = e.X - preCursorPoint.X;
				verticalMove = e.Y - preCursorPoint.Y;
				CalculateDiff();
				ResizeControl(e);
			}
			else if (isDragging)
			{
				horizontalMove = e.X - dragingStartPoint.X;
				verticalMove = e.Y - dragingStartPoint.Y;
				DragControl(horizontalMove, verticalMove);
				//preCursorPoint = e.Location;
			}
			else
			{
				ChangeCursorAtBoundary(e.Location);
			}
		}
		protected void MouseDown(object sender, MouseEventArgs e)
		{
			// Check if the mouse is near the border of the container
			if (IsCursorNearBoundary())
			{
				isResizing = true;
				preCursorPoint = new Point(e.X, e.Y);
			}
			else
			{
				isDragging = true;
				dragingStartPoint = new Point(e.X, e.Y);
			}
			
		}
		protected virtual void MouseUp(object sender, MouseEventArgs e)
		{
			isResizing = false;
			isDragging = false;
			RecoverCursor();
			DefineBoundary();
		}

		protected void MouseLeave(object sender, EventArgs e)
		{
			RecoverCursor();
		}
		protected void ChangeCursorAtBoundary(Point cursorPosition)
		{
			if (borderN.Contains(cursorPosition))
			{
				// Mouse cursor is at the north side
				control.Cursor = Cursors.SizeNS;
				cursorLocation = Border.N;
			}
			else if (borderS.Contains(cursorPosition))
			{
				// Mouse cursor is at the south side
				control.Cursor = Cursors.SizeNS;
				cursorLocation = Border.S;
			}
			else if (borderW.Contains(cursorPosition))
			{
				// Mouse cursor is at the west side
				control.Cursor = Cursors.SizeWE;
				cursorLocation = Border.W;
			}
			else if (borderE.Contains(cursorPosition))
			{
				// Mouse cursor is at the east side
				control.Cursor = Cursors.SizeWE;
				cursorLocation = Border.E;
			}
			else if (borderNW.Contains(cursorPosition))
			{
				// Mouse cursor is at the top-left corner
				control.Cursor = Cursors.SizeNWSE;
				cursorLocation = Border.NW;
			}
			else if (borderNE.Contains(cursorPosition))
			{
				// Mouse cursor is at the top-right corner
				control.Cursor = Cursors.SizeNESW;
				cursorLocation = Border.NE;
			}
			else if (borderSW.Contains(cursorPosition))
			{
				// Mouse cursor is at the bottom-left corner
				control.Cursor = Cursors.SizeNESW;
				cursorLocation = Border.SW;
			}
			else if (borderSE.Contains(cursorPosition))
			{
				// Mouse cursor is at the bottom-right corner
				control.Cursor = Cursors.SizeNWSE;
				cursorLocation = Border.SE;
			}
			else
			{
				control.Cursor = Cursors.Hand;
				cursorLocation = Border.None;
			}
		}
		protected bool IsCursorNearBoundary()
		{
			if (cursorLocation != Border.None)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

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

/* resizable control keep
protected virtual void ResizeControl(object sender, MouseEventArgs e)
		{

			// Adjust the size of the resizable block control_
			if (cursorLocation == Border.N)
			{
				control.Location = new Point(control.Location.X, control.Location.Y + heightDiff);
				control.Height -= heightDiff;
			}
			else if (cursorLocation == Border.NW)
			{
				control.Location = new Point(control.Location.X + widthDiff, control.Location.Y + heightDiff);
				control.Width -= widthDiff;
				control.Height -= heightDiff;
			}
			else if (cursorLocation == Border.W)
			{
				control.Location = new Point(control.Location.X + widthDiff, control.Location.Y);
				control.Width -= widthDiff;
			}
			else if (cursorLocation == Border.SW)
			{
				control.Location = new Point(control.Location.X + widthDiff, control.Location.Y);
				control.Width -= widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(preCursorPoint.X, e.Y);
			}
			else if (cursorLocation == Border.S)
			{
				control.Height += heightDiff;
				preCursorPoint = new Point(preCursorPoint.X, e.Y);
			}
			else if (cursorLocation == Border.SE)
			{
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(e.X, e.Y);
			}
			else if (cursorLocation == Border.E)
			{
				control.Width += widthDiff;
				preCursorPoint = new Point(e.X, preCursorPoint.Y);
			}
			else if (cursorLocation == Border.NE)
			{
				control.Location = new Point(control.Location.X, control.Location.Y + heightDiff);
				control.Width += widthDiff;
				control.Height -= heightDiff;
				preCursorPoint = new Point(e.X, preCursorPoint.Y);
			}
			//Thread.Sleep(1000);
			Debug.WriteLine("Resize to " + control.Size.ToString());

		}
		protected virtual void CalculateDiff()
		{
			widthDiff = horizontalMove;
			heightDiff = verticalMove;
			if (border == borderW || border == Border.N || )
			if (preHeight + heightDiff >= basicSize)
			{
				// safe
			}
			else
			{
				heightDiff = basicSize - preHeight;
			}
			if (preWidth + widthDiff >= basicSize)
			{
				// safe
			}
			else
			{
				widthDiff = basicSize - preWidth;
			}
		}
		protected virtual void MouseMove(object sender, MouseEventArgs e)
		{
			if (isResizing)
			{
				// Calculate the size difference based on the mouse movement
				preWidth = control.Width;
				preHeight = control.Height;
				horizontalMove = e.X - preCursorPoint.X;
				verticalMove = e.Y - preCursorPoint.Y;
				CalculateDiff();
				ResizeControl(sender, e);
				//preCursorPoint = new Point(e.X, e.Y);
			}
			else
			{
				ChangeCursorAtBoundary(e.Location);
			}
		}
		protected virtual void MouseUp(object sender, MouseEventArgs e)
		{
			isResizing = false;
			RecoverCursor();
			DefineBoundary();
			isDragging = false;
		}

		protected void MouseLeave(object sender, EventArgs e)
		{
			RecoverCursor();
		}
		protected void ChangeCursorAtBoundary(Point cursorPosition)
		{
			if (borderN.Contains(cursorPosition))
			{
				// Mouse cursor is at the north side
				control.Cursor = Cursors.SizeNS;
				cursorLocation = Border.N;
			}
			else if (borderS.Contains(cursorPosition))
			{
				// Mouse cursor is at the south side
				control.Cursor = Cursors.SizeNS;
				cursorLocation = Border.S;
			}
			else if (borderW.Contains(cursorPosition))
			{
				// Mouse cursor is at the west side
				control.Cursor = Cursors.SizeWE;
				cursorLocation = Border.W;
			}
			else if (borderE.Contains(cursorPosition))
			{
				// Mouse cursor is at the east side
				control.Cursor = Cursors.SizeWE;
				cursorLocation = Border.E;
			}
			else if (borderNW.Contains(cursorPosition))
			{
				// Mouse cursor is at the top-left corner
				control.Cursor = Cursors.SizeNWSE;
				cursorLocation = Border.NW;
			}
			else if (borderNE.Contains(cursorPosition))
			{
				// Mouse cursor is at the top-right corner
				control.Cursor = Cursors.SizeNESW;
				cursorLocation = Border.NE;
			}
			else if (borderSW.Contains(cursorPosition))
			{
				// Mouse cursor is at the bottom-left corner
				control.Cursor = Cursors.SizeNESW;
				cursorLocation = Border.SW;
			}
			else if (borderSE.Contains(cursorPosition))
			{
				// Mouse cursor is at the bottom-right corner
				control.Cursor = Cursors.SizeNWSE;
				cursorLocation = Border.SE;
			}
			else
			{
				control.Cursor = Cursors.Hand;
				cursorLocation = Border.None;
			}
		}
		protected bool IsCursorNearBoundary()
		{
			if (cursorLocation != Border.None)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
	

 */