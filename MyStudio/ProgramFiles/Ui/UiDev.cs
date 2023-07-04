using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ui
{
    public partial class UiDev : Form
    {
        public UiDev()
        {
            InitializeComponent();
            MyInit();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            isHovering = true;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void label_ex_MouseLeave(object sender, EventArgs e)
        {
            isHovering &= false;
        }

        private void label_ex_MouseMove(object sender, MouseEventArgs e)
        {
            if (isHovering)
            {
                Point cursorPosition = label_ex.PointToClient(Cursor.Position);
                label_ex.Text = cursorPosition.ToString();
            }
        }
    }
}
