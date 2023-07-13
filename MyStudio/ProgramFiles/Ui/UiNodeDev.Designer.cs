using PKG;
using Module;
namespace Ui
{
	partial class UiNodeDev
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
			panel_Node = new Panel();
			pictureBox1 = new PictureBox();
			flowLayoutPanel_NodeTags = new FlowLayoutPanel();
			label_NodeTag = new Label();
			label_NodeTag2 = new Label();
			label3 = new Label();
			textBox1 = new TextBox();
			textBox2 = new TextBox();
			button1 = new Button();
			tableLayoutPanel_NodeBorder.SuspendLayout();
			tableLayoutPanel_NodeImagePlace.SuspendLayout();
			tableLayoutPanel_NodeTagPlace.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).BeginInit();
			panel_Node.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			flowLayoutPanel_NodeTags.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel_NodeBorder
			// 
			tableLayoutPanel_NodeBorder.AutoSize = true;
			tableLayoutPanel_NodeBorder.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel_NodeBorder.ColumnCount = 1;
			tableLayoutPanel_NodeBorder.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel_NodeBorder.Controls.Add(tableLayoutPanel_NodeImagePlace, 0, 0);
			tableLayoutPanel_NodeBorder.Location = new Point(187, 83);
			tableLayoutPanel_NodeBorder.Name = "tableLayoutPanel_NodeBorder";
			tableLayoutPanel_NodeBorder.Padding = new Padding(30);
			tableLayoutPanel_NodeBorder.RowCount = 1;
			tableLayoutPanel_NodeBorder.RowStyles.Add(new RowStyle());
			tableLayoutPanel_NodeBorder.Size = new Size(428, 293);
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
			tableLayoutPanel_NodeImagePlace.Location = new Point(34, 34);
			tableLayoutPanel_NodeImagePlace.Name = "tableLayoutPanel_NodeImagePlace";
			tableLayoutPanel_NodeImagePlace.RowCount = 2;
			tableLayoutPanel_NodeImagePlace.RowStyles.Add(new RowStyle());
			tableLayoutPanel_NodeImagePlace.RowStyles.Add(new RowStyle());
			tableLayoutPanel_NodeImagePlace.Size = new Size(360, 225);
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
			tableLayoutPanel_NodeTagPlace.Controls.Add(panel_Node, 1, 1);
			tableLayoutPanel_NodeTagPlace.Dock = DockStyle.Fill;
			tableLayoutPanel_NodeTagPlace.Location = new Point(78, 4);
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
			tableLayoutPanel_NodeTagPlace.Size = new Size(278, 171);
			tableLayoutPanel_NodeTagPlace.TabIndex = 0;
			// 
			// textBox_NodeContent
			// 
			textBox_NodeContent.BorderStyle = BorderStyle.None;
			textBox_NodeContent.Dock = DockStyle.Fill;
			textBox_NodeContent.Location = new Point(115, 3);
			textBox_NodeContent.Multiline = true;
			textBox_NodeContent.Name = "textBox_NodeContent";
			textBox_NodeContent.Size = new Size(160, 50);
			textBox_NodeContent.TabIndex = 3;
			textBox_NodeContent.Text = "fsd\r\nsds";
			// 
			// pictureBox_NodeImage
			// 
			pictureBox_NodeImage.BorderStyle = BorderStyle.FixedSingle;
			pictureBox_NodeImage.Image = MyStudio.Properties.Resources.kazimierz_black;
			pictureBox_NodeImage.Location = new Point(3, 59);
			pictureBox_NodeImage.Name = "pictureBox_NodeImage";
			pictureBox_NodeImage.Size = new Size(106, 93);
			pictureBox_NodeImage.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox_NodeImage.TabIndex = 1;
			pictureBox_NodeImage.TabStop = false;
			// 
			// panel_Node
			// 
			panel_Node.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			panel_Node.BorderStyle = BorderStyle.FixedSingle;
			panel_Node.Controls.Add(pictureBox1);
			panel_Node.Location = new Point(115, 59);
			panel_Node.Name = "panel_Node";
			panel_Node.Size = new Size(139, 109);
			panel_Node.TabIndex = 5;
			// 
			// pictureBox1
			// 
			pictureBox1.BackColor = Color.PapayaWhip;
			pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			pictureBox1.Image = MyStudio.Properties.Resources.kazimierz_black;
			pictureBox1.Location = new Point(38, 8);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Padding = new Padding(10);
			pictureBox1.Size = new Size(100, 100);
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 4;
			pictureBox1.TabStop = false;
			pictureBox1.SizeChanged += pictureBox1_SizeChanged;
			// 
			// flowLayoutPanel_NodeTags
			// 
			flowLayoutPanel_NodeTags.Controls.Add(label_NodeTag);
			flowLayoutPanel_NodeTags.Controls.Add(label_NodeTag2);
			flowLayoutPanel_NodeTags.Location = new Point(78, 182);
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
			label3.AutoSize = true;
			label3.BackColor = SystemColors.ActiveCaption;
			label3.BorderStyle = BorderStyle.FixedSingle;
			label3.Location = new Point(11, 189);
			label3.Margin = new Padding(10);
			label3.Name = "label3";
			label3.Size = new Size(53, 21);
			label3.TabIndex = 3;
			label3.Text = "label3";
			label3.TextAlign = ContentAlignment.TopCenter;
			// 
			// textBox1
			// 
			textBox1.Location = new Point(878, 156);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(125, 27);
			textBox1.TabIndex = 5;
			textBox1.Text = "ssdfs";
			textBox1.TextChanged += textBox1_TextChanged;
			// 
			// textBox2
			// 
			textBox2.AcceptsReturn = true;
			textBox2.Location = new Point(878, 218);
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(125, 27);
			textBox2.TabIndex = 6;
			// 
			// button1
			// 
			button1.Location = new Point(935, 296);
			button1.Name = "button1";
			button1.Size = new Size(94, 29);
			button1.TabIndex = 7;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// UiNodeDev
			// 
			AutoScaleDimensions = new SizeF(9F, 19F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1223, 450);
			Controls.Add(button1);
			Controls.Add(textBox2);
			Controls.Add(textBox1);
			Controls.Add(tableLayoutPanel_NodeBorder);
			Name = "UiNodeDev";
			Text = "AppMain";
			tableLayoutPanel_NodeBorder.ResumeLayout(false);
			tableLayoutPanel_NodeBorder.PerformLayout();
			tableLayoutPanel_NodeImagePlace.ResumeLayout(false);
			tableLayoutPanel_NodeImagePlace.PerformLayout();
			tableLayoutPanel_NodeTagPlace.ResumeLayout(false);
			tableLayoutPanel_NodeTagPlace.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).EndInit();
			panel_Node.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			flowLayoutPanel_NodeTags.ResumeLayout(false);
			flowLayoutPanel_NodeTags.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		public void MyInit()
		{
			new FlexiblePictureBox(pictureBox1);
			new FlexiblePictureBox(pictureBox_NodeImage);
			new FlexibleControl(label3);
			//UiPKG.SetPictureBoxFitImage(pictureBox1);
			new FlexiblePanel(panel_Node, pictureBox1);
			new AutoSizeTextBox(textBox1);
			new AutoSizeTextBox(textBox2);
		}


		private TableLayoutPanel tableLayoutPanel_NodeBorder;
		private TableLayoutPanel tableLayoutPanel_NodeImagePlace;
		private TableLayoutPanel tableLayoutPanel_NodeTagPlace;
		private PictureBox pictureBox_NodeImage;
		private FlowLayoutPanel flowLayoutPanel_NodeTags;
		private Label label_NodeTag;
		private Label label_NodeTag2;
		private PictureBox pictureBox1;
		private Label label3;
		public TextBox textBox_NodeContent;
		private Panel panel_Node;
		private TextBox textBox1;
		private TextBox textBox2;
		private Button button1;
	}
}