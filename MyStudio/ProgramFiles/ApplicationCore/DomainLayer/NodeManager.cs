using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;
using DomainModel;
using Newtonsoft.Json.Linq;

namespace ApplicationLayer
{
	public class NodeManager:INodeManager
	{
		protected IIdManager? _idManager;
		protected IVaultDatabase _vaultDatabase;
		public Dictionary<string, Node> NodeDictionary = new Dictionary<string, Node>();

		public NodeManager(IIdManager idManager, IVaultDatabase vaultDatabase)
		{
			_idManager = idManager;
			_vaultDatabase = vaultDatabase;
		}
		public void CreateNode()
		{
			string id = _idManager.AquireId();
		}
		public void MaintainNode(string nodeId)
		{

		}
		public void DeleteNode(string nodeId)
		{

		}
		public void LoadNode(string nodeId)
		{

		}
		public void SaveNode(string nodeId)
		{

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
		public void LoadVaultNodes();
		public void SaveVaultNodes();
	}
	#endregion
}
