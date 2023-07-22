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
		#region Field
		protected List<UiNode> _uiNodes = new List<UiNode>();
		#endregion

		#region Resource Locks
		protected object _uiNodesLock = new object();
		#endregion

		#region VaultForm Config
		public Panel canvas;
		#endregion

		#region Node Ui Operation
		public class UiNode
		{
			protected Vault _vault; // outer class
			protected Panel _canvas;
			protected Node _node;

			#region Ui components
			Panel panel_Node;
			TextBox textBox_NodeContent;
			PictureBox pictureBox_NodeImage;
			FlowLayoutPanel flowLayoutPanel_NodeTags;
			Panel panel_NodeContentAndTag;

			ExControl exPanel_Node;
			ExTextBox exTextBox_NodeContent;
			ExPictureBox exPictureBox_NodeImage;
			ExControl exFlowLayoutPanel_NodeTags;
			ExControl exPanel_NodeContentAndTag;
			#endregion
			public UiNode(Vault vault)
			{
				_vault = vault;
				_canvas = _vault.canvas;


			}
			public void Show(string id)
			{
				_node = _vault.FetchNode(id);
				//node.Get<List<string>>(Node.PROPERTY, Node.TAG);

				//...
				#region Basic control initialization
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
				#endregion

				#region Control Extension initialization
				exPanel_Node = new ExControl(panel_Node) { EnableGroup = true, IsGroupRoot = true, EnableFlex = true, enableDrag = true, enableResize = false };
				exFlowLayoutPanel_NodeTags = new ExControl(flowLayoutPanel_NodeTags) { EnableGroup = true, groupRoot = panel_Node, EnableFlex = false, enableDrag = false, enableResize = false };
				exPanel_NodeContentAndTag = new ExControl(panel_NodeContentAndTag) { EnableGroup = true, groupRoot = panel_Node, EnableFlex = false, enableDrag = false, enableResize = false };
				exPictureBox_NodeImage = new ExPictureBox(pictureBox_NodeImage) { EnableGroup = true, groupRoot = panel_Node, EnableFlex = true, enableDrag = false, enableResize = true};
				exTextBox_NodeContent = new ExTextBox(textBox_NodeContent) { EnableGroup = true, groupRoot = panel_Node, EnableFlex = false, enableDrag = false, enableResize = false };


				ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
				ControlAligner.AlignControlsHorizontally(panel_Node);

				pictureBox_NodeImage.SizeChanged += AlignControlsInNode;
				textBox_NodeContent.SizeChanged += AlignControlsInNode;
				flowLayoutPanel_NodeTags.SizeChanged += AlignControlsInNode;
				#endregion

				_canvas.Controls.Add(panel_Node);
			}
			public void AlignControlsInNode(object sender,  EventArgs e)
			{
				ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
				ControlAligner.AlignControlsHorizontally(panel_Node);
			}
		}
		public void CreateUiNode()
		{
			Node node = CreateNode();
			string id = node.id;
			UiNode uiNode = new UiNode(this);
			lock (_uiNodesLock)
			{
				_uiNodes.Add(uiNode);
			}
			uiNode.Show(id);
		}
		public void DeleteUiNode(string id)
		{

		}
		
		#endregion




	}
}
