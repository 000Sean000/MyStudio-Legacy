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

		protected JObject Vault = new JObject();
		public Dictionary<string, Node> LoadedNodes = new Dictionary<string, Node>();

		public NodeManager(IIdManager idManager, IVaultDatabase vaultDatabase)
		{
			_idManager = idManager;
			_vaultDatabase = vaultDatabase;
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
		public void WriteContentToNode(Node baseNode, string plaintext)
		{

		}
		public string? ReadContentFromNode(Node baseNode)
		{
			string plaintext = "";

			return plaintext;
		}

		#endregion
	
		
		

	}
	#region Interfaces
	public interface IIdManager
	{
		public string AcquireId();
		public void ReleaseId(string id);
	}
	public interface INodeContentProcessor
	{
		public void WriteContentToNode(Node baseNode, string text);
		public string ReadContentFromNode(Node baseNode);
		public string InsertReference(Node baseNode, Node referedNode);
		public string TextSetToPlaintext(List<string> textSet);
		public List<string> PlaintextToTextSet(string plaintext);

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
