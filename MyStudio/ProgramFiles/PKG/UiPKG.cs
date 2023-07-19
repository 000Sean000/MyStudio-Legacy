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
		public static FlexibleControl SetControlResizable(Control control_, int thickness_ = 10) { return new FlexibleControl(control_, thickness_); }
		public static DraggableCanvas SetCanvasDaggable(Panel panel, float sensitivity = 1) { return new DraggableCanvas(panel, sensitivity); }
		//public static SuperPictureBox SetPictureBoxFitImage(PictureBox pictureBox_) { return new SuperPictureBox(pictureBox_); }

	}



	public static class ControlAligner
	{
		public static int GetTotalWidth_OfControlsInContainer(Panel panel)
		{
			int totalWidth = 0;
			foreach (Control control in panel.Controls)
			{
				totalWidth += control.Width;
			}
			return totalWidth;
		}
		public static int GetTotalHeight_OfControlsInContainer(Panel panel)
		{
			int totalHeight = 0;
			foreach (Control control in panel.Controls)
			{
				totalHeight += control.Height;
			}
			return totalHeight;
		}
		public static int GetMaxWidth_OfControlsInContainer(Panel panel)
		{
			int maxWidth = 0;
			foreach (Control control in panel.Controls)
			{
				if(control.Width > maxWidth)
				{
					maxWidth = control.Width;
				}
			}
			return maxWidth;
		}
		public static int GetMaxHeight_OfControlsInContainer(Panel panel)
		{
			int maxHeight = 0;
			foreach (Control control in panel.Controls)
			{
				if (control.Height > maxHeight)
				{
					maxHeight = control.Height;
				}
			}
			return maxHeight;
		}
		public static void AlignControlsHorizontally(Panel panel, int spacing = 0)
		{
			//panel.Height = GetMaxHeightOfControlsInContainer(panel);
			panel.Size = new Size(GetTotalWidth_OfControlsInContainer(panel), GetMaxHeight_OfControlsInContainer(panel));

			int centerY = panel.Height / 2;
			int currentX = 0;
			Debug.WriteLine(panel.Controls);
			foreach (Control control in panel.Controls)
			{
				int controlX = currentX;
				int controlY = centerY - (control.Height / 2);

				control.Location = new Point(controlX, controlY);
				currentX += control.Width + spacing;
				Debug.WriteLine(control);
			}
		}

		public static void AlignControlsVertically(Panel panel, int spacing = 0)
		{
			//panel.Width = GetMaxWidthOfControlsInContainer(panel);
			panel.Size = new Size(GetMaxWidth_OfControlsInContainer(panel), GetTotalHeight_OfControlsInContainer(panel));

			int centerX = panel.Width / 2;
			int currentY = 0;

			foreach (Control control in panel.Controls)
			{
				int controlX = centerX - (control.Width / 2);
				int controlY = currentY;

				control.Location = new Point(controlX, controlY);
				currentY += control.Height + spacing;
			}
		}
		public enum Alignment
		{
			Horizontal,
			Vertical,
			Center
		}

		public static void AlignControls(Control container, Alignment alignment)
		{
			int maxControlWidth = 0;
			int totalControlHeight = 0;

			foreach (Control control in container.Controls)
			{
				maxControlWidth = Math.Max(maxControlWidth, control.Width);
				totalControlHeight += control.Height;
			}

			int y = 0;

			foreach (Control control in container.Controls)
			{
				switch (alignment)
				{
					case Alignment.Horizontal:
						control.Location = new Point(0, y);
						control.Anchor = AnchorStyles.Left | AnchorStyles.Top;
						break;
					case Alignment.Vertical:
						control.Location = new Point((maxControlWidth - control.Width) / 2, y);
						control.Anchor = AnchorStyles.Left | AnchorStyles.Top;
						break;
					case Alignment.Center:
						control.Location = new Point((container.Width - control.Width) / 2, y);
						control.Anchor = AnchorStyles.Left | AnchorStyles.Top;
						break;
				}

				y += control.Height;
			}
		}

	}

	public class AutoSizeTextBox
	{
		private TextBox textBox;
		private int basicWidth;
		private int basicHeight;

		public AutoSizeTextBox(TextBox textBox, int basicWidth=50, int basicHeight=0)
		{
			this.textBox = textBox;
			this.basicWidth = basicWidth;
			this.basicHeight = Math.Max(basicHeight, textBox.Font.Height);
			textBox.Multiline = true;
			// Subscribe to the TextChanged event of the TextBox
			textBox.TextChanged += TextBox_TextChanged;

			// Call the initial resize to adjust the size based on the initial text content
			ResizeTextBox();
		}

		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			ResizeTextBox();
		}

		private void ResizeTextBox()
		{
			// Create a temporary Graphics object to measure the text size
			using (Graphics g = textBox.CreateGraphics())
			{
				SizeF textSize = g.MeasureString(textBox.Text, textBox.Font);

				int newWidth = Math.Max((int)textSize.Width + 5, basicWidth);  // Add some padding

				// Calculate the new height based on the number of lines
				int lines = textBox.GetLineFromCharIndex(textBox.TextLength) + 1;
				int newHeight = Math.Max(lines * textBox.Font.Height + 5, basicHeight);

				// Update the TextBox size if it needs to be adjusted
				if (textBox.Width != newWidth || textBox.Height != newHeight)
				{
					textBox.Size = new Size(newWidth, newHeight);
				}
			}
		}
	}

	public class FlexiblePanel
	{
		protected Panel _panel;
		protected Control _control;
		public int padding;
		public FlexiblePanel(Panel panel_, Control control_, int padding_ = 10)
		{
			_panel = panel_;
			_control = control_;
			_control.SizeChanged += PictureBoxSizeChanged;
			padding = padding_;
		}
		public void PictureBoxSizeChanged(object sender, EventArgs e)
		{

			_panel.Size = new Size(_control.Width +2*padding, _control.Height+2*padding);
			_control.Location = new Point(padding, padding);
		}
	}
	public class FlexiblePictureBox : FlexibleControl
	{
		PictureBox pictureBox;
		float imageRatio;
		public FlexiblePictureBox(PictureBox pictureBox_) : base(pictureBox_)
		{
			pictureBox = pictureBox_;
			imageRatio = ((float)pictureBox.Image.Size.Height / (float)pictureBox.Image.Size.Width);
			FitZoomedImage(true);
			int expectedBasicHeight = (int)((float)basicWidth * imageRatio);
			int expectedBasicWidth = (int)((float)basicHeight / imageRatio);
			if (imageRatio > 1)
			{
				if (basicHeight < expectedBasicHeight)
				{
					basicHeight = expectedBasicHeight;
				}
				else
				{
					basicWidth = expectedBasicWidth;
				}
			}
			else if (imageRatio <= 1)
			{
				if (basicWidth < expectedBasicWidth)
				{
					basicWidth = expectedBasicWidth;
				}
				else
				{
					basicHeight = expectedBasicHeight;
				}
			}
			control.MinimumSize = new Size(basicWidth, basicHeight);
			Debug.WriteLine("minimum size: "+control.MinimumSize.ToString());
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
			int diff = (int)(pictureBox.Size.Width * imageRatio) - pictureBox.Size.Height;
			pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y - diff);
		}
		public void MoveHorizontally()
		{
			int diff = (int)(pictureBox.Size.Height / imageRatio) - pictureBox.Size.Width;
			pictureBox.Location = new Point(pictureBox.Location.X - diff, pictureBox.Location.Y);
		}
		public void FitZoomedImage(bool byShrink = false)
		{
			if (byShrink)
			{
				// shrink pictureBox to match imageRatio(eliminate rundent space)
				if (pictureBox.Size.Height > pictureBox.Size.Width * imageRatio) // current height > expected height
				{
					StretchHeight();
				}
				else
				{
					StretchWidth();
				}
			}
			else
			{
				// grow pictureBox to match imageRatio
				if (pictureBox.Size.Height < pictureBox.Size.Width * imageRatio) // current height > expected height
				{
					StretchHeight();
				}
				else
				{
					StretchWidth();
				}
			}
			
			
			Debug.WriteLine("box:" + pictureBox.Size.ToString() + "  image:" + pictureBox.Image.Size.ToString());

		}


		protected override void ResizeControl(MouseEventArgs e)
		{
			// Adjust the size of the resizable block control_
			// don't update preCursorPosition when mousemove relates to N or W direction,

			if (cursorLocation == Border.NW || cursorLocation == Border.N)
			{
				control.Location = new Point(control.Location.X - widthDiff, control.Location.Y - heightDiff);
				control.Width += widthDiff;
				control.Height += heightDiff;
			}

			else if (cursorLocation == Border.SW || cursorLocation == Border.W)
			{
				control.Location = new Point(control.Location.X - widthDiff, control.Location.Y);
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(preCursorPoint.X, e.Y);
			}
			else if (cursorLocation == Border.SE　|| cursorLocation == Border.S)
			{
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(e.X, e.Y);
			}

			else if (cursorLocation == Border.NE || cursorLocation == Border.E)
			{
				control.Location = new Point(control.Location.X, control.Location.Y - heightDiff);
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(e.X, preCursorPoint.Y);
			}

			//Thread.Sleep(1000);
			Debug.WriteLine("Resize to " + control.Size.ToString() + " at " + control.Location.ToString());
		}
		//protected  void CalculateDiff2()
		protected override void CalculateDiff()
		{
			//recover cursorLocation

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
			// fit image ratio
			Debug.WriteLine("imageRatio: " + imageRatio);
			Debug.WriteLine("Ratio: " + (float)preHeight / (float)preWidth);

			if (cursorLocation == Border.N || cursorLocation == Border.S)
			{
				widthDiff = (int)((float)(preHeight + heightDiff) / imageRatio - (float)preWidth);
			}
			else if (cursorLocation == Border.W || cursorLocation == Border.E)
			{
				heightDiff = (int)((float)(preWidth + widthDiff) * imageRatio - (float)preHeight);
			}
			else if (cursorLocation == Border.NW || cursorLocation == Border.SE || cursorLocation == Border.SW || cursorLocation == Border.NE)
			{
				if (widthDiff >= heightDiff)
				{
					heightDiff = (int)((float)(preWidth + widthDiff) * imageRatio - (float)preHeight);
				}
				else
				{
					widthDiff = (int)((float)(preHeight + heightDiff) / imageRatio - (float)preWidth);
				}
			}
			// check basic Size
			if (preHeight + heightDiff <= control.MinimumSize.Height)
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
	}
	/*
	 * Remember to set property "AutoSize" of the control to False!!!!!
	 * Do not put the control in "table layout panel", it will work badly
	 * It is better to put the control in a "panel"
	 */
	public class FlexbleLabel: FlexibleControl
	{
		public FlexbleLabel(Label control_, int thickness_ = 10):base(control_, thickness_)
		{
			control_.AutoSize = false;
		}
	}
	public class FlexibleControl
	{
		#region Fields
		public enum Border
		{
			N, S, W, E, NW, NE, SW, SE, None
		}
		public Control control;
		public bool enableDrag = true;
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
		public FlexibleControl(Control control_, int thickness_ = 10)
		{
			control = control_;
			//originalParent = control_.Parent;
			originalCursor = new Cursor(control_.Cursor.Handle);
			thickness = thickness_;
			basicWidth = 5 * thickness;
			basicHeight = 5 * thickness;
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
			// don't update preCursorPosition when mousemove relates to N or W direction,
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
				if (enableDrag)
				{
					horizontalMove = e.X - dragingStartPoint.X;
					verticalMove = e.Y - dragingStartPoint.Y;
					DragControl(horizontalMove, verticalMove);
					//preCursorPoint = e.Location;
				}

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
