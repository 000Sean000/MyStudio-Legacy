using PKG;
using Module;
namespace Ui
{
	partial class AppMain
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
			tableLayoutPanel_NodeBorder = new TableLayoutPanel();
			tableLayoutPanel_NodeImagePlace = new TableLayoutPanel();
			tableLayoutPanel_NodeTagPlace = new TableLayoutPanel();
			textBox_NodeContent = new TextBox();
			pictureBox_NodeImage = new PictureBox();
			flowLayoutPanel_NodeTags = new FlowLayoutPanel();
			label_NodeTag = new Label();
			label_NodeTag2 = new Label();
			label3 = new Label();
			panel1 = new Panel();
			label2 = new Label();
			label1 = new Label();
			pictureBox1 = new PictureBox();
			tableLayoutPanel1 = new TableLayoutPanel();
			tableLayoutPanel_NodeBorder.SuspendLayout();
			tableLayoutPanel_NodeImagePlace.SuspendLayout();
			tableLayoutPanel_NodeTagPlace.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).BeginInit();
			flowLayoutPanel_NodeTags.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// tableLayoutPanel_NodeBorder
			// 
			tableLayoutPanel_NodeBorder.ColumnCount = 1;
			tableLayoutPanel_NodeBorder.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel_NodeBorder.Controls.Add(tableLayoutPanel_NodeImagePlace, 0, 0);
			tableLayoutPanel_NodeBorder.Location = new Point(187, 83);
			tableLayoutPanel_NodeBorder.Name = "tableLayoutPanel_NodeBorder";
			tableLayoutPanel_NodeBorder.Padding = new Padding(30);
			tableLayoutPanel_NodeBorder.RowCount = 1;
			tableLayoutPanel_NodeBorder.RowStyles.Add(new RowStyle());
			tableLayoutPanel_NodeBorder.Size = new Size(426, 284);
			tableLayoutPanel_NodeBorder.TabIndex = 2;
			// 
			// tableLayoutPanel_NodeImagePlace
			// 
			tableLayoutPanel_NodeImagePlace.AutoSize = true;
			tableLayoutPanel_NodeImagePlace.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel_NodeImagePlace.ColumnCount = 2;
			tableLayoutPanel_NodeImagePlace.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel_NodeImagePlace.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel_NodeImagePlace.Controls.Add(tableLayoutPanel_NodeTagPlace, 1, 0);
			tableLayoutPanel_NodeImagePlace.Controls.Add(flowLayoutPanel_NodeTags, 1, 1);
			tableLayoutPanel_NodeImagePlace.Controls.Add(label3, 0, 1);
			tableLayoutPanel_NodeImagePlace.Dock = DockStyle.Fill;
			tableLayoutPanel_NodeImagePlace.Location = new Point(33, 33);
			tableLayoutPanel_NodeImagePlace.Name = "tableLayoutPanel_NodeImagePlace";
			tableLayoutPanel_NodeImagePlace.RowCount = 2;
			tableLayoutPanel_NodeImagePlace.RowStyles.Add(new RowStyle());
			tableLayoutPanel_NodeImagePlace.RowStyles.Add(new RowStyle());
			tableLayoutPanel_NodeImagePlace.Size = new Size(367, 248);
			tableLayoutPanel_NodeImagePlace.TabIndex = 0;
			tableLayoutPanel_NodeImagePlace.Paint += tableLayoutPanel_NodeImagePlace_Paint;
			// 
			// tableLayoutPanel_NodeTagPlace
			// 
			tableLayoutPanel_NodeTagPlace.AutoSize = true;
			tableLayoutPanel_NodeTagPlace.ColumnCount = 2;
			tableLayoutPanel_NodeTagPlace.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel_NodeTagPlace.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel_NodeTagPlace.Controls.Add(textBox_NodeContent, 1, 0);
			tableLayoutPanel_NodeTagPlace.Controls.Add(pictureBox_NodeImage, 0, 1);
			tableLayoutPanel_NodeTagPlace.Dock = DockStyle.Fill;
			tableLayoutPanel_NodeTagPlace.Location = new Point(114, 4);
			tableLayoutPanel_NodeTagPlace.Name = "tableLayoutPanel_NodeTagPlace";
			tableLayoutPanel_NodeTagPlace.RowCount = 2;
			tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle());
			tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle());
			tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel_NodeTagPlace.Size = new Size(249, 162);
			tableLayoutPanel_NodeTagPlace.TabIndex = 0;
			// 
			// textBox_NodeContent
			// 
			textBox_NodeContent.BorderStyle = BorderStyle.None;
			textBox_NodeContent.Dock = DockStyle.Fill;
			textBox_NodeContent.Location = new Point(127, 3);
			textBox_NodeContent.Multiline = true;
			textBox_NodeContent.Name = "textBox_NodeContent";
			textBox_NodeContent.Size = new Size(119, 50);
			textBox_NodeContent.TabIndex = 3;
			textBox_NodeContent.Text = "fsd\r\nsds";
			// 
			// pictureBox_NodeImage
			// 
			pictureBox_NodeImage.BorderStyle = BorderStyle.FixedSingle;
			pictureBox_NodeImage.Image = MyStudio.Properties.Resources.kazimierz_black;
			pictureBox_NodeImage.Location = new Point(3, 59);
			pictureBox_NodeImage.Name = "pictureBox_NodeImage";
			pictureBox_NodeImage.Size = new Size(118, 100);
			pictureBox_NodeImage.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox_NodeImage.TabIndex = 1;
			pictureBox_NodeImage.TabStop = false;
			// 
			// flowLayoutPanel_NodeTags
			// 
			flowLayoutPanel_NodeTags.Controls.Add(label_NodeTag);
			flowLayoutPanel_NodeTags.Controls.Add(label_NodeTag2);
			flowLayoutPanel_NodeTags.Location = new Point(114, 173);
			flowLayoutPanel_NodeTags.Name = "flowLayoutPanel_NodeTags";
			flowLayoutPanel_NodeTags.Size = new Size(175, 39);
			flowLayoutPanel_NodeTags.TabIndex = 1;
			// 
			// label_NodeTag
			// 
			label_NodeTag.AutoSize = true;
			label_NodeTag.Location = new Point(3, 0);
			label_NodeTag.Name = "label_NodeTag";
			label_NodeTag.Size = new Size(51, 19);
			label_NodeTag.TabIndex = 0;
			label_NodeTag.Text = "label1";
			// 
			// label_NodeTag2
			// 
			label_NodeTag2.AutoSize = true;
			label_NodeTag2.Location = new Point(60, 0);
			label_NodeTag2.Name = "label_NodeTag2";
			label_NodeTag2.Size = new Size(51, 19);
			label_NodeTag2.TabIndex = 1;
			label_NodeTag2.Text = "label1";
			// 
			// label3
			// 
			label3.BackColor = SystemColors.ActiveCaption;
			label3.BorderStyle = BorderStyle.FixedSingle;
			label3.Location = new Point(11, 180);
			label3.Margin = new Padding(10);
			label3.Name = "label3";
			label3.Size = new Size(89, 57);
			label3.TabIndex = 3;
			label3.Text = "label3";
			label3.TextAlign = ContentAlignment.TopCenter;
			// 
			// panel1
			// 
			panel1.Anchor = AnchorStyles.None;
			panel1.AutoScroll = true;
			panel1.AutoScrollMinSize = new Size(500, 500);
			panel1.BackColor = SystemColors.ControlDark;
			panel1.Controls.Add(label2);
			panel1.Location = new Point(864, 55);
			panel1.Name = "panel1";
			panel1.Padding = new Padding(10);
			panel1.Size = new Size(300, 300);
			panel1.TabIndex = 7;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.None;
			label2.BackColor = SystemColors.ActiveCaption;
			label2.BorderStyle = BorderStyle.FixedSingle;
			label2.Location = new Point(20, 83);
			label2.Margin = new Padding(10);
			label2.Name = "label2";
			label2.Size = new Size(111, 50);
			label2.TabIndex = 4;
			label2.Text = "label2";
			label2.TextAlign = ContentAlignment.TopCenter;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.FlatStyle = FlatStyle.Flat;
			label1.Location = new Point(663, 138);
			label1.Name = "label1";
			label1.Size = new Size(51, 19);
			label1.TabIndex = 3;
			label1.Text = "label1";
			// 
			// pictureBox1
			// 
			pictureBox1.BackColor = Color.PapayaWhip;
			pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			pictureBox1.Image = MyStudio.Properties.Resources.kazimierz_black;
			pictureBox1.Location = new Point(653, 240);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Padding = new Padding(10);
			pictureBox1.Size = new Size(118, 100);
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 4;
			pictureBox1.TabStop = false;
			pictureBox1.SizeChanged += pictureBox1_SizeChanged;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel1.Location = new Point(757, 361);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel1.Size = new Size(133, 72);
			tableLayoutPanel1.TabIndex = 8;
			// 
			// AppMain
			// 
			AutoScaleDimensions = new SizeF(9F, 19F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1223, 450);
			Controls.Add(tableLayoutPanel1);
			Controls.Add(panel1);
			Controls.Add(pictureBox1);
			Controls.Add(label1);
			Controls.Add(tableLayoutPanel_NodeBorder);
			Name = "AppMain";
			Text = "AppMain";
			tableLayoutPanel_NodeBorder.ResumeLayout(false);
			tableLayoutPanel_NodeBorder.PerformLayout();
			tableLayoutPanel_NodeImagePlace.ResumeLayout(false);
			tableLayoutPanel_NodeImagePlace.PerformLayout();
			tableLayoutPanel_NodeTagPlace.ResumeLayout(false);
			tableLayoutPanel_NodeTagPlace.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).EndInit();
			flowLayoutPanel_NodeTags.ResumeLayout(false);
			flowLayoutPanel_NodeTags.PerformLayout();
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		public void MyInit()
		{
			new FlexiblePictureBox(pictureBox1);
			new FlexiblePictureBox(pictureBox_NodeImage);
			//UiPKG.SetPictureBoxFitImage(pictureBox1);
			new FlexibleControl(label2);
			new DraggableCanvas(panel1);
		}


		private TableLayoutPanel tableLayoutPanel_NodeBorder;
		private TableLayoutPanel tableLayoutPanel_NodeImagePlace;
		private TableLayoutPanel tableLayoutPanel_NodeTagPlace;
		private PictureBox pictureBox_NodeImage;
		private FlowLayoutPanel flowLayoutPanel_NodeTags;
		private Label label_NodeTag;
		private Label label_NodeTag2;
		private Label label1;
		private PictureBox pictureBox1;
		private Panel panel1;
		private Label label3;
		private Label label2;
		private TableLayoutPanel tableLayoutPanel1;
		public TextBox textBox_NodeContent;
	}
}