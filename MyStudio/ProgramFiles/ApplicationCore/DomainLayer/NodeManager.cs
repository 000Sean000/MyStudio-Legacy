using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;
using DomainModel;
using Newtonsoft.Json.Linq;

namespace DomainLayer
{
	public class NodeManager:INodeManager
	{
		protected IIdManager? _idManager;
		protected IVaultDatabase _vaultDatabase;

		protected JObject Vault = new JObject();
		public Dictionary<string, Node> LoadedNodes = new Dictionary<string, Node>();

		public NodeManager(IIdManager idManager, IVaultDatabase vaultDatabase)
		{
			_idManager = idManager;
			_vaultDatabase = vaultDatabase;
		}
		public void InitializeNode(Node node)
		{
			node.Id = _idManager.AquireId();
			MaintainNode(node);
			node.Set<string>(node.Id, Node.METADATA, Node.ID);

		}
		public void MaintainNode(Node node)
		{

		}
		public void DeleteNode(Node node)
		{

		}
		public void LoadNode(Node node)
		{
			AddLoadedNode(node);
			node.Data = _vaultDatabase.LoadNodeData(node.Id);
		}
		public void SaveNode(Node node)
		{

		}
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

	}
	#region Ports
	public interface IIdManager
	{
		
		public string AquireId();
		public void ReleaseId(string id);
	}
	public partial interface IVaultDatabase
	{
		public void LoadVault();
		public void SaveVault();
		public void LoadVaultNodes();
		public void SaveVaultNodes();
		public JObject LoadNodeData(string id);
		public void SaveNodeData(string id, JObject nodeData);
		public void DeleteNode(string id);
	}
	#endregion
}
