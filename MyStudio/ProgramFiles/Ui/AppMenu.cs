using Module;
using PKG;
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
	public partial class AppMenu : Form
	{
		public AppMenu()
		{
			InitializeComponent();
			AppManager.init();
			MyIntializeComponent();

		}

		private void AppMenu_Load(object sender, EventArgs e)
		{

		}

		private void button_createVault_Click(object sender, EventArgs e)
		{
			AppManager.CreateVault();
		}

		private void tableLayoutPanel_vault0_MouseHover(object sender, EventArgs e)
		{

		}

		private void tableLayoutPanel_vault0_Click(object sender, EventArgs e)
		{

		}

		private void label_vaultName0_Click(object sender, EventArgs e)
		{

		}

		private void label_vaultPath0_Click(object sender, EventArgs e)
		{

		}

		private void button_vaultOption0_Click(object sender, EventArgs e)
		{

		}

		private void label_vaultName0_MouseHover(object sender, EventArgs e)
		{

		}

		private void label_vaultPath0_MouseHover(object sender, EventArgs e)
		{

		}

		private void button_vaultOption0_MouseHover(object sender, EventArgs e)
		{

		}
	}
}
