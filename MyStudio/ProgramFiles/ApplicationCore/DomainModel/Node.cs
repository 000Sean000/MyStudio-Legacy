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

namespace DomainModel
{
	public partial class Node
	{
		#region Static Ports
		static protected INodeDataManager? _dataManager;
		static protected INodeContentProcessor? _contentProcessor;
		#endregion
		public Node()
		{
			_id = _dataManager.GenerateNodeId();
			_data = _dataManager.CreateNodeData();
			_dataManager.SaveNodeData(_id, _data);
		}
		public Node(string id)
		{
			_id = id;
			_data = _dataManager.LoadNodeData(id);
		}

		protected string _id;
		protected JObject _data;

		protected object _dataLock = new object();

		


		#region Node data operation
		public void Create() // create a node
		{
			_data = _dataManager.CreateNodeData();
		}
		public void Load() // load node data from database
		{
			_data = _dataManager.LoadNodeData(_id);
		}
		public void Save() // save node data to database
		{
			_dataManager.SaveNodeData(_id, _data);
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

		public static INodeDataManager DataManager
		{
			set
			{
				_dataManager = value;
			}
		}
		public string Id
		{
			get { return _id; }
		}
	}
	#region Ports to infrastructure
	public interface INodeDataManager
	{
		JObject CreateNodeData();
		JObject MaintainNodeData(JObject data);
		JObject LoadNodeData(string nodeId);
		void SaveNodeData(string nodeId, JObject data);
		public string GenerateNodeId();
	}
	public interface INodeContentProcessor
	{
		void WriteContentToNode(Node baseNode, string text);
		string ReadContentFromNode(Node baseNode);
		void InsertReference(Node baseNode, Node referedNode);
	}
	#endregion
}
