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
			button_createNode = new Button();
			pictureBox1 = new PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// panel_canvas
			// 
			panel_canvas.AutoScroll = true;
			panel_canvas.AutoScrollMinSize = new Size(500, 500);
			panel_canvas.BorderStyle = BorderStyle.FixedSingle;
			panel_canvas.Location = new Point(225, 116);
			panel_canvas.Name = "panel_canvas";
			panel_canvas.Padding = new Padding(10);
			panel_canvas.Size = new Size(388, 250);
			panel_canvas.TabIndex = 0;
			// 
			// button_createNode
			// 
			button_createNode.Location = new Point(361, 369);
			button_createNode.Name = "button_createNode";
			button_createNode.Size = new Size(159, 29);
			button_createNode.TabIndex = 1;
			button_createNode.Text = "create node";
			button_createNode.UseVisualStyleBackColor = true;
			button_createNode.Click += button_createNode_Click;
			// 
			// pictureBox1
			// 
			pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			pictureBox1.Image = MyStudio.Properties.Resources.kazimierz_black;
			pictureBox1.Location = new Point(64, 139);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(125, 62);
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 2;
			pictureBox1.TabStop = false;
			pictureBox1.MouseEnter += pictureBox1_MouseEnter;
			// 
			// VaultForm
			// 
			AutoScaleDimensions = new SizeF(9F, 19F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(pictureBox1);
			Controls.Add(button_createNode);
			Controls.Add(panel_canvas);
			Name = "VaultForm";
			Text = "VaultUi";
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		#region My code
		public void MyInit()
		{
			//TestNodeUi();
			AppManager.workingVault.canvas = panel_canvas;
			new DraggableCanvas(panel_canvas);

		}
		public void TestNodeUi()
		{

		}
		#endregion


		private Panel panel_canvas;
		private Button button_createNode;
		private PictureBox pictureBox1;
	}
}