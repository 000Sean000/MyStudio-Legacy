using System.Diagnostics;

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
		Debug.WriteLine($"autoscroll: {panel.AutoScroll}");
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
