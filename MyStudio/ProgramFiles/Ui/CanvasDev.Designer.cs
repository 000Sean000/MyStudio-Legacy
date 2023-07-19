using PKG;
using Module;

namespace MyStudio.ProgramFiles.Ui
{
	partial class CanvasDev
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
			panel_Canvas = new Panel();
			SuspendLayout();
			// 
			// panel_Canvas
			// 
			panel_Canvas.BorderStyle = BorderStyle.FixedSingle;
			panel_Canvas.Location = new Point(135, 75);
			panel_Canvas.Name = "panel_Canvas";
			panel_Canvas.Size = new Size(566, 324);
			panel_Canvas.TabIndex = 0;
			// 
			// CanvasDev
			// 
			AutoScaleDimensions = new SizeF(9F, 19F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(panel_Canvas);
			Name = "CanvasDev";
			Text = "CanvasDev";
			ResumeLayout(false);
		}

		#endregion
		public void MyInit()
		{
			new DraggableCanvas(panel_Canvas);
			Node.canvas = panel_Canvas;
			//Node.CreateNodeUi();
		}

		private Panel panel_Canvas;
	}
}