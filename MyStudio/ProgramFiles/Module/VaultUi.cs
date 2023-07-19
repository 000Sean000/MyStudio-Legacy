using Newtonsoft.Json.Linq;
using PKG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module
{
	partial class Vault
	{
		#region VaultForm Config
		public Panel canvas;
		#endregion

		#region Node Ui Opation
		public void UiCreateNode()
		{
			Node node = CreateNode();
			UiShowNode(node.id);
		}
		public void UiShowNode(string id)
		{
			Node node = FetchNode(id);
			//node.Get<List<string>>(Node.PROPERTY, Node.TAG);

			//...


			Panel panel_Node;
			TextBox textBox_NodeContent;
			PictureBox pictureBox_NodeImage;
			FlowLayoutPanel flowLayoutPanel_NodeTags;
			Panel panel_NodeContentAndTag;
			panel_Node = new Panel();
			textBox_NodeContent = new TextBox();
			pictureBox_NodeImage = new PictureBox();
			panel_NodeContentAndTag = new Panel();
			flowLayoutPanel_NodeTags = new FlowLayoutPanel();
			// 
			// panel_Node
			// 
			panel_Node.BorderStyle = BorderStyle.FixedSingle;
			panel_Node.Controls.Add(pictureBox_NodeImage);
			panel_Node.Controls.Add(panel_NodeContentAndTag);
			panel_Node.Location = new Point(0, 0);
			panel_Node.Name = "panel_Node";
			panel_Node.Size = new Size(390, 244);
			panel_Node.TabIndex = 6;
			// 
			// textBox_NodeContent
			// 
			textBox_NodeContent.BorderStyle = BorderStyle.None;
			textBox_NodeContent.Location = new Point(45, 14);
			textBox_NodeContent.Multiline = true;
			textBox_NodeContent.Name = "textBox_NodeContent";
			textBox_NodeContent.Size = new Size(125, 34);
			textBox_NodeContent.TabIndex = 1;
			// 
			// pictureBox_NodeImage
			// 
			pictureBox_NodeImage.BorderStyle = BorderStyle.FixedSingle;
			pictureBox_NodeImage.Image = MyStudio.Properties.Resources.kazimierz_black;
			pictureBox_NodeImage.Location = new Point(87, 3);
			pictureBox_NodeImage.Name = "pictureBox_NodeImage";
			pictureBox_NodeImage.Size = new Size(56, 55);
			pictureBox_NodeImage.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox_NodeImage.TabIndex = 0;
			pictureBox_NodeImage.TabStop = false;
			// 
			// panel_NodeContentAndTag
			// 
			panel_NodeContentAndTag.BorderStyle = BorderStyle.FixedSingle;
			panel_NodeContentAndTag.Controls.Add(flowLayoutPanel_NodeTags);
			panel_NodeContentAndTag.Controls.Add(textBox_NodeContent);
			panel_NodeContentAndTag.Location = new Point(112, 83);
			panel_NodeContentAndTag.Name = "panel_NodeContentAndTag";
			panel_NodeContentAndTag.Size = new Size(250, 125);
			panel_NodeContentAndTag.TabIndex = 9;
			// 
			// flowLayoutPanel_NodeTags
			// 
			flowLayoutPanel_NodeTags.AutoSize = true;
			flowLayoutPanel_NodeTags.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			flowLayoutPanel_NodeTags.BorderStyle = BorderStyle.FixedSingle;
			//flowLayoutPanel_NodeTags.Controls.Add(button3);
			//flowLayoutPanel_NodeTags.Controls.Add(button4);
			//flowLayoutPanel_NodeTags.Controls.Add(button5);
			flowLayoutPanel_NodeTags.FlowDirection = FlowDirection.RightToLeft;
			flowLayoutPanel_NodeTags.Location = new Point(23, 72);
			flowLayoutPanel_NodeTags.Name = "flowLayoutPanel_NodeTags";
			flowLayoutPanel_NodeTags.Size = new Size(302, 37);
			flowLayoutPanel_NodeTags.TabIndex = 2;
			FlexiblePictureBox nodeImage = new FlexiblePictureBox(pictureBox_NodeImage);
			nodeImage.enableDrag = false;
			AutoSizeTextBox nodeText = new AutoSizeTextBox(textBox_NodeContent);
			ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
			ControlAligner.AlignControlsHorizontally(panel_Node);
			canvas.Controls.Add(panel_Node);

			pictureBox_NodeImage.SizeChanged += (object sender, EventArgs e) =>
			{
				ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
				ControlAligner.AlignControlsHorizontally(panel_Node);
			};
			textBox_NodeContent.SizeChanged += (object sender, EventArgs e) =>
			{
				ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
				ControlAligner.AlignControlsHorizontally(panel_Node);
			};
			flowLayoutPanel_NodeTags.SizeChanged += (object sender, EventArgs e) =>
			{
				ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
				ControlAligner.AlignControlsHorizontally(panel_Node);
			};
			
			
		}
		private void ImageRegion_SizeChanged(object sender, EventArgs e)
		{

		}
		private void TextRegion_SizeChanged(object sender, EventArgs e)
		{

		}
		private void TagRegion_SizeChanged(object sender, EventArgs e)
		{

		}
		#endregion




	}
}
