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
 * Basic Node operation
 */
namespace DomainModel
{
	public partial class Node
	{
		#region Static Ports
		static protected INodeManager? _nodeManager;
		static protected INodeContentProcessor? _contentProcessor;
		#endregion
		public void init( INodeManager dataManager, INodeContentProcessor contentProcessor)
		{
			_nodeManager = dataManager;
			_contentProcessor = contentProcessor;
		}
		static Node()
		{
			if (_nodeManager == null || _contentProcessor == null)
			{
				throw new InvalidOperationException("Please call Node.init first!");
			}
		}
		public Node()
		{
			Create();
		}
		public Node(string id)
		{
			_id = id;
			Load();
		}

		protected string _id;
		protected JObject _data = new JObject();

		protected object _dataLock = new object();

		


		#region Node data operation
		public void Create() // create a node
		{
			_nodeManager.InitializeNode(this);
		}
		public void Load() // load node data from database
		{
			_nodeManager.LoadNode(this);
		}
		public void Save() // save node data to database
		{
			_nodeManager.SaveNode(this);
		}
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
		public void WriteContent(string text) // write text to node content
		{
			_contentProcessor.WriteContentToNode(this, text);
		}
		public string ReadContent() // read text from node content
		{
			string text;
			text = _contentProcessor.ReadContentFromNode(this);
			return text;
		}
		#endregion
		#region Node relation management
		public void AddLink(Node targetNode, LinkType linkType)
		{
			SetByAdd<string>(targetNode.Id, PROPERTY, LINKING, linkType.ToString());
			targetNode.SetByAdd<string>(_id, PROPERTY, LINKED, linkType.ToString());
		}
		public void RemoveLink(Node targetNode, LinkType linkType)
		{
			SetByRemove<string>(targetNode.Id, PROPERTY, LINKING, linkType.ToString());
			targetNode.SetByRemove<string>(_id, PROPERTY, LINKED, linkType.ToString());
		}
		#endregion

		public string Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public JObject Data
		{
			set { _data = value; }
			get { return _data; }
		}
	}
	#region Ports 
	public interface INodeManager
	{
		public void InitializeNode(Node node);
		public void MaintainNode(Node node);
		public void DeleteNode(Node node);
		public void LoadNode(Node node);
		public void SaveNode(Node node);
	}
	public interface INodeContentProcessor
	{
		public void WriteContentToNode(Node baseNode, string text);
		public string ReadContentFromNode(Node baseNode);
		public void InsertReference(Node baseNode, Node referedNode);
	}
	#endregion
}
