using Module;
namespace Ui
{
	partial class AppMenu
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			tableLayoutPanel_appMenu = new TableLayoutPanel();
			tableLayoutPanel_menuMain = new TableLayoutPanel();
			tableLayoutPanel_menu_options = new TableLayoutPanel();
			button_createVault = new Button();
			label_createVault = new Label();
			button1 = new Button();
			tableLayoutPanel_vaultList = new TableLayoutPanel();
			tableLayoutPanel_vault0 = new TableLayoutPanel();
			button_vaultOption0 = new Button();
			label_vaultName0 = new Label();
			label_vaultPath0 = new Label();
			tableLayoutPanel_appMenu.SuspendLayout();
			tableLayoutPanel_menuMain.SuspendLayout();
			tableLayoutPanel_menu_options.SuspendLayout();
			tableLayoutPanel_vaultList.SuspendLayout();
			tableLayoutPanel_vault0.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel_appMenu
			// 
			tableLayoutPanel_appMenu.ColumnCount = 2;
			tableLayoutPanel_appMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
			tableLayoutPanel_appMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
			tableLayoutPanel_appMenu.Controls.Add(tableLayoutPanel_menuMain, 1, 0);
			tableLayoutPanel_appMenu.Controls.Add(tableLayoutPanel_vaultList, 0, 0);
			tableLayoutPanel_appMenu.Dock = DockStyle.Fill;
			tableLayoutPanel_appMenu.Location = new Point(0, 0);
			tableLayoutPanel_appMenu.Name = "tableLayoutPanel_appMenu";
			tableLayoutPanel_appMenu.RowCount = 1;
			tableLayoutPanel_appMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel_appMenu.Size = new Size(1271, 833);
			tableLayoutPanel_appMenu.TabIndex = 0;
			// 
			// tableLayoutPanel_menuMain
			// 
			tableLayoutPanel_menuMain.ColumnCount = 1;
			tableLayoutPanel_menuMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel_menuMain.Controls.Add(tableLayoutPanel_menu_options, 0, 1);
			tableLayoutPanel_menuMain.Dock = DockStyle.Fill;
			tableLayoutPanel_menuMain.Location = new Point(447, 3);
			tableLayoutPanel_menuMain.Name = "tableLayoutPanel_menuMain";
			tableLayoutPanel_menuMain.RowCount = 3;
			tableLayoutPanel_menuMain.RowStyles.Add(new RowStyle(SizeType.Percent, 42.1052628F));
			tableLayoutPanel_menuMain.RowStyles.Add(new RowStyle(SizeType.Percent, 36.8421059F));
			tableLayoutPanel_menuMain.RowStyles.Add(new RowStyle(SizeType.Percent, 21.0526314F));
			tableLayoutPanel_menuMain.Size = new Size(821, 827);
			tableLayoutPanel_menuMain.TabIndex = 1;
			// 
			// tableLayoutPanel_menu_options
			// 
			tableLayoutPanel_menu_options.ColumnCount = 2;
			tableLayoutPanel_menu_options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
			tableLayoutPanel_menu_options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
			tableLayoutPanel_menu_options.Controls.Add(button_createVault, 1, 0);
			tableLayoutPanel_menu_options.Controls.Add(label_createVault, 0, 0);
			tableLayoutPanel_menu_options.Controls.Add(button1, 1, 1);
			tableLayoutPanel_menu_options.Dock = DockStyle.Fill;
			tableLayoutPanel_menu_options.Location = new Point(3, 351);
			tableLayoutPanel_menu_options.Name = "tableLayoutPanel_menu_options";
			tableLayoutPanel_menu_options.Padding = new Padding(13, 14, 13, 14);
			tableLayoutPanel_menu_options.RowCount = 3;
			tableLayoutPanel_menu_options.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel_menu_options.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel_menu_options.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel_menu_options.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
			tableLayoutPanel_menu_options.Size = new Size(815, 298);
			tableLayoutPanel_menu_options.TabIndex = 0;
			// 
			// button_createVault
			// 
			button_createVault.Location = new Point(585, 35);
			button_createVault.Margin = new Padding(20, 21, 20, 21);
			button_createVault.Name = "button_createVault";
			button_createVault.Size = new Size(197, 48);
			button_createVault.TabIndex = 0;
			button_createVault.Text = "Create";
			button_createVault.UseVisualStyleBackColor = true;
			button_createVault.Click += button_createVault_Click;
			// 
			// label_createVault
			// 
			label_createVault.AutoSize = true;
			label_createVault.Dock = DockStyle.Fill;
			label_createVault.Location = new Point(16, 14);
			label_createVault.Name = "label_createVault";
			label_createVault.Size = new Size(546, 90);
			label_createVault.TabIndex = 1;
			label_createVault.Text = "label_createVault";
			label_createVault.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// button1
			// 
			button1.Location = new Point(569, 108);
			button1.Margin = new Padding(4, 4, 4, 4);
			button1.Name = "button1";
			button1.Size = new Size(125, 40);
			button1.TabIndex = 2;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel_vaultList
			// 
			tableLayoutPanel_vaultList.ColumnCount = 1;
			tableLayoutPanel_vaultList.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel_vaultList.Controls.Add(tableLayoutPanel_vault0, 0, 0);
			tableLayoutPanel_vaultList.Dock = DockStyle.Fill;
			tableLayoutPanel_vaultList.Location = new Point(4, 4);
			tableLayoutPanel_vaultList.Margin = new Padding(4, 4, 4, 4);
			tableLayoutPanel_vaultList.Name = "tableLayoutPanel_vaultList";
			tableLayoutPanel_vaultList.RowCount = 5;
			tableLayoutPanel_vaultList.RowStyles.Add(new RowStyle());
			tableLayoutPanel_vaultList.RowStyles.Add(new RowStyle());
			tableLayoutPanel_vaultList.RowStyles.Add(new RowStyle());
			tableLayoutPanel_vaultList.RowStyles.Add(new RowStyle());
			tableLayoutPanel_vaultList.RowStyles.Add(new RowStyle());
			tableLayoutPanel_vaultList.Size = new Size(436, 825);
			tableLayoutPanel_vaultList.TabIndex = 2;
			// 
			// tableLayoutPanel_vault0
			// 
			tableLayoutPanel_vault0.AutoSize = true;
			tableLayoutPanel_vault0.ColumnCount = 2;
			tableLayoutPanel_vault0.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel_vault0.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 27F));
			tableLayoutPanel_vault0.Controls.Add(button_vaultOption0, 1, 0);
			tableLayoutPanel_vault0.Controls.Add(label_vaultName0, 0, 0);
			tableLayoutPanel_vault0.Controls.Add(label_vaultPath0, 0, 1);
			tableLayoutPanel_vault0.Dock = DockStyle.Fill;
			tableLayoutPanel_vault0.Location = new Point(4, 4);
			tableLayoutPanel_vault0.Margin = new Padding(4, 4, 4, 4);
			tableLayoutPanel_vault0.Name = "tableLayoutPanel_vault0";
			tableLayoutPanel_vault0.RowCount = 3;
			tableLayoutPanel_vault0.RowStyles.Add(new RowStyle());
			tableLayoutPanel_vault0.RowStyles.Add(new RowStyle());
			tableLayoutPanel_vault0.RowStyles.Add(new RowStyle(SizeType.Absolute, 14F));
			tableLayoutPanel_vault0.Size = new Size(428, 85);
			tableLayoutPanel_vault0.TabIndex = 0;
			tableLayoutPanel_vault0.Click += tableLayoutPanel_vault0_Click;
			tableLayoutPanel_vault0.MouseHover += tableLayoutPanel_vault0_MouseHover;
			// 
			// button_vaultOption0
			// 
			button_vaultOption0.Location = new Point(405, 4);
			button_vaultOption0.Margin = new Padding(4, 4, 4, 4);
			button_vaultOption0.Name = "button_vaultOption0";
			button_vaultOption0.Size = new Size(19, 37);
			button_vaultOption0.TabIndex = 0;
			button_vaultOption0.Text = "...";
			button_vaultOption0.UseVisualStyleBackColor = true;
			button_vaultOption0.Click += button_vaultOption0_Click;
			button_vaultOption0.MouseHover += button_vaultOption0_MouseHover;
			// 
			// label_vaultName0
			// 
			label_vaultName0.AutoSize = true;
			label_vaultName0.Dock = DockStyle.Left;
			label_vaultName0.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
			label_vaultName0.Location = new Point(4, 0);
			label_vaultName0.Margin = new Padding(4, 0, 4, 0);
			label_vaultName0.Name = "label_vaultName0";
			label_vaultName0.Size = new Size(341, 45);
			label_vaultName0.TabIndex = 1;
			label_vaultName0.Text = "Recent Vault Name Example";
			label_vaultName0.TextAlign = ContentAlignment.MiddleLeft;
			label_vaultName0.Click += label_vaultName0_Click;
			label_vaultName0.MouseHover += label_vaultName0_MouseHover;
			// 
			// label_vaultPath0
			// 
			label_vaultPath0.AutoSize = true;
			label_vaultPath0.Dock = DockStyle.Left;
			label_vaultPath0.Location = new Point(4, 45);
			label_vaultPath0.Margin = new Padding(4, 0, 4, 0);
			label_vaultPath0.Name = "label_vaultPath0";
			label_vaultPath0.Size = new Size(237, 26);
			label_vaultPath0.TabIndex = 2;
			label_vaultPath0.Text = "example/path/of/vault";
			label_vaultPath0.Click += label_vaultPath0_Click;
			label_vaultPath0.MouseHover += label_vaultPath0_MouseHover;
			// 
			// AppMenu
			// 
			AutoScaleDimensions = new SizeF(12F, 26F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.Window;
			ClientSize = new Size(1271, 833);
			Controls.Add(tableLayoutPanel_appMenu);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			Name = "AppMenu";
			Text = "AppMenu";
			Load += AppMenu_Load;
			tableLayoutPanel_appMenu.ResumeLayout(false);
			tableLayoutPanel_menuMain.ResumeLayout(false);
			tableLayoutPanel_menu_options.ResumeLayout(false);
			tableLayoutPanel_menu_options.PerformLayout();
			tableLayoutPanel_vaultList.ResumeLayout(false);
			tableLayoutPanel_vaultList.PerformLayout();
			tableLayoutPanel_vault0.ResumeLayout(false);
			tableLayoutPanel_vault0.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel_appMenu;
		private TableLayoutPanel tableLayoutPanel_menuMain;
		private TableLayoutPanel tableLayoutPanel_menu_options;
		private Button button_createVault;
		private Label label_createVault;
		private TableLayoutPanel tableLayoutPanel_vaultList;
		private TableLayoutPanel tableLayoutPanel_vault0;
		private Button button_vaultOption0;
		private Label label_vaultName0;
		private Label label_vaultPath0;
		#region MyUiInitialization

		public List<RecentVault> recentVaults = new List<RecentVault>();
		public int vault_index;
		private void MyIntializeComponent()
		{
			AppManager.appMenu = this;
			string device = System.Environment.MachineName;
			vault_index = 0;
			foreach (string vaultPath in AppManager.ListVaultPathes())
			{
				RecentVault recentVault = new RecentVault(this, tableLayoutPanel_vaultList, vault_index);
				recentVaults.Add(recentVault);
				recentVault.SetVaultInfo(new DirectoryInfo(vaultPath).Name, vaultPath);
				vault_index++;
			}

		}

		#endregion

		private Button button1;
	}
}