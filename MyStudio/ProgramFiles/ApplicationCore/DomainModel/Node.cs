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
/*
 * Basic Node data operation
 * Don't create Node instance here, do this in Node Manager!
 */
namespace DomainModel
{
	public partial class Node
	{
		#region Outer Models
		static protected INodeManager? _nodeManager;
		#endregion

		protected JObject _data;

		#region Static Members
		public static void init(INodeManager dataManager)
		{
			_nodeManager = dataManager;
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
			_nodeManager.AddLoadedNode(this);
		}
		public Node(JObject data) // Encapsulate data to a Node
		{
			_data = data;
		}

		#region Node data operation

		public void Set<T>(T value, params string[] keys) // set node data key-value
		{
				JsonPKG.SetJObject<T>(value, _data, keys);
		}
		public T Get<T>(params string[] keys) // get node data key-value
		{
				return JsonPKG.GetJObject<T>(_data, keys);
		}
		public void SetByAdd<T>(T element, params string[] keys) // set node data key-value
		{
			JsonPKG.SetJObjectByAddElementToList<T>(element, _data, keys);
		}
		public void SetByRemove<T>(T element, params string[] keys) // set node data key-value
		{
			JsonPKG.SetJObjectByRemoveElementFromList<T>(element, _data, keys);
		}
		public void WriteContent(string plaintext) // write text to node content
		{
			_nodeManager.WriteContentToNode(this, plaintext);
			return;
		}
		public string ReadContent() // read text from node content
		{
			return _nodeManager.ReadContentFromNode(this);
		}
		#endregion
		#region Node relation management
		public void AddLink(Node targetNode, LinkType linkType)
		{
			SetByAdd<string>(targetNode.Id, PROPERTY, LINKING, linkType.ToString());
			targetNode.SetByAdd<string>(Id, PROPERTY, LINKED, linkType.ToString());
		}
		public void RemoveLink(Node targetNode, LinkType linkType)
		{
			SetByRemove<string>(targetNode.Id, PROPERTY, LINKING, linkType.ToString());
			targetNode.SetByRemove<string>(Id, PROPERTY, LINKED, linkType.ToString());
		}
		#endregion

		public string Id
		{
			get
			{
				return Get<string>(METADATA, ID);
			}
			set 
			{ 
				Set<string>(value, METADATA, ID); 
			}
		}
		public JObject Data { get { return _data; } }
	}
	#region Interface for inverse control
	public interface INodeManager
	{
		public JObject InitNodeData();
		public void AddLoadedNode(Node node);
		public void RemoveLoadedNode(Node node);
		public void WriteContentToNode(Node baseNode, string plaintext);
		public string? ReadContentFromNode(Node baseNode);
	}
	
	#endregion
}
