using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using DomainModel;
using Newtonsoft.Json.Linq;

namespace DomainLayer
{
	public class GroupNode : Node
	{
		public List<Node> members = new List<Node>();
		public GroupNode(JObject data):base(data)
		{
			LoadMembers();
		}

		public void LoadMembers()
		{
			List<string> ids = Get<List<string>>(PROPERTY, LINKING, LinkType.Member.ToString());
			foreach (string id in ids)
			{
				members.Add(_nodeManager.FetchNode(id));
			}
		}
	}
	public class Template:Node
	{
		public Template(JObject data):base(data)
		{

		}
		public void AddLabelNode(string label)
		{
			Node labelNode = _nodeManager.CreateNode();
			labelNode.Set<string>(Node.ContentType.Label.ToString(), METADATA, CONTENT_TYPE);
		}
		public void AddDataNode()
		{
			Node dataNode = _nodeManager.CreateNode();
			dataNode.Set<string>(Node.ContentType.Data.ToString(), METADATA, CONTENT_TYPE);
		}
	}
	public class DatabaseNode:Node
	{
		public DatabaseNode(JObject data):base(data) 
		{
			LoadItems();
			LoadProperties();
			
		}

		public List<Node> items = new List<Node>();
		public List<Node> properties = new List<Node>();
		public void LoadItems()
		{
			List<string> ids = Get<List<string>>(PROPERTY, LINKING, LinkType.DB_Item.ToString());
			foreach (string id in ids)
			{
				items.Add(_nodeManager.FetchNode(id));
			}
		}
		public void LoadProperties()
		{
			List<string> ids = Get<List<string>>(PROPERTY, LINKING, LinkType.DB_Property.ToString());
			foreach (string id in ids)
			{
				properties.Add(_nodeManager.FetchNode(id));
			}
		}
		public void AddProperty(Node propertyNode)
		{
			SetByAdd<string>(propertyNode.Id, PROPERTY, LINKING, LinkType.DB_Property.ToString());
			properties.Add(propertyNode);
			foreach(Node item in items)
			{
				
			}
		}
		public void AddItem(Node itemNode)
		{

		}
		public Node GetDB(Node itemNode, Node PropertyNode)
		{

			return new Node();
		}

	}
}
