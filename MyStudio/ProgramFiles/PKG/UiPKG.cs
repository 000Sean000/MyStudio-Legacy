using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using static PKG.FlexibleControl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

/*
 *  Control Extension
 */

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
			int x, y;
			int parentX = parent.Location.X;
			int parentY = parent.Location.Y;

			if (cursorLocation == Side.NW || cursorLocation == Side.N)
			{
				x = control.Location.X - widthDiff;
				y = control.Location.Y - heightDiff;
				if (x < 0) { parentX += x; }
				if (y < 0) { parentY += y; }
				parent.Location = new Point(parentX, parentY);
				control.Location = new Point(x, y);
				control.Width += widthDiff;
				control.Height += heightDiff;
			}

			else if (cursorLocation == Side.SW || cursorLocation == Side.W)
			{
				//...
				control.Location = new Point(control.Location.X - widthDiff, control.Location.Y);
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(preCursorPoint.X, e.Y);
			}
			else if (cursorLocation == Side.SE　|| cursorLocation == Side.S)
			{
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(e.X, e.Y);
			}

			else if (cursorLocation == Side.NE || cursorLocation == Side.E)
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
		protected override void CalculateSizeDifference()
		{
			//recover cursorLocation

			widthDiff = horizontalMove;
			heightDiff = verticalMove;
			/* border near N or W need opposite diff
			 * positive diff => grow
			 * negative diff => shrink
			 */
			if (cursorLocation == Side.NW)
			{
				widthDiff = -widthDiff;
				heightDiff = -heightDiff;
			}
			else if (cursorLocation == Side.N || cursorLocation == Side.NE)
			{
				heightDiff = -heightDiff;
			}
			else if (cursorLocation == Side.W || cursorLocation == Side.SW)
			{
				widthDiff = -widthDiff;
			}
			// fit image ratio
			Debug.WriteLine("imageRatio: " + imageRatio);
			Debug.WriteLine("Ratio: " + (float)preHeight / (float)preWidth);

			if (cursorLocation == Side.N || cursorLocation == Side.S)
			{
				widthDiff = (int)((float)(preHeight + heightDiff) / imageRatio - (float)preWidth);
			}
			else if (cursorLocation == Side.W || cursorLocation == Side.E)
			{
				heightDiff = (int)((float)(preWidth + widthDiff) * imageRatio - (float)preHeight);
			}
			else if (cursorLocation == Side.NW || cursorLocation == Side.SE || cursorLocation == Side.SW || cursorLocation == Side.NE)
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
	#region those having ".AutoSize" can set false:
	public class FlexbleLabel: FlexibleControl
	{
		public FlexbleLabel(Label control_, int thickness_ = 10):base(control_, thickness_)
		{
			control_.AutoSize = false;
		}
	}
	#endregion
	public class FlexibleControl
	{
		#region Fields
		public enum Side
		{
			N, S, W, E, NW, NE, SW, SE, None
		}
		public Control control;
		public Control parent;
		public bool enableDrag = true;
		//protected Control originalParent;
		protected Cursor originalCursor;
		protected Side cursorLocation = Side.None;
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
			parent = control_.Parent;
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
			DefineBorder();
			control.MouseDown += MouseDown;
			control.MouseUp += MouseUp;
			control.MouseMove += MouseMove;
			control.MouseLeave += MouseLeave;
		}
		public void DefineBorder()
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
			int x, y;
			if (cursorLocation == Side.N)
			{
				y = control.Location.Y - heightDiff;
				if (y < 0)
				{
					parent.Location = new Point(parent.Location.X, parent.Location.Y - heightDiff);
				}
				control.Location = new Point(control.Location.X, y);
				control.Height += heightDiff;
			}
			else if (cursorLocation == Side.NW)
			{
				control.Location = new Point(control.Location.X - widthDiff, control.Location.Y - heightDiff);
				control.Width += widthDiff;
				control.Height += heightDiff;
			}
			else if (cursorLocation == Side.W)
			{
				control.Location = new Point(control.Location.X - widthDiff, control.Location.Y);
				control.Width += widthDiff;
			}
			else if (cursorLocation == Side.SW)
			{
				control.Location = new Point(control.Location.X - widthDiff, control.Location.Y);
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(preCursorPoint.X, e.Y);
			}
			else if (cursorLocation == Side.S)
			{
				control.Height += heightDiff;
				preCursorPoint = new Point(preCursorPoint.X, e.Y);
			}
			else if (cursorLocation == Side.SE)
			{
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(e.X, e.Y);
			}
			else if (cursorLocation == Side.E)
			{
				control.Width += widthDiff;
				preCursorPoint = new Point(e.X, preCursorPoint.Y);
			}
			else if (cursorLocation == Side.NE)
			{
				control.Location = new Point(control.Location.X, control.Location.Y - heightDiff);
				control.Width += widthDiff;
				control.Height += heightDiff;
				preCursorPoint = new Point(e.X, preCursorPoint.Y);
			}
			
			//Thread.Sleep(1000);
			Debug.WriteLine("Resize to " + control.Size.ToString() +" at " + control.Location.ToString());
		}
		protected virtual void CalculateSizeDifference()
		{
			widthDiff = horizontalMove;
			heightDiff = verticalMove;
			/* border near N or W need opposite diff
			 * positive diff => grow
			 * negative diff => shrink
			 */
			if (cursorLocation == Side.NW)
			{
				widthDiff = -widthDiff;
				heightDiff = -heightDiff;
			}
			else if (cursorLocation == Side.N || cursorLocation == Side.NE) 
			{ 
				heightDiff = -heightDiff; 
			}
			else if (cursorLocation == Side.W || cursorLocation == Side.SW) 
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
				CalculateSizeDifference();
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
				ChangeCursorByRegion(e.Location);
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
			DefineBorder();
		}

		protected void MouseLeave(object sender, EventArgs e)
		{
			RecoverCursor();
		}
		protected void ChangeCursorByRegion(Point cursorPosition)
		{
			if (borderN.Contains(cursorPosition))
			{
				// Mouse cursor is at the north side
				control.Cursor = Cursors.SizeNS;
				cursorLocation = Side.N;
			}
			else if (borderS.Contains(cursorPosition))
			{
				// Mouse cursor is at the south side
				control.Cursor = Cursors.SizeNS;
				cursorLocation = Side.S;
			}
			else if (borderW.Contains(cursorPosition))
			{
				// Mouse cursor is at the west side
				control.Cursor = Cursors.SizeWE;
				cursorLocation = Side.W;
			}
			else if (borderE.Contains(cursorPosition))
			{
				// Mouse cursor is at the east side
				control.Cursor = Cursors.SizeWE;
				cursorLocation = Side.E;
			}
			else if (borderNW.Contains(cursorPosition))
			{
				// Mouse cursor is at the top-left corner
				control.Cursor = Cursors.SizeNWSE;
				cursorLocation = Side.NW;
			}
			else if (borderNE.Contains(cursorPosition))
			{
				// Mouse cursor is at the top-right corner
				control.Cursor = Cursors.SizeNESW;
				cursorLocation = Side.NE;
			}
			else if (borderSW.Contains(cursorPosition))
			{
				// Mouse cursor is at the bottom-left corner
				control.Cursor = Cursors.SizeNESW;
				cursorLocation = Side.SW;
			}
			else if (borderSE.Contains(cursorPosition))
			{
				// Mouse cursor is at the bottom-right corner
				control.Cursor = Cursors.SizeNWSE;
				cursorLocation = Side.SE;
			}
			else
			{
				control.Cursor = Cursors.Hand;
				cursorLocation = Side.None;
			}
		}
		protected bool IsCursorNearBoundary()
		{
			if (cursorLocation != Side.None)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
	
	public class ExControl
	{
		#region Fields
		public Control control;
		public Control parent; // parent control
		public float sizeRatio;
		#region Functionality enable
		protected bool _enableGroup = false;
		protected bool _enableFlex = true;
		public bool EnableGroup
		{
			set
			{
				_enableGroup = value;
				if (value)
				{
					Subscribe_Group_Handlers();
				}
				else
				{
					Unsubscribe_Group_Handlers();
				}
			}
			get { return _enableGroup; }
		}
		public bool EnableFlex
		{
			set
			{
				_enableFlex = value;
				if (value)
				{
					Subscribe_Flex_Handlers();
				}
				else { Unsubscribe_Flex_Handlers();}
			}
			get { return _enableFlex; }
		}

		public bool enableDrag = true;
		public bool enableResize = true;
		public bool enableRatioFixed = false;
		

		#endregion
		#region Group Extension
		public Control? root = null; // root control of group container
		protected bool _isRoot = false;
		public bool IsRoot
		{
			set
			{
				_isRoot = value;
				if (_isRoot)
				{
					control.MouseEnter += Group_MouseEnter;
					control.MouseLeave += Group_MouseLeave;
					control.MouseClick += Group_Click;
				}
				else
				{
					control.MouseEnter -= Group_MouseEnter;
					control.MouseLeave -= Group_MouseLeave;
					control.MouseClick -= Group_Click;
				}
			}
			get { return _isRoot; }
		}
		public bool IsRootOpen // the group is being selected
		{
			set
			{
				if (root != null)
				{
					root.Capture = !value;
				}
			}
			get
			{
				if (root == null)
				{
					return true;
				}
                else
                {
					return !root.Capture;
				}
            }
		}
		#endregion
		#region Resize & Drag Extension
		public enum Side
		{
			N, S, W, E, None
		}
		protected Cursor originalCursor;
		
		protected int thickness; // border thickness
		protected int basicWidth;
		protected int basicHeight;
		protected Rectangle wholeRegion;
		protected Rectangle borderN;
		protected Rectangle borderS;
		protected Rectangle borderW;
		protected Rectangle borderE;
		// flags
		protected bool isResizing = false;
		protected bool isDragging = false;
		// calculation variables
		bool[] isAtSide = new bool[4];
		protected Point preCursorPoint; // previous cursor point
		protected Point dragingStartPoint;
		protected int verticalMove, horizontalMove; // mouse movement
		protected int preWidth, preHeight; // previous W/H
		protected int widthDiff, heightDiff; // difference
		#endregion

		#endregion
		#region Constructor
		public ExControl(Control control)
		{
			this.control = control;
			init();
		}
		#region Constructor with setting ".AutoSize = false"
		public ExControl(Label control)
		{
			this.control = control;
			control.AutoSize = false;
			init();
		}
		#endregion
		public void init()
		{
			parent = control.Parent;
			EnableGroup = true;
			EnableFlex = true;
		}
		#endregion

		#region Group Extension
		public void Subscribe_Group_Handlers()
		{
			control.MouseEnter += Group_MouseEnter;
			control.MouseLeave += Group_MouseLeave;
			control.Click += Group_Click;
		}
		public void Unsubscribe_Group_Handlers()
		{
			control.MouseEnter -= Group_MouseEnter;
			control.MouseLeave -= Group_MouseLeave;
			control.Click -= Group_Click;
		}
		public void Group_MouseEnter(object sender, EventArgs e)
		{
			control.Capture = true;
		}
		public void Group_MouseLeave(object sender, EventArgs e)
		{
			control.Capture = false;
		}
		public void Group_Click(object sender, EventArgs e)
		{
			control.Capture = false;
			
		}
		//public delegate void EventHandler(object sender, EventArgs e);
		public void ActWhenRootOpen(object sender, EventArgs e, EventHandler action)
		{
			if (IsRootOpen)
			{
				action(sender, e);
			}
			/* calling example
			ActWhenRootOpen<object>(null, (x) => { });
			ActWhenRootOpen<int>(0, (x) => { });
			*/
		}
		public void ActWhenRootOpen<T>(T? arg, Action<T?> action)
		{
			if (IsRootOpen)
			{
				action(arg);
			}
			/* calling example
			ActWhenRootOpen<object>(null, (x) => { });
			ActWhenRootOpen<int>(0, (x) => { });
			*/
		}
		public void ActWhenRootOpen(Action action)
		{
			if (IsRootOpen)
			{
				action();
			}
			/* calling example
			ActWhenRootOpen(() => { });
			*/
		}

		#endregion

		#region Flexibility Extension
		public void Subscribe_Flex_Handlers()
		{
			control.MouseMove += Flex_MouseMove;
			control.MouseDown += Flex_MouseDown;
			control.MouseUp += Flex_MouseUp;
			control.MouseLeave += Flex_MouseLeave;
		}
		public void Unsubscribe_Flex_Handlers()
		{
			control.MouseMove -= Flex_MouseMove;
			control.MouseDown -= Flex_MouseDown;
			control.MouseUp -= Flex_MouseUp;
			control.MouseLeave -= Flex_MouseLeave;
		}
		#region Event Handlers
		protected virtual void Flex_MouseMove(object sender, MouseEventArgs e)
		{
			if (!IsRootOpen) { return; } 
			if (isResizing)
			{
				// Calculate the size difference based on the mouse movement
				preWidth = control.Width;
				preHeight = control.Height;
				horizontalMove = e.X - preCursorPoint.X;
				verticalMove = e.Y - preCursorPoint.Y;
				CalculateSizeDifference();
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
				CheckCursorLocatingRegion(e.Location);
				ChangeCursorByRegion();
			}

		}
		protected void Flex_MouseDown(object sender, MouseEventArgs e)
		{
			if (!IsRootOpen) { return; }
			// Check if the mouse is near the border of the container
			if (IsCursorNearBoundary())
			{
				isResizing = enableResize;
				preCursorPoint = new Point(e.X, e.Y);
			}
			else
			{
				isDragging = enableDrag;
				dragingStartPoint = new Point(e.X, e.Y);
			}

		}
		protected virtual void Flex_MouseUp(object sender, MouseEventArgs e)
		{
			if (!IsRootOpen) { return; }
			isResizing = false;
			isDragging = false;
			RecoverCursor();
			DefineBorder();
		}
		protected void Flex_MouseLeave(object sender, EventArgs e)
		{
			if (!IsRootOpen) { return; }
			RecoverCursor();
		}
		#endregion
		#region Functions for Handlers
		public void DefineBorder()
		{
			wholeRegion = control.ClientRectangle;
			// Define the regions for each side of the wholeRegion
			borderN = new Rectangle(wholeRegion.Left, wholeRegion.Top, wholeRegion.Width, thickness);
			borderS = new Rectangle(wholeRegion.Left, wholeRegion.Bottom - thickness, wholeRegion.Width, thickness);
			borderW = new Rectangle(wholeRegion.Left, wholeRegion.Top, thickness, wholeRegion.Height);
			borderE = new Rectangle(wholeRegion.Right - thickness, wholeRegion.Top, thickness, wholeRegion.Height);
			
		}
		public void RecoverCursor()
		{
			control.Cursor = new Cursor(originalCursor.Handle);
		}

		protected virtual void ResizeControl(MouseEventArgs e)
		{
			// Adjust the size of the resizable block control_
			// Only update preCursorPosition when mousemove relates to S or E direction,

			int controlX = control.Location.X + horizontalMove * Convert.ToInt32(isAtSide[(int)Side.W]);
			int controlY = control.Location.Y + verticalMove * Convert.ToInt32(isAtSide[(int)Side.N]);

			int parentX = parent.Location.X;
			int parentY = parent.Location.Y;
			if (controlX < 0) { parentX -= controlX; }
			if (controlY < 0) { parentY -= controlY; }

			int preCursorX = isAtSide[(int)Side.E] ? e.X : preCursorPoint.X;
			int preCursorY = isAtSide[(int)Side.S] ? e.Y : preCursorPoint.Y;

			parent.Location = new Point(parentX, parentY);
			control.Location = new Point(controlX, controlY);
			control.Width += widthDiff * Convert.ToInt32(isAtSide[(int)Side.W] | isAtSide[(int)Side.E]);
			control.Height += heightDiff * Convert.ToInt32(isAtSide[(int)Side.N] | isAtSide[(int)Side.S]);
			preCursorPoint = new Point(preCursorX, preCursorY);

			Debug.WriteLine("Resize to " + control.Size.ToString() + " at " + control.Location.ToString());
		}
		protected virtual void CalculateSizeDifference()
		{

			/* border near N or W need get diff by opposite move
			 * positive move => grow
			 * negative move => shrink
			 */
			heightDiff = isAtSide[(int)Side.N] ? -verticalMove : verticalMove;
			widthDiff = isAtSide[(int)Side.W] ? -horizontalMove : horizontalMove;

			// check ratio
			if (enableRatioFixed)
			{

				Debug.WriteLine("sizeRatio: " + sizeRatio);
				Debug.WriteLine("Ratio: " + (float)preHeight / (float)preWidth);
				
				if ((Convert.ToInt32(isAtSide[(int)Side.N]) 
					+ Convert.ToInt32(isAtSide[(int)Side.S]) 
					+ Convert.ToInt32(isAtSide[(int)Side.N]) 
					+ Convert.ToInt32(isAtSide[(int)Side.S])) >= 2) // at side corner
				{
					if (widthDiff >= heightDiff)
					{
						heightDiff = (int)((float)(preWidth + widthDiff) * sizeRatio - (float)preHeight);
					}
					else
					{
						widthDiff = (int)((float)(preHeight + heightDiff) / sizeRatio - (float)preWidth);
					}
				}
				else 
				{
					if (isAtSide[(int)Side.N] | isAtSide[(int)Side.S])
					{
						widthDiff = (int)((float)(preHeight + heightDiff) / sizeRatio - (float)preWidth);
					}
					else if (isAtSide[(int)Side.W] | isAtSide[(int)Side.E])
					{
						heightDiff = (int)((float)(preWidth + widthDiff) * sizeRatio - (float)preHeight);
					}
				}
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
			Debug.WriteLine("widthDiff: " + widthDiff + "  heightDiff: " + heightDiff);
		}

		protected virtual void DragControl(int horizontalMove, int verticalMove)
		{
			control.Location = new Point(control.Location.X + horizontalMove, control.Location.Y + verticalMove);
		}
		protected void CheckCursorLocatingRegion(Point cursorPosition)
		{
			isAtSide[(int)Side.N] = borderN.Contains(cursorPosition);
			isAtSide[(int)Side.S] = borderS.Contains(cursorPosition);
			isAtSide[(int)Side.W] = borderW.Contains(cursorPosition);
			isAtSide[(int)Side.E] = borderE.Contains(cursorPosition);
		}
		protected void ChangeCursorByRegion()
		{
			if (enableResize)
			{
				if (isAtSide[(int)Side.N] & isAtSide[(int)Side.W])
				{
					control.Cursor = Cursors.SizeNWSE;
				}
				else if (isAtSide[(int)Side.N] & isAtSide[(int)Side.E])
				{
					control.Cursor = Cursors.SizeNESW;
				}
				else if (isAtSide[(int)Side.S] & isAtSide[(int)Side.W])
				{
					control.Cursor = Cursors.SizeNESW;
				}
				else if (isAtSide[(int)Side.S] & isAtSide[(int)Side.E])
				{
					control.Cursor = Cursors.SizeNWSE;
				}
				else
				{
					if (isAtSide[(int)Side.N])
					{
						control.Cursor = Cursors.SizeNS;
					}
					else if (isAtSide[(int)Side.S])
					{
						control.Cursor = Cursors.SizeNS;
					}
					else if (isAtSide[(int)Side.W])
					{
						control.Cursor = Cursors.SizeWE;
					}
					else if (isAtSide[(int)Side.E])
					{
						control.Cursor = Cursors.SizeWE;
					}
				}
			}
			if (enableDrag)
			{
				if (!(isAtSide[(int)Side.N] | isAtSide[(int)Side.S] | isAtSide[(int)Side.W] | isAtSide[(int)Side.E]))
				{
					control.Cursor = Cursors.Hand;
				}
			}

			
		}
		protected bool IsCursorNearBoundary()
		{
			return isAtSide[(int)Side.N] | isAtSide[(int)Side.S] | isAtSide[(int)Side.W] | isAtSide[(int)Side.E];
		}
		#endregion
		#endregion
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
			// Subscribe the AdjustWorldSpaceSize method to the appropriate events
			_panel.ControlAdded += AdjustWorldSpaceSize;
			_panel.ControlRemoved += AdjustWorldSpaceSize;
		}
		public void AdjustWorldSpaceSize(object sender, EventArgs e)
		{
			// Calculate the minimum required size for the world space
			int minWidth = 0;
			int minHeight = 0;
			int offsetX = int.MaxValue;
			int offsetY = int.MaxValue;

			foreach (Control childControl in _panel.Controls)
			{
				// Subscribe to the LocationChanged event for each child control
				childControl.LocationChanged -= AdjustWorldSpaceSize;
				childControl.LocationChanged += AdjustWorldSpaceSize;

				// Subscribe to the SizeChanged event for each child control
				childControl.SizeChanged -= AdjustWorldSpaceSize;
				childControl.SizeChanged += AdjustWorldSpaceSize;
				// Adjust the required width and height based on the child control's position and size
				minWidth = Math.Max(minWidth, childControl.Right);
				minHeight = Math.Max(minHeight, childControl.Bottom);

				// Track the minimum negative X and Y coordinates
				offsetX = Math.Min(offsetX, childControl.Left);
				offsetY = Math.Min(offsetY, childControl.Top);
			}

			// Adjust the minimum required size for the world space based on negative offsets
			minWidth -= offsetX;
			minHeight -= offsetY;

			// Set the minimum required size for the world space
			_panel.AutoScrollMinSize = new Size(minWidth, minHeight);
			_panel.AutoScrollPosition = new Point(-offsetX, -offsetY);
			Debug.WriteLine($"autoscroll: {_panel.AutoScroll}");
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

	#region Developing
	public class MultiClickHandler
	{
		private int clickCount = 0;
		private System.Threading.Timer clickTimer;

		public MultiClickHandler()
		{
			clickTimer = new System.Threading.Timer(ClickTimerCallback, null, Timeout.Infinite, Timeout.Infinite);
		}

		private void YourClickEventHandler(object sender, EventArgs e)
		{
			clickCount++;
			clickTimer.Change(300, Timeout.Infinite);
		}

		private void ClickTimerCallback(object state)
		{
			if (clickCount == 1)
			{
				// Single click action
			}
			else if (clickCount == 2)
			{
				// Double click action
			}

			// Reset the click count
			clickCount = 0;
		}

	}
	#endregion
}
