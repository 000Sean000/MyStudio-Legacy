using Module;
using Newtonsoft.Json.Linq;
using PKG;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static NodeModel.Node;
/*
 * Basic Node data operation
 * Don't create Node instance here, do this in Node Manager!
 * Use Node as argument instead of using id
 */
namespace NodeModel
{
	public interface NodeDataAccessor
	{
		#region Data Accessors
		public string Id { get; set; }
		#endregion
		#region 
		public void AddLink(Node targetNode, EnumLinkType linkType);
		public void RemoveLink(Node targetNode, EnumLinkType linkType);
		#endregion
	}
	public partial class Node0
	{
		#region Outer Models
		static protected INodeManager? _nodeManager;
		#endregion

		protected JObject _data;

		#region Static Members
		public static void BindNodeManager(INodeManager nodeManager)
		{
			_nodeManager = nodeManager;
		}
		public static JObject MaintainData(JObject data) // maintain keys and default value
		{

			return data;
		}
		#endregion
		static Node()
		{
			if (_nodeManager == null)
			{
				throw new InvalidOperationException("Please call Node.init first!");
			}
		}
		public Node() // Create a new Node
		{
			_data = _nodeManager.InitNodeData();
		}
		public Node(JObject data) // Encapsulate data to a Node
		{
			_data = data;
		}

		#region Node data operation

		protected void Set<T>(T value, params string[] keys) // set node data key-value
		{
				JsonPKG.SetJObject<T>(value, _data, keys);
		}
		protected T Get<T>(params string[] keys) // get node data key-value
		{
				return JsonPKG.GetJObject<T>(_data, keys);
		}
		protected void SetByAdd<T>(T element, params string[] keys) // set node data key-value
		{
			JsonPKG.SetJObjectByAddElementToList<T>(element, _data, keys);
		}
		protected void SetByRemove<T>(T element, params string[] keys) // set node data key-value
		{
			JsonPKG.SetJObjectByRemoveElementFromList<T>(element, _data, keys);
		}
		protected void WriteContent(string plaintext) // write text to node content
		{
			_nodeManager.WriteContentToNode(this, plaintext);
		}
		protected string ReadContent() // read text from node content
		{
			return _nodeManager.ReadContentFromNode(this);
		}
		#endregion
		#region Node relation management
		public void AddLink(Node targetNode, EnumLinkType linkType)
		{
			SetByAdd<string>(targetNode.Id, PROPERTY, LINKING, linkType.ToString());
			targetNode.SetByAdd<string>(Id, PROPERTY, LINKED, linkType.ToString());
		}
		public void RemoveLink(Node targetNode, EnumLinkType linkType)
		{
			SetByRemove<string>(targetNode.Id, PROPERTY, LINKING, linkType.ToString());
			targetNode.SetByRemove<string>(Id, PROPERTY, LINKED, linkType.ToString());
		}
		#endregion
		
	}
	#region Interface
	public interface INodeManager // Driving & Driven
	{
		public JObject InitNodeData();
		public void WriteContentToNode(Node node, string plaintext);
		public string? ReadContentFromNode(Node node);
		public Node FetchNode(string id);
		public Node CreateNode();
	}

	#endregion
}
