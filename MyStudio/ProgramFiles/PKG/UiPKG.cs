using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
/*
 *  Control Extension
 */

namespace PKG
{
	public static class UiPKG
	{
		// Replace oldControl with newControl in parentControl
		public static void ReplaceControl(Control oldControl, Control newControl, Control parentControl)
		{
			// Step 1: Find the index of oldControl in the Panel's Controls collection
			int index = parentControl.Controls.IndexOf(oldControl);

			// Step 2: Insert newControl at the same index as oldControl
			parentControl.Controls.Add(newControl);
			parentControl.Controls.SetChildIndex(newControl, index);

			// Step 3: Remove oldControl from the Controls collection
			parentControl.Controls.Remove(oldControl);
		}
	}



	public static class ControlAligner
	{
		public static int GetTotalWidth_OfControlsInContainer(Panel panel, int spacing = 0)
		{
			int totalWidth = -spacing;
			foreach (Control control in panel.Controls)
			{
				totalWidth += control.Width + spacing;
			}
			return totalWidth;
		}
		public static int GetTotalHeight_OfControlsInContainer(Panel panel, int spacing = 0)
		{
			int totalHeight = -spacing;
			foreach (Control control in panel.Controls)
			{
				totalHeight += control.Height + spacing;
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
		public static void AlignControlsHorizontally(Panel panel, int padding = 0, int spacing = 0)
		{
			//panel.Height = GetMaxHeightOfControlsInContainer(panel);
			panel.Size = new Size(GetTotalWidth_OfControlsInContainer(panel, spacing) + 2 * padding, GetMaxHeight_OfControlsInContainer(panel) + 2 * padding);

			int centerY = panel.Height / 2;
			int currentX = padding;
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

		public static void AlignControlsVertically(Panel panel, int padding = 0, int spacing = 0)
		{
			//panel.Width = GetMaxWidthOfControlsInContainer(panel);
			panel.Size = new Size(GetMaxWidth_OfControlsInContainer(panel) + 2 * padding, GetTotalHeight_OfControlsInContainer(panel, spacing) + 2 * padding);

			int centerX = panel.Width / 2;
			int currentY = padding;

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

	// AutoResize when text changed
	
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
		public bool EnableDrag = true;
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

		protected int BorderThickness; // border BorderThickness
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
			BorderThickness = thickness_;
			basicWidth = 5 * BorderThickness;
			basicHeight = 5 * BorderThickness;
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
			borderN = new Rectangle(border.Left + BorderThickness, border.Top, border.Width - (2 * BorderThickness), BorderThickness);
			borderS = new Rectangle(border.Left + BorderThickness, border.Bottom - BorderThickness, border.Width - (2 * BorderThickness), BorderThickness);
			borderW = new Rectangle(border.Left, border.Top + BorderThickness, BorderThickness, border.Height - (2 * BorderThickness));
			borderE = new Rectangle(border.Right - BorderThickness, border.Top + BorderThickness, BorderThickness, border.Height - (2 * BorderThickness));
			borderNW = new Rectangle(border.Left, border.Top, BorderThickness, BorderThickness);
			borderNE = new Rectangle(border.Right - BorderThickness, border.Top, BorderThickness, BorderThickness);
			borderSW = new Rectangle(border.Left, border.Bottom - BorderThickness, BorderThickness, BorderThickness);
			borderSE = new Rectangle(border.Right - BorderThickness, border.Bottom - BorderThickness, BorderThickness, BorderThickness);
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
				if (EnableDrag)
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
	public class ExTextBox : ExControl
	{
		private TextBox textBox;
		private int basicWidth;
		private int basicHeight;

		public ExTextBox(TextBox textBox, int basicWidth = 50, int basicHeight = 0) : base(textBox)
		{
			this.textBox = textBox;
			this.basicWidth = basicWidth;
			this.basicHeight = Math.Max(basicHeight, textBox.Font.Height);
			textBox.Multiline = true;
			// Subscribe to the TextChanged event of the TextBox
			textBox.TextChanged += TextBox_TextChanged;

			// Call the initial resize to adjust the size based on the initial text content
			AutoResizeTextBox();
		}

		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			AutoResizeTextBox();
		}

		private void AutoResizeTextBox()
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
	public class ExPictureBox:ExControl
	{
		PictureBox pictureBox;
		public ExPictureBox(PictureBox pictureBox) : base(pictureBox)
		{
			this.pictureBox = pictureBox;
			SizeRatio = ((float)pictureBox.Image.Size.Height / (float)pictureBox.Image.Size.Width);
			EnableRatioFixed = true;
			control.AutoSize = false;
			FitBoxToZoomedImage(true);
		}
		public void StretchWidth()
		{
			pictureBox.Size = new Size((int)(pictureBox.Size.Height / SizeRatio), (int)(pictureBox.Size.Height));
		}
		public void StretchHeight()
		{
			pictureBox.Size = new Size((int)(pictureBox.Size.Width), (int)(pictureBox.Size.Width * SizeRatio));
		}
		public void FitBoxToZoomedImage(bool byShrink = true)
		{
			if (byShrink)
			{
				// shrink pictureBox to match imageRatio(eliminate rundent space)
				if (pictureBox.Size.Height > pictureBox.Size.Width * SizeRatio) // current height > expected height
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
				if (pictureBox.Size.Height < pictureBox.Size.Width * SizeRatio) // current height > expected height
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
	}
	public class ExControl
	{
		#region Fields
		public Control control;
		public Control parent; // parent control
		/*
		 * To prevent recusion of accessor:
		 * Define the direction of calling and forbit reverse direction calling.
		 * 
		 * 1. Let object accessor call flag accessor
		 *	e.g. GroupRoot = someControl call IsGroupRoot
		 * 
		 * 2. Let parent flag accessor getter refresh itself according to child flag,
		 *    And let getter call setter, setter don't call getter
		 * (Letting child flag accessor setter refresh parent will repeat many same refresh action)
		 *	e.g. EnableFlex = EnableDrag || EnableResize
		 * 
		 */
		#region Functionality Enable
		protected bool _enableGroup;
		protected bool _enableFlex;
		public bool EnableGroup
		{
			set
			{
				_enableGroup = value;
			}
			get
			{
				EnableGroup = GroupRoot != null || GroupInnerBody != null;
				return _enableGroup;
			}
		}
		public bool EnableFlex
		{
			set
			{
				_enableFlex = value;
				if (_enableFlex)
				{
					Subscribe_Flex_Handlers();
				}
				else
				{
					Unsubscribe_Flex_Handlers();
				}
			}
			get
			{
				EnableFlex = EnableDrag || EnableResize; 
				return _enableFlex;
			}
		}

		public bool _enableDrag;
		public bool _enableResize;
		public bool _enableRatioFixed;
		public bool EnableDrag
		{
			set
			{
				_enableDrag = value;
			}
			get { return _enableDrag; }
		}
		public bool EnableResize
		{
			set
			{
				_enableResize = value;
			}
			get 
			{
				EnableResize |= _enableRatioFixed;
				return _enableResize; 
			}
		}
		public bool EnableRatioFixed
		{
			set
			{
				_enableRatioFixed = value;
			}
			get { return _enableRatioFixed; }
		}
		#endregion
		#region Appearance Extension
		public Color borderColor = Color.Red; // default border color
		public Color BorderColor
		{
			set
			{
				borderColor = value;
				control.Invalidate(); // Trigger a repaint to update the border color
			}
			get { return borderColor; }
		}
		#endregion
		#region Group Extension
		private Color originalBorderColor;
		public Color groupSelectedBorderColor = Color.Blue;
		public Control? _groupRoot = null; // GroupRoot control of group container
		public Control? GroupRoot
		{
			set
			{
				_groupRoot = value;
				Debug.WriteLine($"\t_groupRoot:{_groupRoot}, control:{control}");
				if (_groupRoot == control)
				{
					IsGroupRoot = true;
				}
				else
				{
					IsGroupRoot = false;
				}
				
			}
			get { return _groupRoot; }
		}
		protected bool _isGroupRoot;
		protected ExControl? _groupInnerBody; // to track child's isRootOpen
		public ExControl? GroupInnerBody
		{
			set
			{
				_groupInnerBody = value;
				if (_groupInnerBody != null)
				{
					GroupRoot = control;
					IsGroupRoot = true;
					Debug.WriteLine($"\tGroupInnerBody is not null. IsGroupRoot:{IsGroupRoot}");
				}
				else
				{
					IsGroupRoot = false;
					Debug.WriteLine("\tGroupInnerBody is null!");
				}
			}
			get { return _groupInnerBody; }
		}
		protected bool _isRootOpen;
		public bool IsGroupRoot
		{
			set
			{
				_isGroupRoot = value;
				if (_isGroupRoot)
				{
					Subscribe_GroupRoot_Handlers();
				}
				else
				{
					Unsubscribe_GroupRoot_Handlers();
				}
			}
			get { return _isGroupRoot; }
		}
		public bool IsRootOpen // the group is being selected
		{
			set
			{
				if (EnableGroup && GroupRoot != null)
				{
					_isRootOpen = value;
					if (_isRootOpen)
					{
						GroupRoot.Capture = false;
						originalBorderColor = borderColor;
						borderColor = groupSelectedBorderColor;
					}
					else
					{
						borderColor = originalBorderColor;
					}
				}
			}
			get
			{
				if (!EnableGroup || GroupRoot == null || IsGroupRoot)
				{
					//Debug.WriteLine($"\tEnableGroup:{EnableGroup}, GroupRoot == null:{GroupRoot == null}, IsGroupRoot:{IsGroupRoot}");
					return true;
				}
				else
				{
					if (GroupRoot.Capture) { _isRootOpen = false;}
					return _isRootOpen;
				}
			}
		}
		#endregion
		#region Flex Extension
		public enum Side
		{
			N, S, W, E, None
		}
		protected Cursor originalCursor;
		protected int _borderThickness; // border BorderThickness
		public int BorderThickness
		{
			set
			{
				_borderThickness = value;
				DefineBasicSize();
			}
			get { return _borderThickness; }
		}

		protected float _sizeRatio;
		public float SizeRatio
		{
			set
			{
				_sizeRatio = value;
				DefineBasicSize();
			}
			get { return _sizeRatio; }
		}
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
		#region Constructors with: "SizeRatio = ...", ".AutoSize = false"
		public ExControl(Label control)
		{
			this.control = control;
			control.AutoSize = false;
			init();
		}
		#endregion
		public void init()
		{
			EnableGroup = false;
			IsGroupRoot = false;
			EnableFlex = false;
			EnableDrag = false;
			EnableResize = false;
			EnableRatioFixed = false;

			parent = control.Parent;
			originalCursor = new Cursor(control.Cursor.Handle);
			BorderThickness = 10;

			control.Paint += Control_Paint;

		}
		public void DefineBasicSize()
		{
			basicWidth = 5 * BorderThickness;
			basicHeight = 5 * BorderThickness;
			if (control.MinimumSize.Width > basicWidth)
			{
				basicWidth = control.MinimumSize.Width;
			}
			if (control.MinimumSize.Height > basicHeight)
			{
				basicHeight = control.MinimumSize.Height;
			}
			if (EnableRatioFixed) // maintain ratio policy: only growth
			{
				int expectedBasicHeight = (int)((float)basicWidth * SizeRatio);
				int expectedBasicWidth = (int)((float)basicHeight / SizeRatio);
				if (SizeRatio > 1)
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
				else if (SizeRatio <= 1)
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
			}
			control.MinimumSize = new Size(basicWidth, basicHeight);
			DefineBorder();
		}
		#endregion
		#region Appearance Extension
		private void Control_Paint(object sender, PaintEventArgs e)
		{
			// Draw the custom border using the specified color
			using (var pen = new Pen(borderColor, 2))
			{
				e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, control.Width - 1, control.Height - 1));
			}
		}
		#endregion
		#region Group Extension
		public void Subscribe_GroupRoot_Handlers()
		{
			Unsubscribe_GroupRoot_Handlers();// prevent duplicated handler subscription
			control.MouseEnter += Group_MouseEnter;
			control.MouseMove += Group_MouseMove;
			control.Click += Group_Click;
			Debug.WriteLine("\tGroup Extension Subscription");
		}
		public void Unsubscribe_GroupRoot_Handlers()
		{
			control.MouseEnter -= Group_MouseEnter;
			control.MouseMove -= Group_MouseMove;
			control.Click -= Group_Click;
		}
		public void Group_MouseEnter(object sender, EventArgs e)
		{
			Debug.WriteLine($"\tIsGroupRoot:{IsGroupRoot}, GroupInnerBody.IsRootOpen:{GroupInnerBody.IsRootOpen}");
			if (IsGroupRoot)
			{
				//if (!GroupInnerBody.IsRootOpen)
				//{
					control.Capture = true;
				//}
			}
		}
		// MouseLeave will not be triggered when capture is still true
		// so use MouseMove
		public void Group_MouseMove(object sender, MouseEventArgs e) 
		{
			//Debug.WriteLine($"control.Location:{control.Location}, Cursor:{e.Location}");
			if (!control.ClientRectangle.Contains(e.Location))
			{
				control.Capture = false;
				borderColor = originalBorderColor;
			}

		}
		public void Group_Click(object sender, EventArgs e) // represent a quick click
		{
			IsRootOpen = true;
			originalBorderColor = borderColor;
			borderColor = Color.Blue;
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
		#region Flex Extension
		public void Subscribe_Flex_Handlers()
		{
			Unsubscribe_Flex_Handlers();// prevent duplicated handler subscription
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
				if (EnableDrag)
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
				isResizing = EnableResize;
				preCursorPoint = new Point(e.X, e.Y);
			}
			else
			{
				isDragging = EnableDrag;
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
			borderN = new Rectangle(wholeRegion.Left, wholeRegion.Top, wholeRegion.Width, BorderThickness);
			borderS = new Rectangle(wholeRegion.Left, wholeRegion.Bottom - BorderThickness, wholeRegion.Width, BorderThickness);
			borderW = new Rectangle(wholeRegion.Left, wholeRegion.Top, BorderThickness, wholeRegion.Height);
			borderE = new Rectangle(wholeRegion.Right - BorderThickness, wholeRegion.Top, BorderThickness, wholeRegion.Height);
			
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
			if (EnableRatioFixed)
			{

				Debug.WriteLine("SizeRatio: " + SizeRatio);
				Debug.WriteLine("Ratio: " + (float)preHeight / (float)preWidth);
				
				if ((Convert.ToInt32(isAtSide[(int)Side.N]) 
					+ Convert.ToInt32(isAtSide[(int)Side.S]) 
					+ Convert.ToInt32(isAtSide[(int)Side.N]) 
					+ Convert.ToInt32(isAtSide[(int)Side.S])) >= 2) // at side corner
				{
					if (widthDiff >= heightDiff)
					{
						heightDiff = (int)((float)(preWidth + widthDiff) * SizeRatio - (float)preHeight);
					}
					else
					{
						widthDiff = (int)((float)(preHeight + heightDiff) / SizeRatio - (float)preWidth);
					}
				}
				else 
				{
					if (isAtSide[(int)Side.N] | isAtSide[(int)Side.S])
					{
						widthDiff = (int)((float)(preHeight + heightDiff) / SizeRatio - (float)preWidth);
					}
					else if (isAtSide[(int)Side.W] | isAtSide[(int)Side.E])
					{
						heightDiff = (int)((float)(preWidth + widthDiff) * SizeRatio - (float)preHeight);
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
			if (EnableResize)
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
			if (EnableDrag)
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
	public class PanelCanvas
	{
		protected Point _dragStartPoint;
		protected Point _canvasStartPoint;
		protected bool _isDragging;
		protected bool _readyToDraw = false;
		protected float _sensitivity;

		public Panel panel;
		public Label canvasX = new Label();
		public Label canvasY = new Label();
		public Label canvasX0 = new Label();
		public Label canvasY0 = new Label();
		public Label mouseX = new Label();
		public Label mouseY = new Label();
		public Label mouseX0 = new Label();
		public Label mouseY0 = new Label();
		public PanelCanvas(Panel panel, float sensitivity = 1)
		{
			this.panel = panel;
			panel.Capture = true;
			_sensitivity = sensitivity;
			panel.MouseDown += MouseDown;
			panel.MouseUp += MouseUp;
			panel.MouseMove += MouseMove;
			// Subscribe the AdjustWorldSpaceSize method to the appropriate events
			panel.ControlAdded += AdjustWorldSpaceSize;
			panel.ControlRemoved += AdjustWorldSpaceSize;
		}
		public void AdjustWorldSpaceSize(object sender, EventArgs e)
		{
			// Calculate the minimum required size for the world space
			int minWidth = 0;
			int minHeight = 0;
			int offsetX = int.MaxValue;
			int offsetY = int.MaxValue;

			foreach (Control childControl in panel.Controls)
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
			panel.AutoScrollMinSize = new Size(minWidth, minHeight);
			panel.AutoScrollPosition = new Point(-offsetX, -offsetY);
			//Debug.WriteLine($"autoscroll: {panel.AutoScroll}");
		}
		public void MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				_dragStartPoint = new Point(e.X, e.Y);
				_isDragging = true;


				Point currentPosition = panel.AutoScrollPosition;
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
				Point currentPosition = panel.AutoScrollPosition;
				panel.AutoScrollPosition = new Point(
					-_canvasStartPoint.X - (int)((e.X - _dragStartPoint.X) * 1),
					-_canvasStartPoint.Y - (int)((e.Y - _dragStartPoint.Y) * 1));
				currentPosition = panel.AutoScrollPosition; // read the AutoScrollPosition again
				canvasX.Text = currentPosition.X.ToString();
				canvasY.Text = currentPosition.Y.ToString();
				*/
			}
		}

		public void MouseMove(object sender, MouseEventArgs e)
		{
			if (_isDragging)
			{

				panel.AutoScrollPosition = new Point(
					-_canvasStartPoint.X - (int)((e.X - _dragStartPoint.X) * _sensitivity),
					-_canvasStartPoint.Y - (int)((e.Y - _dragStartPoint.Y) * _sensitivity));
				Point currentPosition = panel.AutoScrollPosition;
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
