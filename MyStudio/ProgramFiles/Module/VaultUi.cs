using Newtonsoft.Json.Linq;
using PKG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKG;
using System.Diagnostics;

namespace Module
{
	partial class Vault
	{
		#region Field
		public Panel canvas;
		public Form rootForm;
		protected List<UiNode> _uiNodes = new List<UiNode>();

		#endregion

		#region Resource Locks
		protected object _uiNodesLock = new object();
		#endregion

		#region VaultForm Config
		public void ConfigUi()
		{
			
		}

		
		#endregion

		#region Node Ui Operation
		public class UiNode
		{
			protected Vault _vault; // outer class
			protected Panel _canvas;
			protected Form _rootForm;
			protected Node _node;
			public List<Control> _controls = new List<Control>();

			#region Ui components
			Panel panel_NodeGroup;
			Panel panel_NodeInnerBody;
			Label label_NodeContent;
			TextBox textBox_NodeContent;
			PictureBox pictureBox_NodeImage;
			FlowLayoutPanel flowLayoutPanel_NodeTag;
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
				_rootForm = _vault.rootForm;

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
				flowLayoutPanel_NodeTag = new FlowLayoutPanel();
				panel_NodeGroup.SuspendLayout();
				panel_NodeInnerBody.SuspendLayout();
				((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).BeginInit();
				panel_NodeContentAndTag.SuspendLayout();
				flowLayoutPanel_NodeTag.SuspendLayout();
				// 
				// panel_NodeGroup
				// 
				panel_NodeGroup.BorderStyle = BorderStyle.FixedSingle;
				panel_NodeGroup.Controls.Add(panel_NodeInnerBody);
				panel_NodeGroup.Location = new Point(0, 0);
				panel_NodeGroup.Name = "panel_NodeGroup";
				panel_NodeGroup.Padding = new Padding(10);
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
				pictureBox_NodeImage.Image = MyStudio.Properties.Resources.kazimierz_RB_Carve;
				pictureBox_NodeImage.Location = new Point(-1, 9);
				pictureBox_NodeImage.Name = "pictureBox_NodeImage";
				pictureBox_NodeImage.Size = new Size(224, 44);
				pictureBox_NodeImage.SizeMode = PictureBoxSizeMode.Zoom;
				pictureBox_NodeImage.TabIndex = 0;
				pictureBox_NodeImage.TabStop = false;
				// 
				// panel_NodeContentAndTag
				// 
				panel_NodeContentAndTag.BorderStyle = BorderStyle.FixedSingle;
				panel_NodeContentAndTag.Controls.Add(label_NodeContent);
				panel_NodeContentAndTag.Controls.Add(flowLayoutPanel_NodeTag);
				panel_NodeContentAndTag.Location = new Point(112, 83);
				panel_NodeContentAndTag.Name = "panel_NodeContentAndTag";
				panel_NodeContentAndTag.Size = new Size(250, 125);
				panel_NodeContentAndTag.TabIndex = 9;
				// 
				// flowLayoutPanel_NodeTag
				// 
				flowLayoutPanel_NodeTag.AutoSize = true;
				flowLayoutPanel_NodeTag.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				flowLayoutPanel_NodeTag.BorderStyle = BorderStyle.FixedSingle;
				//flowLayoutPanel_NodeTag.Controls.Add(button3);
				//flowLayoutPanel_NodeTag.Controls.Add(button4);
				//flowLayoutPanel_NodeTag.Controls.Add(button5);
				flowLayoutPanel_NodeTag.FlowDirection = FlowDirection.RightToLeft;
				flowLayoutPanel_NodeTag.Location = new Point(23, 72);
				flowLayoutPanel_NodeTag.Name = "flowLayoutPanel_NodeTag";
				flowLayoutPanel_NodeTag.Size = new Size(302, 37);
				flowLayoutPanel_NodeTag.TabIndex = 2;

				panel_NodeGroup.ResumeLayout(false);
				panel_NodeInnerBody.ResumeLayout(false);
				((System.ComponentModel.ISupportInitialize)pictureBox_NodeImage).EndInit();
				panel_NodeContentAndTag.ResumeLayout(false);
				panel_NodeContentAndTag.PerformLayout();
				flowLayoutPanel_NodeTag.ResumeLayout(false);
				#endregion

				#region Control Extension initialization

				UiPKG.ReplaceControl(label_NodeContent, textBox_NodeContent, panel_NodeContentAndTag);
				exPanel_NodeGroup = new ExControl(panel_NodeGroup, _rootForm) { EnablePaintBorder = true, EnableDrag = true };
				exPanel_NodeInnerBody = new ExControl(panel_NodeInnerBody, _rootForm) { GroupRoot = exPanel_NodeGroup};
				exFlowLayoutPanel_NodeTags = new ExControl(flowLayoutPanel_NodeTag, _rootForm) { GroupRoot = exPanel_NodeGroup};
				exPanel_NodeContentAndTag = new ExControl(panel_NodeContentAndTag, _rootForm) { GroupRoot = exPanel_NodeGroup};
				exPictureBox_NodeImage = new ExPictureBox(pictureBox_NodeImage, _rootForm) { EnablePaintBorder = false, GroupRoot = exPanel_NodeGroup, EnableResize = true, EnableRatioFixed = true };
				exTextBox_NodeContent = new ExTextBox(textBox_NodeContent, _rootForm) { GroupRoot = exPanel_NodeGroup};
				exLabel_NodeContent = new ExControl(label_NodeContent, _rootForm) { GroupRoot = exPanel_NodeGroup};

				exPanel_NodeGroup.Watch_FunctionalityEnable();


				AlignControlsInNode();


				pictureBox_NodeImage.SizeChanged += AlignControlsInNode_handler;
				textBox_NodeContent.SizeChanged += AlignControlsInNode_handler;
				label_NodeContent.SizeChanged += AlignControlsInNode_handler;
				flowLayoutPanel_NodeTag.SizeChanged += AlignControlsInNode_handler;
				exPictureBox_NodeImage.ActionAfterFlex += AlignControlsInNode;

				#endregion

				_canvas.Controls.Add(panel_NodeGroup);
			}
			public void AlignControlsInNode()
			{
				Debug.WriteLine("Alignment");
				// the order is important
				ControlAligner.AlignControlsVertically(panel_NodeContentAndTag);
				ControlAligner.AlignControlsVertically(panel_NodeInnerBody);
				ControlAligner.AlignControlsHorizontally(panel_NodeGroup, GroupPadding);
			}
			public void AlignControlsInNode_handler(object sender,  EventArgs e)
			{
				AlignControlsInNode();
			}
			public void AlignControlsInNode_handler(object sender, ControlEventArgs e)
			{
				AlignControlsInNode();
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
