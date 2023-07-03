using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace PKG
{
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
