using Module;
using Newtonsoft.Json.Linq;
using PKG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
	public partial class Node
	{
		#region Static 
		static protected INodeDataManager _dataManager;
		static protected INodeContentProcessor _contentProcessor;
		#endregion
		public Node()
		{
			_id = _dataManager.GetNodeId();
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

		#region Port to infrastructure
		public interface INodeDataManager
		{
			JObject CreateNodeData();
			JObject MaintainNodeData(JObject data);
			JObject LoadNodeData(string nodeId);
			void SaveNodeData(string nodeId, JObject data);
			
		}
		public interface INodeContentProcessor
		{
			object WriteContent(string text);
			string ReadContent(object contentSet);
		}
		#endregion


		#region Node data operation
		public void Create() // create a node
		{
			_data = _dataManager.CreateNode();
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
			object contentSet;
			contentSet = _contentProcessor.WriteContent(text);
			Set<object>(contentSet, CONTENT, TEXT);
		}
		public string ReadContent() // read text from node content
		{
			object contentSet = Get<object>(CONTENT, TEXT);
			string text;
			text = _contentProcessor.ReadContent(contentSet);
			return text;
		}
		#endregion
		#region Node relation management
		public void AddLink(string targetNodeId, LinkType linkType)
		{
			SetByAdd<string>(targetNodeId, PROPERTY, LINKING, linkType.ToString());
			Node targetNode = new Node(targetNodeId);
			targetNode.SetByAdd<string>(_id, PROPERTY, LINKED, linkType.ToString());
		}
		public void RemoveLink(string targetNodeId, LinkType linkType)
		{
			SetByRemove<string>(targetNodeId, PROPERTY, LINKING, linkType.ToString());
			Node targetNode = new Node(targetNodeId);
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
	}
}
