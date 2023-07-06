using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKG;
using Module;
namespace Ui
{
	#region App Menu
	public class RecentVault
	{
		public AppMenu appMenu;
		private TableLayoutPanel tableLayoutPanel_vault = new TableLayoutPanel();
		private Button button_vaultOption = new Button();
		private Label label_vaultName = new Label();
		private Label label_vaultPath = new Label();
		private string _vaultPath;
		//public string VaultPath { get { return _vaultPath; } }
		public RecentVault(AppMenu appMenu_, TableLayoutPanel tableLayoutPanel_vaultList, int rowIndex)
		{
			appMenu = appMenu_;

			// 
			// tableLayoutPanel_vault
			// 
			tableLayoutPanel_vault.AutoSize = true;
			tableLayoutPanel_vault.ColumnCount = 2;
			tableLayoutPanel_vault.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel_vault.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
			tableLayoutPanel_vault.Controls.Add(button_vaultOption, 1, 0);
			tableLayoutPanel_vault.Controls.Add(label_vaultName, 0, 0);
			tableLayoutPanel_vault.Controls.Add(label_vaultPath, 0, 1);
			tableLayoutPanel_vault.Dock = DockStyle.Fill;
			tableLayoutPanel_vault.Location = new Point(3, 3);
			tableLayoutPanel_vault.Name = "tableLayoutPanel_vault";
			tableLayoutPanel_vault.RowCount = 3;
			tableLayoutPanel_vault.RowStyles.Add(new RowStyle());
			tableLayoutPanel_vault.RowStyles.Add(new RowStyle());
			tableLayoutPanel_vault.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
			tableLayoutPanel_vault.Size = new Size(321, 114);
			tableLayoutPanel_vault.TabIndex = 0;
			tableLayoutPanel_vault.MouseHover += tableLayoutPanel_vault_MouseHover;
			tableLayoutPanel_vault.Click += tableLayoutPanel_vault_Click;
			// 
			// button_vaultOption
			// 
			button_vaultOption.Location = new Point(304, 3);
			button_vaultOption.Name = "button_vaultOption";
			button_vaultOption.Size = new Size(14, 27);
			button_vaultOption.TabIndex = 0;
			button_vaultOption.Text = "...";
			button_vaultOption.UseVisualStyleBackColor = true;
			button_vaultOption.Click += button_vaultOption_Click;
			button_vaultOption.MouseHover += button_vaultOption_MouseHover;
			// 
			// label_vaultName
			// 
			label_vaultName.AutoSize = true;
			label_vaultName.Dock = DockStyle.Left;
			label_vaultName.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
			label_vaultName.Location = new Point(3, 0);
			label_vaultName.Name = "label_vaultName";
			label_vaultName.Size = new Size(197, 33);
			label_vaultName.TabIndex = 1;
			label_vaultName.Text = "Recent Vault Name";
			label_vaultName.TextAlign = ContentAlignment.MiddleLeft;
			label_vaultName.Click += label_vaultName_Click;
			label_vaultName.MouseHover += label_vaultName_MouseHover;
			// 
			// label_vaultPath
			// 
			label_vaultPath.AutoSize = true;
			label_vaultPath.Dock = DockStyle.Left;
			label_vaultPath.Location = new Point(3, 33);
			label_vaultPath.Name = "label_vaultPath";
			label_vaultPath.Size = new Size(82, 50);
			label_vaultPath.TabIndex = 2;
			label_vaultPath.Text = "Vault/Path";
			label_vaultPath.Click += label_vaultPath_Click;
			label_vaultPath.MouseHover += label_vaultPath_MouseHover;

			tableLayoutPanel_vaultList.Controls.Add(tableLayoutPanel_vault, rowIndex, 0);
		}
		public void SetVaultInfo(string vaultName_,  string vaultPath_)
		{
			_vaultPath = vaultPath_;
			label_vaultName.Text = vaultName_;
			label_vaultPath.Text = _vaultPath.Replace(' ', '_');
			
			
		}
		public void OpenVault()
		{
			if (_vaultPath == null)
			{
				//...
			}
			else
			{
				Vault vault = AppManager.OpenVault(_vaultPath);
				AppMain appMain = new AppMain();
				appMain.Show();
				appMenu.Hide();
			}
		}




		#region Event Handlers
		private void tableLayoutPanel_vault_MouseHover(object sender, EventArgs e)
		{

		}
		private void tableLayoutPanel_vault_Click(object sender, EventArgs e)
		{
			OpenVault();
		}
		private void label_vaultName_Click(object sender, EventArgs e)
		{
			OpenVault();
		}
		private void label_vaultPath_Click(object sender, EventArgs e)
		{
			OpenVault();
		}
		private void button_vaultOption_Click(object sender, EventArgs e)
		{

		}

		private void label_vaultName_MouseHover(object sender, EventArgs e)
		{

		}

		private void label_vaultPath_MouseHover(object sender, EventArgs e)
		{

		}
		private void button_vaultOption_MouseHover(object sender, EventArgs e)
		{

		}
		#endregion
	}
	#endregion
}
