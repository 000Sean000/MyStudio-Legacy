using PKG;

namespace Ui
{
	partial class UiDev
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
			label2 = new Label();
			label3 = new Label();
			tableLayoutPanel1 = new TableLayoutPanel();
			label1 = new Label();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			// 
			// label2
			// 
			label2.BackColor = SystemColors.ActiveCaption;
			label2.Location = new Point(135, 73);
			label2.Margin = new Padding(10);
			label2.Name = "label2";
			label2.Size = new Size(100, 41);
			label2.TabIndex = 2;
			label2.Text = "label2";
			// 
			// label3
			// 
			label3.BackColor = SystemColors.ActiveCaption;
			label3.Location = new Point(247, 43);
			label3.Margin = new Padding(10);
			label3.Name = "label3";
			label3.Size = new Size(100, 42);
			label3.TabIndex = 3;
			label3.Text = "label3";
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel1.Controls.Add(label1, 1, 0);
			tableLayoutPanel1.Controls.Add(label2, 1, 1);
			tableLayoutPanel1.Location = new Point(222, 202);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 2;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel1.Size = new Size(250, 125);
			tableLayoutPanel1.TabIndex = 4;
			// 
			// label1
			// 
			label1.BackColor = SystemColors.ActiveCaption;
			label1.Location = new Point(135, 11);
			label1.Margin = new Padding(10);
			label1.Name = "label1";
			label1.Size = new Size(100, 41);
			label1.TabIndex = 5;
			label1.Text = "label1";
			// 
			// UiDev
			// 
			AutoScaleDimensions = new SizeF(9F, 19F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(tableLayoutPanel1);
			Controls.Add(label3);
			Name = "UiDev";
			Text = "UiDev";
			tableLayoutPanel1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion


		public void MyInit()
		{
			new ResizableControl(label1, (label1) => { tableLayoutPanel1.Controls.Add(label1, 1, 0); });
			new ResizableControl(label2, (label2) => { tableLayoutPanel1.Controls.Add(label2, 1, 1); });
			new ResizableControl(label3);
		}


		private Label label2;
		private Label label3;
		private TableLayoutPanel tableLayoutPanel1;
		private Label label1;
	}
}