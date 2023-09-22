using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKG;
using DomainLayer;
using DomainModel;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
/*
 * Give Node required data
 * Manager Node entities, Nodes are only allow to be instanciated here
 * All other model only can access Nodes through NodeManager
 */
namespace DomainLayer
{
	public class NodeManager:INodeManager
	{
		protected IIdManager? _idManager;
		protected IVaultDatabase _vaultDatabase;
		protected INoteProcessor _noteProcessor;

		protected string _vaultPath;
		protected JObject _vaultData = new JObject();
		public Dictionary<string, Node> LoadedNodes = new Dictionary<string, Node>();

		public NodeManager(string vaultPath, INoteProcessor noteProcessor, IIdManager idManager, IVaultDatabase vaultDatabase)
		{
			_vaultPath = vaultPath;
			_idManager = idManager;
			_idManager.BindVault(_vaultPath);
			_noteProcessor = noteProcessor;
			_noteProcessor.BindNodeManager(this);
			_vaultDatabase = vaultDatabase;
			_vaultData = _vaultDatabase.LoadVaultData(_vaultPath);


		}
		#region Necessary Implementation
		public JObject InitNodeData()
		{
			JObject data = new JObject();
			data = Node.MaintainData(data);
			string id = _idManager.AcquireId();
			JsonPKG.SetJObject<string>(id, data, Node.METADATA, Node.ID); ;
			return data;
		}
		
		public void WriteContentToNode(Node node, string plaintext)
		{
			_noteProcessor.WriteContentToNode(node, plaintext);
		}
		public string? ReadContentFromNode(Node node)
		{
			return _noteProcessor.ReadContentFromNode(node);
		}
		public Node FetchNode(string id)
		{
			Node node;
			if (LoadedNodes.ContainsKey(id))
			{
				node = LoadedNodes[id];
			}
			else
			{
				JObject data = _vaultDatabase.LoadNodeData(id);
				node = new Node(data);
				AddLoadedNode(node);
			}
			return node;
		}
		#endregion
		public void AddLoadedNode(Node node)
		{
			if (!LoadedNodes.ContainsKey(node.Id))
			{
				LoadedNodes.Add(node.Id, node);
			}
		}
		public void RemoveLoadedNode(Node node)
		{
			if (LoadedNodes.ContainsKey(node.Id))
			{
				LoadedNodes.Remove(node.Id);
			}
		}

		
		public Node CreateNode()
		{
			Node node = new Node();
			AddLoadedNode(node);
			return node;
		}

	}
	#region Interfaces
	public interface IIdManager
	{
		public void BindVault(string vaultPath);
		public string AcquireId();
		public void ReleaseId(string id);
	}
	public interface INoteProcessor // Node content parsing
	{
		public void BindNodeManager(NodeManager nodeManager);
		public string GetReferenceForm(Node node);

		public void WriteContentToNode(Node node, string plaintext);
		public string? ReadContentFromNode(Node node);

	}
	public partial interface IVaultDatabase
	{
		public JObject LoadVaultData(string vaultPath);
		public void SaveVaultData(string vaultPath, JObject vaultData);
		public JObject LoadNodeData(string id);
		public void SaveNodeData(string id, JObject nodeData);
		public void DeleteNode(string id);
	}
	#endregion
}
