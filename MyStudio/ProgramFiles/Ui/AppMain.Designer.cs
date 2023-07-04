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
            flowLayoutPanel_NodeTags = new FlowLayoutPanel();
            label_NodeTag = new Label();
            label_NodeTag2 = new Label();
            pictureBox_NodeImage = new PictureBox();
            label1 = new Label();
            tableLayoutPanel_NodeBorder.SuspendLayout();
            tableLayoutPanel_NodeImagePlace.SuspendLayout();
            tableLayoutPanel_NodeTagPlace.SuspendLayout();
            flowLayoutPanel_NodeTags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).BeginInit();
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
            tableLayoutPanel_NodeImagePlace.ColumnCount = 2;
            tableLayoutPanel_NodeImagePlace.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel_NodeImagePlace.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel_NodeImagePlace.Controls.Add(tableLayoutPanel_NodeTagPlace, 1, 0);
            tableLayoutPanel_NodeImagePlace.Controls.Add(pictureBox_NodeImage, 0, 0);
            tableLayoutPanel_NodeImagePlace.Dock = DockStyle.Fill;
            tableLayoutPanel_NodeImagePlace.Location = new Point(33, 33);
            tableLayoutPanel_NodeImagePlace.Name = "tableLayoutPanel_NodeImagePlace";
            tableLayoutPanel_NodeImagePlace.RowCount = 2;
            tableLayoutPanel_NodeImagePlace.RowStyles.Add(new RowStyle());
            tableLayoutPanel_NodeImagePlace.RowStyles.Add(new RowStyle());
            tableLayoutPanel_NodeImagePlace.Size = new Size(360, 218);
            tableLayoutPanel_NodeImagePlace.TabIndex = 0;
            // 
            // tableLayoutPanel_NodeTagPlace
            // 
            tableLayoutPanel_NodeTagPlace.ColumnCount = 2;
            tableLayoutPanel_NodeTagPlace.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel_NodeTagPlace.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel_NodeTagPlace.Controls.Add(textBox_NodeContent, 0, 0);
            tableLayoutPanel_NodeTagPlace.Controls.Add(flowLayoutPanel_NodeTags, 0, 1);
            tableLayoutPanel_NodeTagPlace.Dock = DockStyle.Fill;
            tableLayoutPanel_NodeTagPlace.Location = new Point(109, 3);
            tableLayoutPanel_NodeTagPlace.Name = "tableLayoutPanel_NodeTagPlace";
            tableLayoutPanel_NodeTagPlace.RowCount = 2;
            tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle());
            tableLayoutPanel_NodeTagPlace.RowStyles.Add(new RowStyle());
            tableLayoutPanel_NodeTagPlace.Size = new Size(248, 100);
            tableLayoutPanel_NodeTagPlace.TabIndex = 0;
            // 
            // textBox_NodeContent
            // 
            textBox_NodeContent.BorderStyle = BorderStyle.None;
            textBox_NodeContent.Dock = DockStyle.Fill;
            textBox_NodeContent.Location = new Point(3, 3);
            textBox_NodeContent.Multiline = true;
            textBox_NodeContent.Name = "textBox_NodeContent";
            textBox_NodeContent.Size = new Size(175, 50);
            textBox_NodeContent.TabIndex = 0;
            textBox_NodeContent.Text = "fsd\r\nsds";
            // 
            // flowLayoutPanel_NodeTags
            // 
            flowLayoutPanel_NodeTags.Controls.Add(label_NodeTag);
            flowLayoutPanel_NodeTags.Controls.Add(label_NodeTag2);
            flowLayoutPanel_NodeTags.Dock = DockStyle.Fill;
            flowLayoutPanel_NodeTags.Location = new Point(3, 59);
            flowLayoutPanel_NodeTags.Name = "flowLayoutPanel_NodeTags";
            flowLayoutPanel_NodeTags.Size = new Size(175, 38);
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
            // pictureBox_NodeImage
            // 
            pictureBox_NodeImage.Dock = DockStyle.Fill;
            pictureBox_NodeImage.Image = MyStudio.Properties.Resources.kazimierz_RB_Carve;
            pictureBox_NodeImage.Location = new Point(3, 3);
            pictureBox_NodeImage.Name = "pictureBox_NodeImage";
            pictureBox_NodeImage.Size = new Size(100, 100);
            pictureBox_NodeImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_NodeImage.TabIndex = 1;
            pictureBox_NodeImage.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Location = new Point(710, 77);
            label1.Name = "label1";
            label1.Size = new Size(51, 19);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // AppMain
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(tableLayoutPanel_NodeBorder);
            Name = "AppMain";
            Text = "AppMain";
            tableLayoutPanel_NodeBorder.ResumeLayout(false);
            tableLayoutPanel_NodeImagePlace.ResumeLayout(false);
            tableLayoutPanel_NodeTagPlace.ResumeLayout(false);
            tableLayoutPanel_NodeTagPlace.PerformLayout();
            flowLayoutPanel_NodeTags.ResumeLayout(false);
            flowLayoutPanel_NodeTags.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel_NodeBorder;
        private TableLayoutPanel tableLayoutPanel_NodeImagePlace;
        private TableLayoutPanel tableLayoutPanel_NodeTagPlace;
        private PictureBox pictureBox_NodeImage;
        public TextBox textBox_NodeContent;
        private FlowLayoutPanel flowLayoutPanel_NodeTags;
        private Label label_NodeTag;
        private Label label_NodeTag2;
        private Label label1;
    }
}