using Newtonsoft.Json.Linq;
using PKG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKG;
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
			Panel panel_NodeGroup;
			Panel panel_NodeInnerBody;
			Label label_NodeContent;
			TextBox textBox_NodeContent;
			PictureBox pictureBox_NodeImage;
			FlowLayoutPanel flowLayoutPanel_NodeTags;
			Panel panel_NodeContentAndTag;

			ExControl exPanel_NodeGroup;
			ExControl exPanel_NodeInnerBody;
			ExControl exLabel_NodeContent;
			ExTextBox exTextBox_NodeContent;
			ExPictureBox exPictureBox_NodeImage;
			ExControl exFlowLayoutPanel_NodeTags;
			ExControl exPanel_NodeContentAndTag;
			#endregion
			#region Ui parameters
			public int GroupPadding = 10;
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
				panel_NodeGroup = new Panel();
				panel_NodeInnerBody = new Panel();
				label_NodeContent = new Label();
				textBox_NodeContent = new TextBox();
				pictureBox_NodeImage = new PictureBox();
				panel_NodeContentAndTag = new Panel();
				flowLayoutPanel_NodeTags = new FlowLayoutPanel();
				// 
				// panel_NodeGroup
				// 
				panel_NodeGroup.BorderStyle = BorderStyle.FixedSingle;
				panel_NodeGroup.Controls.Add(panel_NodeInnerBody);
				panel_NodeGroup.Location = new Point(0, 0);
				panel_NodeGroup.Name = "panel_NodeGroup";
				panel_NodeGroup.Size = new Size(400, 300);
				panel_NodeGroup.TabIndex = 6;

				// 
				// panel_NodeInnerBody
				// 
				panel_NodeInnerBody.BorderStyle = BorderStyle.FixedSingle;
				panel_NodeInnerBody.Controls.Add(pictureBox_NodeImage);
				panel_NodeInnerBody.Controls.Add(panel_NodeContentAndTag);
				panel_NodeInnerBody.Location = new Point(0, 0);
				panel_NodeInnerBody.Name = "panel_NodeInnerBody";
				panel_NodeInnerBody.Size = new Size(390, 244);
				panel_NodeInnerBody.TabIndex = 6;
				// 
				// label_NodeContent
				// 
				label_NodeContent.BorderStyle = BorderStyle.FixedSingle;
				label_NodeContent.Location = new Point(45, 14);
				label_NodeContent.Name = "label_NodeContent";
				label_NodeContent.Size = new Size(125, 34);
				label_NodeContent.TabIndex = 1;
				label_NodeContent.Text = "Label"; // test
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
				panel_NodeContentAndTag.Controls.Add(textBox_NodeContent);
				panel_NodeContentAndTag.Controls.Add(flowLayoutPanel_NodeTags);
				UiPKG.ReplaceControl(textBox_NodeContent, label_NodeContent, panel_NodeContentAndTag);
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
				exPanel_NodeGroup = new ExControl(panel_NodeGroup) { GroupInnerBody = exPanel_NodeInnerBody, EnableDrag = true };
				exPanel_NodeInnerBody = new ExControl(panel_NodeInnerBody) { GroupRoot = panel_NodeGroup};
				exFlowLayoutPanel_NodeTags = new ExControl(flowLayoutPanel_NodeTags) { GroupRoot = panel_NodeGroup};
				exPanel_NodeContentAndTag = new ExControl(panel_NodeContentAndTag) { GroupRoot = panel_NodeGroup};
				exPictureBox_NodeImage = new ExPictureBox(pictureBox_NodeImage) { GroupRoot = panel_NodeGroup, EnableResize = true, EnableRatioFixed = true };
				exTextBox_NodeContent = new ExTextBox(textBox_NodeContent) { GroupRoot = panel_NodeGroup};
				exLabel_NodeContent = new ExTextBox(textBox_NodeContent) { GroupRoot = panel_NodeGroup};
				/*
				 *exPanel_NodeGroup = new ExControl(panel_NodeGroup) { EnableGroup = true, GroupInnerBody = exPanel_NodeInnerBody, EnableFlex = true, EnableDrag = true, EnableResize = false };
				exPanel_NodeInnerBody = new ExControl(panel_NodeInnerBody) { EnableGroup = true, GroupRoot = panel_NodeGroup, EnableFlex = false, EnableDrag = false, EnableResize = false };
				exFlowLayoutPanel_NodeTags = new ExControl(flowLayoutPanel_NodeTags) { EnableGroup = true, GroupRoot = panel_NodeGroup, EnableFlex = false, EnableDrag = false, EnableResize = false };
				exPanel_NodeContentAndTag = new ExControl(panel_NodeContentAndTag) { EnableGroup = true, GroupRoot = panel_NodeGroup, EnableFlex = false, EnableDrag = false, EnableResize = false };
				exPictureBox_NodeImage = new ExPictureBox(pictureBox_NodeImage) { EnableGroup = true, GroupRoot = panel_NodeGroup, EnableFlex = true, EnableDrag = false, EnableResize = true, EnableRatioFixed = true};
				exTextBox_NodeContent = new ExTextBox(textBox_NodeContent) { EnableGroup = true, GroupRoot = panel_NodeGroup, EnableFlex = false, EnableDrag = false, EnableResize = false };
				exLabel_NodeContent = new ExTextBox(textBox_NodeContent) { EnableGroup = true, GroupRoot = panel_NodeGroup, EnableFlex = false, EnableDrag = false, EnableResize = false };
				 */
				ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
				ControlAligner.AlignControlsHorizontally(panel_NodeInnerBody);
				ControlAligner.AlignControlsHorizontally(panel_NodeGroup, GroupPadding);

				pictureBox_NodeImage.SizeChanged += AlignControlsInNode;
				textBox_NodeContent.SizeChanged += AlignControlsInNode;
				flowLayoutPanel_NodeTags.SizeChanged += AlignControlsInNode;
				#endregion

				_canvas.Controls.Add(panel_NodeGroup);
			}
			public void AlignControlsInNode(object sender,  EventArgs e)
			{
				// the order is important
				ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
				ControlAligner.AlignControlsHorizontally(panel_NodeInnerBody);
				ControlAligner.AlignControlsHorizontally(panel_NodeGroup, GroupPadding);
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
