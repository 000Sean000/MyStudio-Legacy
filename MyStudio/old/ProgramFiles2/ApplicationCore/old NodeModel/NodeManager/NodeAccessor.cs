using Newtonsoft.Json.Linq;
using PKG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace NodeModel
{
	/*
	 * Using Json Object saving Node data
	 */
	public partial class Node
	{

		#region Json keys
		public const string METADATA = "Metadata";
		public const string ID = "id";
		////public const string SUB_PATH = "Relative path to nodesDir";
		public const string NODE_TYPE = "Node Type";
		public const string NODE_CLASS = "Node Class"; 
		public const string CONTENT = "Content";
		public const string TEXT_SET = "TextSet";
		public const string REF_ID = "Refered node ids";
		public const string REFERING = "refering to";
		public const string REFERED = "be refered by";
		public const string PROPERTY = "Property";
		public const string LINKING = "linking to";
		public const string LINKED = "be linked by";
		public const string MEDIA = "Media";
		public const string IMAGE = "Image";
		public const string HYPERLINK = "Hyperlink";
		public const string TAG = "Tag";
		public const string USER_DEF = "User-defined";
		public const string ATTRIBUTE = "Attributes";
		public const string LOGIC = "Logic";
		public const string USAGE = "Usage";
		public const string PHASE = "Phase";
		public const string VISUAL = "Visual Format";
		// ...

		public const string CONTENT_TYPE = "Content Type";
		public enum EnumContentType
		{
			Data, Label
		}
		public enum EnumNodeType
		{
			Normal, Citer
		}
		public enum EnumNodeClass
		{
			Template, Module, Group, Database, View, Tag, Layout, Appearance,
		}
		public enum EnumLinkType
		{
			Parent, Component, SubPart, Next, Related, // normal node

			Member, // Group
			DB_Item, DB_Property, // database group

		}

		#endregion
		#region Accessor
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
		public EnumContentType ContentType
		{
			set { Set<string>(value.ToString(), METADATA, CONTENT_TYPE); }
			get { return Get<EnumContentType>(METADATA, CONTENT_TYPE); }
		}
		public string Content
		{
			set { WriteContent(value); }
			get { return ReadContent(); }
		}
		public List<string> ContentTextSet
		{
			set { Set<List<string>>(value, CONTENT, TEXT_SET); }
			get { return Get<List<string>>(CONTENT, TEXT_SET); }
		}

		public JObject Data { get { return _data; } }
		#endregion

	}
	public partial class Node
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
}