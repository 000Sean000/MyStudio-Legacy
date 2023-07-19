using Module;
using PKG;

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
			panel_canvas = new Panel();
			SuspendLayout();
			// 
			// panel_canvas
			// 
			panel_canvas.BorderStyle = BorderStyle.FixedSingle;
			panel_canvas.Location = new Point(225, 116);
			panel_canvas.Name = "panel_canvas";
			panel_canvas.Size = new Size(388, 225);
			panel_canvas.TabIndex = 0;
			// 
			// VaultUi
			// 
			AutoScaleDimensions = new SizeF(9F, 19F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(panel_canvas);
			Name = "VaultUi";
			Text = "VaultUi";
			ResumeLayout(false);
		}

		#endregion

		#region My code
		public void MyInit()
		{
			//TestNodeUi();
			//AppManager.workingVault.

		}
		public void TestNodeUi()
		{
			new DraggableCanvas(panel_canvas);
			Node.canvas = panel_canvas;
			Node.CreateNodeUi();
		}
		#endregion


		private Panel panel_canvas;
	}
}