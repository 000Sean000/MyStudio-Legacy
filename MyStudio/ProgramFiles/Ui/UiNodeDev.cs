using Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ui
{
	public partial class UiNodeDev : Form
	{
		public UiNodeDev()
		{
			InitializeComponent();
			MyInit();
		}

		private void pictureBox1_SizeChanged(object sender, EventArgs e)
		{

		}

		private void tableLayoutPanel_NodeImagePlace_Paint(object sender, PaintEventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Debug.WriteLine(textBox1.Text);
			string input = "This \nis a [[citation1]]. Another [[citation2]] here.";

			List<string> separatedStrings = Node.SeparateSubstringsAndCitations(input);

			foreach (string substring in separatedStrings)
			{
				Debug.WriteLine(substring+'\n');
			}

		}
	}
}
