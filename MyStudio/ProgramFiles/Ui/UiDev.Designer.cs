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
            tableLayoutPanel1 = new TableLayoutPanel();
            label_ex = new Label();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Cursor = Cursors.SizeNESW;
            tableLayoutPanel1.Location = new Point(366, 175);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(250, 125);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label_ex
            // 
            label_ex.BackColor = SystemColors.ActiveCaption;
            label_ex.Cursor = Cursors.SizeAll;
            label_ex.Location = new Point(128, 85);
            label_ex.Name = "label_ex";
            label_ex.Size = new Size(159, 98);
            label_ex.TabIndex = 1;
            label_ex.Text = "label1";
            label_ex.Click += label1_Click;
            label_ex.MouseDown += label1_MouseDown;
            label_ex.MouseLeave += label_ex_MouseLeave;
            label_ex.MouseHover += label1_MouseHover;
            label_ex.MouseMove += label_ex_MouseMove;
            // 
            // UiDev
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label_ex);
            Controls.Add(tableLayoutPanel1);
            Name = "UiDev";
            Text = "UiDev";
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label_ex;

        ResizableControl re;
        private bool isHovering = false;
        public void MyInit()
        {
            re = new ResizableControl(label_ex);
        }
    }
}