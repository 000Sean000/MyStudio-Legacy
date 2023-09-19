using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;
using Newtonsoft.Json.Linq;

namespace ApplicationLayer
{
	public class NodeDataManager:Node.INodeDataManager
	{
		public NodeDataManager()
		{
			Node.DataManager = this;
		}
		public JObject CreateNodeData()
		{
			return new JObject();
		}
		public JObject MaintainNodeData(JObject data)
		{
			return data;
		}
		public JObject LoadNodeData(string nodeId)
		{
			return new JObject();
		}
		public void SaveNodeData(string nodeId, JObject data)
		{

		}

		public string GenerateNodeId()
		{
			return "";
		}

	}
}
