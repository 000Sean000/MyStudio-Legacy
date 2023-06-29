
namespace MyStudio
{
    partial class Ui_Form
    {
        private Point _dragStartPoint;
        private Point _canvasStartPoint;
        private bool _isDragging;

        private bool _readyToDraw = false;
        public void futher_init()
        {
            

        }



    }
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
        }
    }

}
