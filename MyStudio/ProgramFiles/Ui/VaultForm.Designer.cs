using Module;
using PKG;
using System.Diagnostics;

namespace Ui
{
	partial class VaultForm
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
			panel_canvas = new Panel();
			panel_NodeGroup = new Panel();
			panel_NodeInnerBody = new Panel();
			pictureBox_NodeImage = new PictureBox();
			panel_NodeContentAndTag = new Panel();
			textBox_NodeContent = new TextBox();
			flowLayoutPanel_NodeTag = new FlowLayoutPanel();
			button_NodeTag = new Button();
			button2 = new Button();
			button1 = new Button();
			button_createNode = new Button();
			label_NodeContent = new Label();
			pictureBox1 = new PictureBox();
			pictureBox2 = new PictureBox();
			panel_NodeGroup.SuspendLayout();
			panel_NodeInnerBody.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).BeginInit();
			panel_NodeContentAndTag.SuspendLayout();
			flowLayoutPanel_NodeTag.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			SuspendLayout();
			// 
			// panel_canvas
			// 
			panel_canvas.AutoScroll = true;
			panel_canvas.AutoScrollMinSize = new Size(500, 500);
			panel_canvas.BorderStyle = BorderStyle.FixedSingle;
			panel_canvas.Location = new Point(489, 91);
			panel_canvas.Margin = new Padding(4);
			panel_canvas.Name = "panel_canvas";
			panel_canvas.Padding = new Padding(12, 13, 12, 13);
			panel_canvas.Size = new Size(474, 315);
			panel_canvas.TabIndex = 0;
			// 
			// panel_NodeGroup
			// 
			panel_NodeGroup.BorderStyle = BorderStyle.FixedSingle;
			panel_NodeGroup.Controls.Add(panel_NodeInnerBody);
			panel_NodeGroup.Location = new Point(34, 15);
			panel_NodeGroup.Margin = new Padding(4);
			panel_NodeGroup.Name = "panel_NodeGroup";
			panel_NodeGroup.Padding = new Padding(12, 13, 12, 13);
			panel_NodeGroup.Size = new Size(347, 259);
			panel_NodeGroup.TabIndex = 2;
			// 
			// panel_NodeInnerBody
			// 
			panel_NodeInnerBody.BorderStyle = BorderStyle.FixedSingle;
			panel_NodeInnerBody.Controls.Add(pictureBox_NodeImage);
			panel_NodeInnerBody.Controls.Add(panel_NodeContentAndTag);
			panel_NodeInnerBody.Location = new Point(12, 13);
			panel_NodeInnerBody.Margin = new Padding(4);
			panel_NodeInnerBody.Name = "panel_NodeInnerBody";
			panel_NodeInnerBody.Size = new Size(318, 232);
			panel_NodeInnerBody.TabIndex = 0;
			panel_NodeInnerBody.ControlAdded += panel_NodeInnerBody_ControlAdded;
			// 
			// pictureBox_NodeImage
			// 
			pictureBox_NodeImage.BorderStyle = BorderStyle.FixedSingle;
			pictureBox_NodeImage.Image = MyStudio.Properties.Resources.kazimierz_RB_Carve;
			pictureBox_NodeImage.Location = new Point(-1, 11);
			pictureBox_NodeImage.Margin = new Padding(4);
			pictureBox_NodeImage.Name = "pictureBox_NodeImage";
			pictureBox_NodeImage.Size = new Size(273, 63);
			pictureBox_NodeImage.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox_NodeImage.TabIndex = 0;
			pictureBox_NodeImage.TabStop = false;
			// 
			// panel_NodeContentAndTag
			// 
			panel_NodeContentAndTag.BorderStyle = BorderStyle.FixedSingle;
			panel_NodeContentAndTag.Controls.Add(textBox_NodeContent);
			panel_NodeContentAndTag.Controls.Add(flowLayoutPanel_NodeTag);
			panel_NodeContentAndTag.Location = new Point(4, 82);
			panel_NodeContentAndTag.Margin = new Padding(4);
			panel_NodeContentAndTag.Name = "panel_NodeContentAndTag";
			panel_NodeContentAndTag.Size = new Size(309, 117);
			panel_NodeContentAndTag.TabIndex = 1;
			// 
			// textBox_NodeContent
			// 
			textBox_NodeContent.BorderStyle = BorderStyle.FixedSingle;
			textBox_NodeContent.Location = new Point(51, 4);
			textBox_NodeContent.Margin = new Padding(4);
			textBox_NodeContent.Multiline = true;
			textBox_NodeContent.Name = "textBox_NodeContent";
			textBox_NodeContent.Size = new Size(152, 43);
			textBox_NodeContent.TabIndex = 0;
			// 
			// flowLayoutPanel_NodeTag
			// 
			flowLayoutPanel_NodeTag.AutoSize = true;
			flowLayoutPanel_NodeTag.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			flowLayoutPanel_NodeTag.BorderStyle = BorderStyle.FixedSingle;
			flowLayoutPanel_NodeTag.Controls.Add(button_NodeTag);
			flowLayoutPanel_NodeTag.Controls.Add(button2);
			flowLayoutPanel_NodeTag.Controls.Add(button1);
			flowLayoutPanel_NodeTag.FlowDirection = FlowDirection.RightToLeft;
			flowLayoutPanel_NodeTag.Location = new Point(4, 54);
			flowLayoutPanel_NodeTag.Margin = new Padding(4);
			flowLayoutPanel_NodeTag.Name = "flowLayoutPanel_NodeTag";
			flowLayoutPanel_NodeTag.Size = new Size(316, 47);
			flowLayoutPanel_NodeTag.TabIndex = 1;
			// 
			// button_NodeTag
			// 
			button_NodeTag.Location = new Point(250, 4);
			button_NodeTag.Margin = new Padding(4);
			button_NodeTag.Name = "button_NodeTag";
			button_NodeTag.Size = new Size(60, 37);
			button_NodeTag.TabIndex = 0;
			button_NodeTag.Text = "tag";
			button_NodeTag.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			button2.Location = new Point(127, 4);
			button2.Margin = new Padding(4);
			button2.Name = "button2";
			button2.Size = new Size(115, 37);
			button2.TabIndex = 2;
			button2.Text = "button2";
			button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			button1.Location = new Point(4, 4);
			button1.Margin = new Padding(4);
			button1.Name = "button1";
			button1.Size = new Size(115, 37);
			button1.TabIndex = 1;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			// 
			// button_createNode
			// 
			button_createNode.Location = new Point(703, 453);
			button_createNode.Margin = new Padding(4);
			button_createNode.Name = "button_createNode";
			button_createNode.Size = new Size(194, 37);
			button_createNode.TabIndex = 1;
			button_createNode.Text = "create node";
			button_createNode.UseVisualStyleBackColor = true;
			button_createNode.Click += button_createNode_Click;
			// 
			// label_NodeContent
			// 
			label_NodeContent.AutoSize = true;
			label_NodeContent.Location = new Point(532, 453);
			label_NodeContent.Margin = new Padding(4, 0, 4, 0);
			label_NodeContent.Name = "label_NodeContent";
			label_NodeContent.Size = new Size(64, 24);
			label_NodeContent.TabIndex = 3;
			label_NodeContent.Text = "label1";
			label_NodeContent.MouseLeave += label_NodeContent_MouseLeave;
			// 
			// pictureBox1
			// 
			pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			pictureBox1.Image = MyStudio.Properties.Resources.kazimierz_black;
			pictureBox1.Location = new Point(369, 329);
			pictureBox1.Margin = new Padding(4);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(90, 78);
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 4;
			pictureBox1.TabStop = false;
			pictureBox1.MouseDoubleClick += pictureBox1_MouseDoubleClick;
			// 
			// pictureBox2
			// 
			pictureBox2.BorderStyle = BorderStyle.FixedSingle;
			pictureBox2.Image = MyStudio.Properties.Resources.kazimierz_silver;
			pictureBox2.Location = new Point(48, 356);
			pictureBox2.Margin = new Padding(4);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new Size(251, 78);
			pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox2.TabIndex = 5;
			pictureBox2.TabStop = false;
			pictureBox2.Click += pictureBox2_Click;
			// 
			// VaultForm
			// 
			AutoScaleDimensions = new SizeF(11F, 24F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(978, 569);
			Controls.Add(pictureBox2);
			Controls.Add(pictureBox1);
			Controls.Add(label_NodeContent);
			Controls.Add(panel_NodeGroup);
			Controls.Add(button_createNode);
			Controls.Add(panel_canvas);
			Margin = new Padding(4);
			Name = "VaultForm";
			Text = "VaultUi";
			panel_NodeGroup.ResumeLayout(false);
			panel_NodeInnerBody.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).EndInit();
			panel_NodeContentAndTag.ResumeLayout(false);
			panel_NodeContentAndTag.PerformLayout();
			flowLayoutPanel_NodeTag.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		#region My code
		public void MyInit()
		{
			//TestNodeUi();
			AppManager.workingVault.canvas = panel_canvas;
			AppManager.workingVault.rootForm = this;
			ExControl exControl_Canvas = new ExControl(panel_canvas, this);
			ExPanel exPanel_Canvas = new ExPanel(panel_canvas);
			TestNodeUi();
			new ExPictureBox(pictureBox1, this) { EnableDrag = true, EnableResize = true, EnableRatioFixed = false };
			new ExPictureBox(pictureBox2, this) { EnableDrag = true, EnableResize = true, EnableRatioFixed = false };
		}
		public void TestNodeUi()
		{
			int GroupPadding = 10;

			ExControl exPanel_NodeGroup;
			ExControl exPanel_NodeInnerBody;
			ExControl exLabel_NodeContent;
			ExTextBox exTextBox_NodeContent;
			ExPictureBox exPictureBox_NodeImage;
			ExControl exFlowLayoutPanel_NodeTags;
			ExControl exPanel_NodeContentAndTag;


			#region Control Extension initialization
			exPanel_NodeGroup = new ExControl(panel_NodeGroup, this) { EnablePaintBorder = true, EnableDrag = true };
			exPanel_NodeInnerBody = new ExControl(panel_NodeInnerBody, this) { GroupRoot = exPanel_NodeGroup };
			exFlowLayoutPanel_NodeTags = new ExControl(flowLayoutPanel_NodeTag, this) { GroupRoot = exPanel_NodeGroup };
			exPanel_NodeContentAndTag = new ExControl(panel_NodeContentAndTag, this) { GroupRoot = exPanel_NodeGroup };
			exPictureBox_NodeImage = new ExPictureBox(pictureBox_NodeImage, this) { EnablePaintBorder = false, GroupRoot = exPanel_NodeGroup, EnableResize = true, EnableRatioFixed = true };
			exTextBox_NodeContent = new ExTextBox(textBox_NodeContent, this) { GroupRoot = exPanel_NodeGroup };
			exLabel_NodeContent = new ExControl(label_NodeContent, this) { GroupRoot = exPanel_NodeGroup };

			exPanel_NodeGroup.Watch_FunctionalityEnable();

			AlignControlsInNode();

			pictureBox_NodeImage.SizeChanged += AlignControlsInNode_handler;
			textBox_NodeContent.SizeChanged += AlignControlsInNode_handler;
			label_NodeContent.SizeChanged += AlignControlsInNode_handler;
			flowLayoutPanel_NodeTag.SizeChanged += AlignControlsInNode_handler;
			///panel_NodeInnerBody.ControlAdded += AlignControlsInNode_handler;
			exPictureBox_NodeImage.ActionAfterFlex += AlignControlsInNode;
			#endregion
		}
		public void AlignControlsInNode()
		{
			int GroupPadding = 10;
			Debug.WriteLine("Alignment");
			// the order is important
			ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
			ControlAligner.AlignControlsVertically(panel_NodeInnerBody);
			ControlAligner.AlignControlsHorizontally(panel_NodeGroup, GroupPadding);
		}
		public void AlignControlsInNode_handler(object sender, EventArgs e)
		{
			AlignControlsInNode();
		}
		public void AlignControlsInNode_handler(object sender, ControlEventArgs e)
		{
			AlignControlsInNode();
		}
		#endregion


		private Panel panel_canvas;
		private Button button_createNode;
		private Panel panel_NodeGroup;
		private Panel panel_NodeInnerBody;
		private PictureBox pictureBox_NodeImage;
		private Panel panel_NodeContentAndTag;
		private FlowLayoutPanel flowLayoutPanel_NodeTag;
		private Button button_NodeTag;
		private TextBox textBox_NodeContent;
		private Button button2;
		private Button button1;
		private Label label_NodeContent;
		private PictureBox pictureBox1;
		private PictureBox pictureBox2;
	}
}