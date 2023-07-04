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
            tableLayoutPanel_vaultList = new TableLayoutPanel();
            tableLayoutPanel_vault0 = new TableLayoutPanel();
            button_vaultOption0 = new Button();
            label_vaultName0 = new Label();
            label_vaultPath0 = new Label();
            button1 = new Button();
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
            tableLayoutPanel_appMenu.Margin = new Padding(2);
            tableLayoutPanel_appMenu.Name = "tableLayoutPanel_appMenu";
            tableLayoutPanel_appMenu.RowCount = 1;
            tableLayoutPanel_appMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel_appMenu.Size = new Size(953, 609);
            tableLayoutPanel_appMenu.TabIndex = 0;
            // 
            // tableLayoutPanel_menuMain
            // 
            tableLayoutPanel_menuMain.ColumnCount = 1;
            tableLayoutPanel_menuMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel_menuMain.Controls.Add(tableLayoutPanel_menu_options, 0, 1);
            tableLayoutPanel_menuMain.Dock = DockStyle.Fill;
            tableLayoutPanel_menuMain.Location = new Point(335, 2);
            tableLayoutPanel_menuMain.Margin = new Padding(2);
            tableLayoutPanel_menuMain.Name = "tableLayoutPanel_menuMain";
            tableLayoutPanel_menuMain.RowCount = 3;
            tableLayoutPanel_menuMain.RowStyles.Add(new RowStyle(SizeType.Percent, 42.1052628F));
            tableLayoutPanel_menuMain.RowStyles.Add(new RowStyle(SizeType.Percent, 36.8421059F));
            tableLayoutPanel_menuMain.RowStyles.Add(new RowStyle(SizeType.Percent, 21.0526314F));
            tableLayoutPanel_menuMain.Size = new Size(616, 605);
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
            tableLayoutPanel_menu_options.Location = new Point(2, 256);
            tableLayoutPanel_menu_options.Margin = new Padding(2);
            tableLayoutPanel_menu_options.Name = "tableLayoutPanel_menu_options";
            tableLayoutPanel_menu_options.Padding = new Padding(10);
            tableLayoutPanel_menu_options.RowCount = 3;
            tableLayoutPanel_menu_options.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel_menu_options.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel_menu_options.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel_menu_options.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tableLayoutPanel_menu_options.Size = new Size(612, 218);
            tableLayoutPanel_menu_options.TabIndex = 0;
            // 
            // button_createVault
            // 
            button_createVault.Location = new Point(439, 25);
            button_createVault.Margin = new Padding(15);
            button_createVault.Name = "button_createVault";
            button_createVault.Size = new Size(148, 35);
            button_createVault.TabIndex = 0;
            button_createVault.Text = "Create";
            button_createVault.UseVisualStyleBackColor = true;
            button_createVault.Click += button_createVault_Click;
            // 
            // label_createVault
            // 
            label_createVault.AutoSize = true;
            label_createVault.Dock = DockStyle.Fill;
            label_createVault.Location = new Point(12, 10);
            label_createVault.Margin = new Padding(2, 0, 2, 0);
            label_createVault.Name = "label_createVault";
            label_createVault.Size = new Size(410, 65);
            label_createVault.TabIndex = 1;
            label_createVault.Text = "label_createVault";
            label_createVault.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel_vaultList
            // 
            tableLayoutPanel_vaultList.ColumnCount = 1;
            tableLayoutPanel_vaultList.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel_vaultList.Controls.Add(tableLayoutPanel_vault0, 0, 0);
            tableLayoutPanel_vaultList.Dock = DockStyle.Fill;
            tableLayoutPanel_vaultList.Location = new Point(3, 3);
            tableLayoutPanel_vaultList.Name = "tableLayoutPanel_vaultList";
            tableLayoutPanel_vaultList.RowCount = 5;
            tableLayoutPanel_vaultList.RowStyles.Add(new RowStyle());
            tableLayoutPanel_vaultList.RowStyles.Add(new RowStyle());
            tableLayoutPanel_vaultList.RowStyles.Add(new RowStyle());
            tableLayoutPanel_vaultList.RowStyles.Add(new RowStyle());
            tableLayoutPanel_vaultList.RowStyles.Add(new RowStyle());
            tableLayoutPanel_vaultList.Size = new Size(327, 603);
            tableLayoutPanel_vaultList.TabIndex = 2;
            // 
            // tableLayoutPanel_vault0
            // 
            tableLayoutPanel_vault0.AutoSize = true;
            tableLayoutPanel_vault0.ColumnCount = 2;
            tableLayoutPanel_vault0.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel_vault0.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel_vault0.Controls.Add(button_vaultOption0, 1, 0);
            tableLayoutPanel_vault0.Controls.Add(label_vaultName0, 0, 0);
            tableLayoutPanel_vault0.Controls.Add(label_vaultPath0, 0, 1);
            tableLayoutPanel_vault0.Dock = DockStyle.Fill;
            tableLayoutPanel_vault0.Location = new Point(3, 3);
            tableLayoutPanel_vault0.Name = "tableLayoutPanel_vault0";
            tableLayoutPanel_vault0.RowCount = 3;
            tableLayoutPanel_vault0.RowStyles.Add(new RowStyle());
            tableLayoutPanel_vault0.RowStyles.Add(new RowStyle());
            tableLayoutPanel_vault0.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tableLayoutPanel_vault0.Size = new Size(321, 119);
            tableLayoutPanel_vault0.TabIndex = 0;
            tableLayoutPanel_vault0.Click += tableLayoutPanel_vault0_Click;
            tableLayoutPanel_vault0.MouseHover += tableLayoutPanel_vault0_MouseHover;
            // 
            // button_vaultOption0
            // 
            button_vaultOption0.Location = new Point(304, 3);
            button_vaultOption0.Name = "button_vaultOption0";
            button_vaultOption0.Size = new Size(14, 27);
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
            label_vaultName0.Location = new Point(3, 0);
            label_vaultName0.Name = "label_vaultName0";
            label_vaultName0.Size = new Size(197, 33);
            label_vaultName0.TabIndex = 1;
            label_vaultName0.Text = "Recent Vault Name";
            label_vaultName0.TextAlign = ContentAlignment.MiddleLeft;
            label_vaultName0.Click += label_vaultName0_Click;
            label_vaultName0.MouseHover += label_vaultName0_MouseHover;
            // 
            // label_vaultPath0
            // 
            label_vaultPath0.AutoSize = true;
            label_vaultPath0.Dock = DockStyle.Left;
            label_vaultPath0.Location = new Point(3, 33);
            label_vaultPath0.Name = "label_vaultPath0";
            label_vaultPath0.Size = new Size(291, 76);
            label_vaultPath0.TabIndex = 2;
            label_vaultPath0.Text = "C:\\\\Users\\\\sean_wu\\\\OneDrive\\\\MyNotes\\\\Main project\\\\codeTools\\\\MyStudio\\\\MyStudio\\\\TestVault";
            label_vaultPath0.Click += label_vaultPath0_Click;
            label_vaultPath0.MouseHover += label_vaultPath0_MouseHover;
            // 
            // button1
            // 
            button1.Location = new Point(427, 78);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // AppMenu
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(953, 609);
            Controls.Add(tableLayoutPanel_appMenu);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(2);
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
        private void MyIntializeComponent()
        {
            string device = System.Environment.MachineName;
            int i = 0;
            foreach (string vaultPath in AppManager.ListVaultPathes())
            {
                RecentVault recentVault = new RecentVault(this, tableLayoutPanel_vaultList, i);
                recentVaults.Add(recentVault);
                recentVault.SetVaultInfo(new DirectoryInfo(vaultPath).Name, vaultPath);
                i++;
            }

        }

        #endregion

        private Button button1;
    }
}