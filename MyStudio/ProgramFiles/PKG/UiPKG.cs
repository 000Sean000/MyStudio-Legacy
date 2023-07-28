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

/*
* To prevent recursion of accessor:
* Define the direction of calling and forbit reverse direction calling.
* 
* 1. Let object accessor call flag accessor
*	e.g. GroupRoot = someControl call IsGroupRoot
* 

* 2. disable of parent don't modify child enable=>
*	refresh parent at child setter, encapsulate refresh 
*	(e.g. EnableFlex = EnableDrag || EnableResize)
*	let setter call getter, getter don't call setter
*	
* old 2. Let parent flag accessor getter refresh itself according to child flag,
*    And let getter call setter, setter don't call getter
* (Letting child flag accessor setter refresh parent will repeat many same refresh action)
*	e.g. EnableFlex = EnableDrag || EnableResize
*	
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
	
/*
	 * Remember to set property "AutoSize" of the control to False!!!!!
	 * Do not put the control in "table layout panel", it will work badly
	 * It is better to put the control in a "panel"
	 */
	public class ExTextBox : ExControl
	{
		private TextBox textBox;
		private int basicWidth;
		private int basicHeight;
		public int space = 10; // reserve some space for correct resizing

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

				int newWidth = Math.Max((int)textSize.Width + space, basicWidth);  // Add some padding

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
		#region Fields & Accessors
		public Control control;
		public Control parent; // parent control

		#region Functionality Enable
		protected bool _enablePaintBorder;
		public bool EnablePaintBorder
		{
			set
			{
				_enablePaintBorder = value;
				if (_enablePaintBorder)
				{
					Subscribe_PaintBorder_Handlers();
				}
				else
				{
					Unsubscribe_PaintBorder_Handlers();
				}
				BorderColor = original_borderColor;
			}
			get
			{
				return _enablePaintBorder;
			}
		}
		protected bool _enableGroup;
		protected bool _enableFlex;
		public bool EnableGroup
		{
			set
			{
				_enableGroup = value;
				if (_enableGroup) { Subscribe_Group_Handlers(); }
				else { Unsubscribe_Group_Handlers(); }
				/// no group hander to subscribe, only root need to subscribe GroupRootHanders
			}
			get
			{
				return _enableGroup;
			}
		}
		public void Refresh_EnableGroup()
		{
			///EnableGroup = GroupRoot != null || GroupInnerBody != null; 
			EnableGroup = IsGroupRoot || GroupRoot != null;
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
				return _enableFlex;
			}
		}
		public void Refresh_EnableFlex() { EnableFlex = EnableDrag || EnableResize; }
		public bool _enableDrag;
		public bool _enableResize;
		public bool _enableRatioFixed;
		public bool EnableDrag
		{
			set
			{
				_enableDrag = value;
				if (_enableDrag) Refresh_EnableFlex();
			}
			get { return _enableDrag; }
		}
		public bool EnableResize
		{
			set
			{
				_enableResize = value;
				if (_enableResize) Refresh_EnableFlex();
			}
			get 
			{
				return _enableResize; 
			}
		}
		public void Refresh_EnableResize() { EnableResize = EnableResize || EnableRatioFixed; }
		public bool EnableRatioFixed
		{
			set
			{
				Refresh_EnableResize();
				_enableRatioFixed = value;
			}
			get { return _enableRatioFixed; }
		}
		#endregion
		#region Appearance Extension
		public Color original_borderColor = Color.Black;
		public Color hover_borderColor = Color.Cyan;
		public Color rootOpen_borderColor = Color.Blue;
		protected Color _backColor = Color.Gray;
		protected Color _borderColor; // default border color
		protected int _borderThickness = 5;
		public Color BackColor
		{
			get { return _backColor; }
			set
			{
				_backColor = value;
				control.Invalidate(); // Trigger repaint
			}
		}
		public Color BorderColor
		{
			get { return _borderColor; }
			set
			{
				if (EnablePaintBorder)
				{
					_borderColor = value;
					control.Invalidate(); // Trigger repaint
				}
			}
		}

		public int BorderThickness
		{
			get { return _borderThickness; }
			set
			{
				_borderThickness = value;
				control.Invalidate(); // Trigger repaint
			}
		}

		#endregion
		#region Group Extension
		public enum GroupSt // Group States
		{
			BeyondGroup, AimingGroup, EditingGroup, 
		}
		protected GroupSt _groupState = GroupSt.BeyondGroup;
		public GroupSt GroupState
		{
			set
			{
				if (!IsGroupRoot)
				{
					Debug.WriteLine($"\t this is not groupRoot: {this}");
					return;
				}
				_groupState = value;
				if (_groupState == GroupSt.BeyondGroup)
				{
					control.Capture = false;
					BorderColor = original_borderColor;
					Debug.WriteLine($"\tGroupState: {_groupState}");
				}
				else if (_groupState == GroupSt.AimingGroup)
				{
					control.Capture = true;
					_isRootOpen = false;
					BorderColor = hover_borderColor;
					Debug.WriteLine($"\tGroupState: {_groupState}");
				}
				else if (_groupState == GroupSt.EditingGroup)
				{
					control.Capture = false;
					_isRootOpen = true;
					BorderColor = rootOpen_borderColor;
					Debug.WriteLine($"\tGroupState: {_groupState}");
				}
				else
				{
					Debug.WriteLine($"\tGroupState: {_groupState}");
				}
			}
			get { return _groupState; }
		}
		public ExControl? _groupRoot = null; // GroupRoot control of group container
		public ExControl? GroupRoot
		{
			set
			{
				_groupRoot = value;
				//Debug.WriteLine($"\t_groupRoot:{_groupRoot}, control:{control}");
				Refresh_EnableGroup();
				if (_groupRoot != null)
				{
					if (!_groupRoot.IsGroupRoot)
					{
						_groupRoot.IsGroupRoot = true;
					}
				}
				
			}
			get { return _groupRoot; }
		}
		protected bool _isGroupRoot;
		/*
		protected ExControl? _groupInnerBody; // to track child's isRootOpen
		public ExControl? GroupInnerBody
		{
			set
			{
				_groupInnerBody = value;
				if (_groupInnerBody != null)
				{
					///GroupRoot = this; it may have higher level root
					IsGroupRoot = true;
					Refresh_EnableGroup();
					//Debug.WriteLine($"\tGroupInnerBody is not null. IsGroupRoot:{IsGroupRoot}");
				}
				else
				{
					IsGroupRoot = false;
					Debug.WriteLine("\tGroupInnerBody is null!");
				}
			}
			get { return _groupInnerBody; }
		}
		*/
		protected bool _isRootOpen;
		public bool IsGroupRoot
		{
			set
			{
				_isGroupRoot = value;
				Refresh_EnableGroup();
				/*
				if (_isGroupRoot)
				{
					Subscribe_Group_Handlers();
				}
				else
				{
					Unsubscribe_Group_Handlers();
				}
				*/
			}
			get { return _isGroupRoot; }
		}
		public void Refresh_IsRootOpen()
		{
			if (!EnableGroup || GroupRoot == null || IsGroupRoot)
			{
				//Debug.WriteLine($"\tEnableGroup:{EnableGroup}, GroupRoot == null:{GroupRoot == null}, IsGroupRoot:{IsGroupRoot}");
				IsRootOpen = true;
			}
			else
			{
				if (GroupRoot.GroupState == GroupSt.EditingGroup) { IsRootOpen = true; }
				else { IsRootOpen = false; }
			}
		}
		public bool IsRootOpen // the group is being selected
		{
			set
			{
				_isRootOpen = value;
			}
			get
			{
				Refresh_IsRootOpen(); // to be put to child setter
				return _isRootOpen;
			}
		}
		#endregion
		#region Flex Extension
		public enum Side
		{
			N, S, W, E, None
		}
		protected Cursor originalCursor;
		protected int _borderSize; // border BorderSize
		public int BorderSize
		{
			set
			{
				_borderSize = value;
				DefineBasicSize();
			}
			get { return _borderSize; }
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
		protected bool[] isAtSide = new bool[4];
		protected Point preCursorPoint; // previous cursor point
		protected Point dragingStartPoint;
		protected int verticalMove, horizontalMove; // mouse movement
		protected int preWidth, preHeight; // previous W/H
		protected int widthDiff, heightDiff; // difference
		#endregion

		#endregion

		#region Constructor
		public ExControl(Control control_)
		{
			
			control = control_;
			init();
		}
		#region Constructors with: "SizeRatio = ...", ".AutoSize = false"
		public ExControl(Label control_)
		{
			control = control_;
			control.AutoSize = false;
			init();
		}
		#endregion
		public void init()
		{
			EnablePaintBorder = false;
			EnableGroup = false;
			IsGroupRoot = false;
			EnableFlex = false;
			EnableDrag = false;
			EnableResize = false;
			EnableRatioFixed = false;

			parent = control.Parent;
			if (parent == null) { Debug.WriteLine($"\tparent is null: {control}"); }
			originalCursor = new Cursor(control.Cursor.Handle);
			BorderSize = 10;

			

		}
		public void DefineBasicSize()
		{
			basicWidth = 5 * BorderSize;
			basicHeight = 5 * BorderSize;
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
		public void CheckFunctionalityEnable()
		{
			Debug.WriteLine($"\t>>CheckFunctionalityEnable");
			Debug.WriteLine($"\tEnableGroup:{EnableGroup}");
			Debug.WriteLine($"\tIsGroupRoot:{IsGroupRoot}");
			Debug.WriteLine($"\tEnableFlex:{EnableFlex}");
			Debug.WriteLine($"\tEnableDrag:{EnableDrag}");
			Debug.WriteLine($"\tEnableResize:{EnableResize}");
			Debug.WriteLine($"\tEnableRatioFixed:{EnableRatioFixed}");
		}

		#region Appearance Extension
		public void Subscribe_PaintBorder_Handlers()
		{
			Unsubscribe_PaintBorder_Handlers();
			control.Paint += Appearance_Paint;
			control.SizeChanged += Appearance_SizeChanged;
		}
		public void Unsubscribe_PaintBorder_Handlers()
		{
			control.Paint -= Appearance_Paint;
			control.SizeChanged -= Appearance_SizeChanged;
		}
		public void DrawBorder(PaintEventArgs e)
		{
			_borderThickness = 1;
			// Draw the custom border using the specified color and thickness
			using (var pen = new Pen(BorderColor, BorderThickness))
			{
				int shift = 3; // error shift
				Rectangle borderRect = new Rectangle(0, 0, control.Width - shift, control.Height - shift);
				e.Graphics.DrawRectangle(pen, borderRect);
			}
		}

		protected void DrawBackground(PaintEventArgs e)
		{

			using (var backBrush = new SolidBrush(BackColor))
			{
				Rectangle rect = control.ClientRectangle;
				e.Graphics.FillRectangle(backBrush, rect);
				//Debug.WriteLine($"\t({rect.Width} x {rect.Height}) at ({rect.X}, {rect.Y}) ");
			}
			///if (_backColor != Color.Gray) { _backColor = Color.Gray; }
			///else { _backColor = Color.Blue; }
		}
		private void Appearance_SizeChanged(object sender, EventArgs e)
		{
			control.Invalidate();
		}


		// Pass the Paint event to draw border and background
		public void Appearance_Paint(object sender, PaintEventArgs e)
		{
			DrawBackground(e);
			DrawBorder(e);
		}
		private void PaintBorder(object sender, PaintEventArgs e)
		{

			// Draw the custom border using the specified color
			using (var pen = new Pen(BorderColor, BorderThickness))
			{
				int shift = 3; // error shift
				Rectangle borderRect = new Rectangle(0, 0, control.Width - shift, control.Height - shift);
				e.Graphics.DrawRectangle(pen, borderRect);
			}
			if (_borderColor == Color.Red) { _borderColor = Color.Green; }
			else { _borderColor = Color.Red; }
			
		}
		#endregion
		#region Group Extension
		public void Subscribe_Group_Handlers()
		{
			Unsubscribe_Group_Handlers();// prevent duplicated handler subscription
			control.MouseEnter += Group_MouseEnter;
			control.MouseMove += Group_MouseMove;
			control.Click += Group_Click;
			Debug.WriteLine("\tGroup Extension Subscription");
		}
		public void Unsubscribe_Group_Handlers()
		{
			control.MouseEnter -= Group_MouseEnter;
			control.MouseMove -= Group_MouseMove;
			control.Click -= Group_Click;
			Debug.WriteLine("\tGroup Extension Unsubscription");
		}
		public void Group_MouseEnter(object sender, EventArgs e)
		{
			if (IsGroupRoot)
			{
				Debug.WriteLine($"\tIsGroupRoot:{IsGroupRoot}");
				if (GroupState == GroupSt.BeyondGroup)
				{
					GroupState = GroupSt.AimingGroup;
				}
				Debug.WriteLine($"\tCapture:{control.Capture}");
			}
			
		}
		// MouseLeave will not be triggered when capture is still true
		// so use MouseMove
		public void Group_MouseMove(object sender, MouseEventArgs e) 
		{
			if (IsGroupRoot)
			{
				///Debug.WriteLine($"control.Location:{control.Location}, Cursor:{e.Location}");
				if (!control.ClientRectangle.Contains(e.Location)) // leave control
				{
					if (GroupState == GroupSt.AimingGroup)
					{
						GroupState = GroupSt.BeyondGroup;
					}
				}
			}
				
		}
		public void Group_Click(object sender, EventArgs e) // represent a quick click
		{
			if (IsGroupRoot)
			{
				if (GroupState == GroupSt.AimingGroup)
				{
					GroupState = GroupSt.EditingGroup;
				}
				else if (GroupState == GroupSt.EditingGroup)
				{
					GroupState = GroupSt.AimingGroup;
				}
			}
			else // Group Members
			{
				if (GroupRoot != null)
				{
					if (GroupRoot.GroupState == GroupSt.EditingGroup)
					{
						GroupRoot.GroupState = GroupSt.AimingGroup;
					}
				}
				
			}
			
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
			//Debug.WriteLine($"\tIsRootOpen:{IsRootOpen}");
			if (!IsRootOpen) { return; } 
			if (isResizing)
			{
				if (EnableResize)
				{
					// Calculate the size difference based on the mouse movement
					preWidth = control.Width;
					preHeight = control.Height;
					horizontalMove = e.X - preCursorPoint.X;
					verticalMove = e.Y - preCursorPoint.Y;
					CalculateSizeDifference();
					ResizeControl(e);
				}
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
			if (EnableFlex)
			{
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
			

		}
		protected virtual void Flex_MouseUp(object sender, MouseEventArgs e)
		{
			if (!IsRootOpen) { return; }
			if (EnableFlex)
			{
				isResizing = false;
				isDragging = false;
				//RecoverCursor();
				DefineBorder();
			}
				
		}
		protected void Flex_MouseLeave(object sender, EventArgs e)
		{
			if (!IsRootOpen) { return; }
			//RecoverCursor();
		}
		#endregion
		#region Functions for Handlers
		public void DefineBorder()
		{
			wholeRegion = control.ClientRectangle;
			// Define the regions for each side of the wholeRegion
			borderN = new Rectangle(wholeRegion.Left, wholeRegion.Top, wholeRegion.Width, BorderSize);
			borderS = new Rectangle(wholeRegion.Left, wholeRegion.Bottom - BorderSize, wholeRegion.Width, BorderSize);
			borderW = new Rectangle(wholeRegion.Left, wholeRegion.Top, BorderSize, wholeRegion.Height);
			borderE = new Rectangle(wholeRegion.Right - BorderSize, wholeRegion.Top, BorderSize, wholeRegion.Height);
			
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

			///Debug.WriteLine("Resize to " + control.Size.ToString() + " at " + control.Location.ToString());
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

				///Debug.WriteLine("SizeRatio: " + SizeRatio);
				///Debug.WriteLine("Ratio: " + (float)preHeight / (float)preWidth);
				
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
			///Debug.WriteLine("widthDiff: " + widthDiff + "  heightDiff: " + heightDiff);
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
	public class ExPanel
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
		public ExPanel(Panel panel, float sensitivity = 1)
		{
			this.panel = panel;
			panel.Capture = true;
			_sensitivity = sensitivity;
			panel.MouseDown += MouseDown;
			panel.MouseUp += MouseUp;
			panel.MouseMove += MouseMove;
			// Subscribe the AdjustWorldSpaceSize method to the appropriate events
			///panel.ControlAdded += AdjustWorldSpaceSize;
			///panel.ControlRemoved += AdjustWorldSpaceSize;
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
