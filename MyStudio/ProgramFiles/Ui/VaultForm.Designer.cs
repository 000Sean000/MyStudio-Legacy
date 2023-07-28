using Module;
using PKG;

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
			panel_NodeGroup.SuspendLayout();
			panel_NodeInnerBody.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).BeginInit();
			panel_NodeContentAndTag.SuspendLayout();
			flowLayoutPanel_NodeTag.SuspendLayout();
			SuspendLayout();
			// 
			// panel_canvas
			// 
			panel_canvas.AutoScroll = true;
			panel_canvas.AutoScrollMinSize = new Size(500, 500);
			panel_canvas.BorderStyle = BorderStyle.FixedSingle;
			panel_canvas.Location = new Point(400, 72);
			panel_canvas.Name = "panel_canvas";
			panel_canvas.Padding = new Padding(10);
			panel_canvas.Size = new Size(388, 250);
			panel_canvas.TabIndex = 0;
			// 
			// panel_NodeGroup
			// 
			panel_NodeGroup.BorderStyle = BorderStyle.FixedSingle;
			panel_NodeGroup.Controls.Add(panel_NodeInnerBody);
			panel_NodeGroup.Location = new Point(57, 127);
			panel_NodeGroup.Name = "panel_NodeGroup";
			panel_NodeGroup.Padding = new Padding(10);
			panel_NodeGroup.Size = new Size(250, 125);
			panel_NodeGroup.TabIndex = 2;
			// 
			// panel_NodeInnerBody
			// 
			panel_NodeInnerBody.BorderStyle = BorderStyle.FixedSingle;
			panel_NodeInnerBody.Controls.Add(pictureBox_NodeImage);
			panel_NodeInnerBody.Controls.Add(panel_NodeContentAndTag);
			panel_NodeInnerBody.Location = new Point(10, 10);
			panel_NodeInnerBody.Name = "panel_NodeInnerBody";
			panel_NodeInnerBody.Size = new Size(228, 103);
			panel_NodeInnerBody.TabIndex = 0;
			// 
			// pictureBox_NodeImage
			// 
			pictureBox_NodeImage.BorderStyle = BorderStyle.FixedSingle;
			pictureBox_NodeImage.Image = MyStudio.Properties.Resources.kazimierz_black;
			pictureBox_NodeImage.Location = new Point(168, 0);
			pictureBox_NodeImage.Name = "pictureBox_NodeImage";
			pictureBox_NodeImage.Size = new Size(58, 101);
			pictureBox_NodeImage.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox_NodeImage.TabIndex = 0;
			pictureBox_NodeImage.TabStop = false;
			// 
			// panel_NodeContentAndTag
			// 
			panel_NodeContentAndTag.BorderStyle = BorderStyle.FixedSingle;
			panel_NodeContentAndTag.Controls.Add(textBox_NodeContent);
			panel_NodeContentAndTag.Controls.Add(flowLayoutPanel_NodeTag);
			panel_NodeContentAndTag.Location = new Point(0, 0);
			panel_NodeContentAndTag.Name = "panel_NodeContentAndTag";
			panel_NodeContentAndTag.Size = new Size(168, 101);
			panel_NodeContentAndTag.TabIndex = 1;
			// 
			// textBox_NodeContent
			// 
			textBox_NodeContent.BorderStyle = BorderStyle.FixedSingle;
			textBox_NodeContent.Location = new Point(45, 14);
			textBox_NodeContent.Multiline = true;
			textBox_NodeContent.Name = "textBox_NodeContent";
			textBox_NodeContent.Size = new Size(125, 34);
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
			flowLayoutPanel_NodeTag.Location = new Point(0, 62);
			flowLayoutPanel_NodeTag.Name = "flowLayoutPanel_NodeTag";
			flowLayoutPanel_NodeTag.Size = new Size(257, 37);
			flowLayoutPanel_NodeTag.TabIndex = 1;
			// 
			// button_NodeTag
			// 
			button_NodeTag.Location = new Point(203, 3);
			button_NodeTag.Name = "button_NodeTag";
			button_NodeTag.Size = new Size(49, 29);
			button_NodeTag.TabIndex = 0;
			button_NodeTag.Text = "tag";
			button_NodeTag.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			button2.Location = new Point(103, 3);
			button2.Name = "button2";
			button2.Size = new Size(94, 29);
			button2.TabIndex = 2;
			button2.Text = "button2";
			button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			button1.Location = new Point(3, 3);
			button1.Name = "button1";
			button1.Size = new Size(94, 29);
			button1.TabIndex = 1;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			// 
			// button_createNode
			// 
			button_createNode.Location = new Point(575, 359);
			button_createNode.Name = "button_createNode";
			button_createNode.Size = new Size(159, 29);
			button_createNode.TabIndex = 1;
			button_createNode.Text = "create node";
			button_createNode.UseVisualStyleBackColor = true;
			button_createNode.Click += button_createNode_Click;
			// 
			// label_NodeContent
			// 
			label_NodeContent.AutoSize = true;
			label_NodeContent.Location = new Point(96, 331);
			label_NodeContent.Name = "label_NodeContent";
			label_NodeContent.Size = new Size(51, 19);
			label_NodeContent.TabIndex = 3;
			label_NodeContent.Text = "label1";
			// 
			// VaultForm
			// 
			AutoScaleDimensions = new SizeF(9F, 19F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(label_NodeContent);
			Controls.Add(panel_NodeGroup);
			Controls.Add(button_createNode);
			Controls.Add(panel_canvas);
			Name = "VaultForm";
			Text = "VaultUi";
			panel_NodeGroup.ResumeLayout(false);
			panel_NodeInnerBody.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).EndInit();
			panel_NodeContentAndTag.ResumeLayout(false);
			panel_NodeContentAndTag.PerformLayout();
			flowLayoutPanel_NodeTag.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		#region My code
		public void MyInit()
		{
			//TestNodeUi();
			AppManager.workingVault.canvas = panel_canvas;
			ExControl exControl_Canvas = new ExControl(panel_canvas);
			ExPanel exPanel_Canvas = new ExPanel(panel_canvas);
			TestNodeUi();

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
			exPanel_NodeGroup = new ExControl(panel_NodeGroup) { EnablePaintBorder = true, EnableDrag = true };
			exPanel_NodeInnerBody = new ExControl(panel_NodeInnerBody) { GroupRoot = exPanel_NodeGroup };
			exFlowLayoutPanel_NodeTags = new ExControl(flowLayoutPanel_NodeTag) { GroupRoot = exPanel_NodeGroup };
			exPanel_NodeContentAndTag = new ExControl(panel_NodeContentAndTag) { GroupRoot = exPanel_NodeGroup };
			exPictureBox_NodeImage = new ExPictureBox(pictureBox_NodeImage) { EnablePaintBorder = false, GroupRoot = exPanel_NodeGroup, EnableResize = true, EnableRatioFixed = true };
			exTextBox_NodeContent = new ExTextBox(textBox_NodeContent) { GroupRoot = exPanel_NodeGroup };
			exLabel_NodeContent = new ExControl(label_NodeContent) { GroupRoot = exPanel_NodeGroup };

			exPanel_NodeGroup.CheckFunctionalityEnable();

			ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
			ControlAligner.AlignControlsHorizontally(panel_NodeInnerBody);
			ControlAligner.AlignControlsHorizontally(panel_NodeGroup, GroupPadding);

			pictureBox_NodeImage.SizeChanged += AlignControlsInNode;
			textBox_NodeContent.SizeChanged += AlignControlsInNode;
			flowLayoutPanel_NodeTag.SizeChanged += AlignControlsInNode;
			flowLayoutPanel_NodeTag.SizeChanged += AlignControlsInNode;
			#endregion
		}
		public void AlignControlsInNode(object sender, EventArgs e)
		{
			int GroupPadding = 10;
			// the order is important
			ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
			ControlAligner.AlignControlsHorizontally(panel_NodeInnerBody);
			ControlAligner.AlignControlsHorizontally(panel_NodeGroup, GroupPadding);
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
	}
}