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
    
    public class ResizableControl
    {
        private Control _control;
        private Cursor _originalCursor;

        private bool isResizing = false;
        private Point resizeStartPoint;
        private Rectangle boundary;

        private int margin; // Margin or sensitivity at the boundary
        Rectangle northRegion;
        Rectangle southRegion;
        Rectangle westRegion;
        Rectangle eastRegion;
        Rectangle topLeftRegion;
        Rectangle topRightRegion;
        Rectangle bottomLeftRegion;
        Rectangle bottomRightRegion;
        public ResizableControl(Control control, int margin_ = 10)
        {
            _control = control;
            _originalCursor = new Cursor(control.Cursor.Handle);
            margin = margin_;

            boundary = _control.ClientRectangle;
            // Define the regions for each side of the boundary
            northRegion = new Rectangle(boundary.Left, boundary.Top, boundary.Width, margin);
            southRegion = new Rectangle(boundary.Left, boundary.Bottom - margin, boundary.Width, margin);
            westRegion = new Rectangle(boundary.Left, boundary.Top, margin, boundary.Height);
            eastRegion = new Rectangle(boundary.Right - margin, boundary.Top, margin, boundary.Height);
            topLeftRegion = new Rectangle(boundary.Left, boundary.Top, margin, margin);
            topRightRegion = new Rectangle(boundary.Right - margin, boundary.Top, margin, margin);
            bottomLeftRegion = new Rectangle(boundary.Left, boundary.Bottom - margin, margin, margin);
            bottomRightRegion = new Rectangle(boundary.Right - margin, boundary.Bottom - margin, margin, margin);

            _control.MouseDown += MouseDown;
            _control.MouseUp += MouseUp;
            _control.MouseMove += MouseMove;
            _control.MouseHover += MouseHover;
            _control.MouseLeave += MouseLeave;
        }
        

        private void MouseDown(object sender, MouseEventArgs e)
        {
            // Check if the mouse is near the boundary of the container
            if (IsNearBoundary(e.Location))
            {
                isResizing = true;
                resizeStartPoint = e.Location;
            }
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                // Calculate the size difference based on the mouse movement
                int widthDiff = e.X - resizeStartPoint.X;
                int heightDiff = e.Y - resizeStartPoint.Y;

                // Adjust the size of the resizable block control
                _control.Width += widthDiff;
                _control.Height += heightDiff;

                resizeStartPoint = e.Location;
            }
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            isResizing = false;
            //_control.Cursor = new Cursor(_originalCursor.Handle);
        }
        private void MouseHover(object sender, EventArgs e)
        {
            IsNearBoundary(Cursor.Position);
        }
        private void MouseLeave(object sender, EventArgs e)
        {
            //_control.Cursor = new Cursor(_originalCursor.Handle);
        }
        private bool IsNearBoundary(Point mousePosition)
        {
            
            if (northRegion.Contains(mousePosition))
            {
                // Mouse cursor is at the north side
                _control.Cursor = Cursors.SizeNS;
                Cursor.Current = _control.Cursor;
                return true;
                
            }
            else if (southRegion.Contains(mousePosition))
            {
                // Mouse cursor is at the south side
                _control.Cursor= Cursors.SizeNS;
                Cursor.Current = _control.Cursor;
                return true;
            }
            else if (westRegion.Contains(mousePosition))
            {
                // Mouse cursor is at the west side
                _control.Cursor = Cursors.SizeWE;
                Cursor.Current = _control.Cursor;
                return true;
            }
            else if (eastRegion.Contains(mousePosition))
            {
                // Mouse cursor is at the east side
                _control.Cursor = Cursors.SizeWE;
                Cursor.Current = _control.Cursor;
                return true;
            }
            else if (topLeftRegion.Contains(mousePosition))
            {
                // Mouse cursor is at the top-left corner
                _control.Cursor = Cursors.SizeNWSE;
                Cursor.Current = _control.Cursor;
                return true;
            }
            else if (topRightRegion.Contains(mousePosition))
            {
                // Mouse cursor is at the top-right corner
                _control.Cursor = Cursors.SizeNESW;
                Cursor.Current = _control.Cursor;
                return true;
            }
            else if (bottomLeftRegion.Contains(mousePosition))
            {
                // Mouse cursor is at the bottom-left corner
                _control.Cursor = Cursors.SizeNESW;
                Cursor.Current = _control.Cursor;
                return true;
            }
            else if (bottomRightRegion.Contains(mousePosition))
            {
                // Mouse cursor is at the bottom-right corner
                _control.Cursor = Cursors.SizeNWSE;
                Cursor.Current = _control.Cursor;
                return true;
            }
            else
            {
                _control.Cursor = Cursors.Hand;
                Cursor.Current = _control.Cursor;
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
