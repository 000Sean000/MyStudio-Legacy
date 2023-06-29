
using System.Drawing;

namespace MyStudio
{
    public partial class Ui_Form : Form
    {
        public Ui_Form()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (_readyToDraw)
            {
                Control node1 = button10; // these are just placeholders, replace with your actual nodes
                Control node2 = pictureBox1;

                // determine the center of each control
                Point center1 = new Point(node1.Left + node1.Width / 2, node1.Top + node1.Height / 2);
                Point center2 = new Point(node2.Left + node2.Width / 2, node2.Top + node2.Height / 2);

                // draw a line between the centers
                e.Graphics.DrawLine(Pens.Black, center1, center2);
                //_readyToDraw = false;
                //panel1.Invalidate();
            }


        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _dragStartPoint = new Point(e.X, e.Y);
                _isDragging = true;


                Point currentPosition = panel1.AutoScrollPosition;
                _canvasStartPoint = new Point(currentPosition.X, currentPosition.Y);
                canvasX0.Text = currentPosition.X.ToString();
                canvasY0.Text = currentPosition.Y.ToString();
                mouseX.Text = e.X.ToString();
                mouseY.Text = e.Y.ToString();
                mouseX0.Text = _dragStartPoint.X.ToString();
                mouseY0.Text = _dragStartPoint.Y.ToString();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _isDragging = false;
                /*
                Point currentPosition = panel1.AutoScrollPosition;
                panel1.AutoScrollPosition = new Point(
                    -_canvasStartPoint.X - (int)((e.X - _dragStartPoint.X) * 1),
                    -_canvasStartPoint.Y - (int)((e.Y - _dragStartPoint.Y) * 1));
                currentPosition = panel1.AutoScrollPosition; // read the AutoScrollPosition again
                canvasX.Text = currentPosition.X.ToString();
                canvasY.Text = currentPosition.Y.ToString();
                */
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {

                panel1.AutoScrollPosition = new Point(
                    -_canvasStartPoint.X - (int)((e.X - _dragStartPoint.X) * 1),
                    -_canvasStartPoint.Y - (int)((e.Y - _dragStartPoint.Y) * 1));
                Point currentPosition = panel1.AutoScrollPosition;
                canvasX.Text = currentPosition.X.ToString();
                canvasY.Text = currentPosition.Y.ToString();
                mouseX.Text = e.X.ToString();
                mouseY.Text = e.Y.ToString();


            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void canvasX_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            _readyToDraw = true;
            panel1.Invalidate();
            panel1.Update();
        }

        private void button10_MouseClick(object sender, MouseEventArgs e)
        {

        }


        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            //_readyToDraw = true;

        }

        private void button10_MouseUp(object sender, MouseEventArgs e)
        {
            //_readyToDraw = false;
        }
    }
}