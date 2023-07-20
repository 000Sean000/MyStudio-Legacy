using Module;
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
	public partial class VaultForm : Form
	{
		public VaultForm()
		{
			InitializeComponent();
			MyInit();
		}

		private void button_createNode_Click(object sender, EventArgs e)
		{
			AppManager.workingVault.UiCreateNode();
		}

		private void pictureBox1_MouseEnter(object sender, EventArgs e)
		{

		}
	}
}
