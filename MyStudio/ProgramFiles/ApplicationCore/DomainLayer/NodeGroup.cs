using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
namespace DomainLayer
{
	public class SimpleGroup : Node
	{
		public SimpleGroup()
		{

		}
	}
	public class DatabaseGroup:Node
	{
		public DatabaseGroup():base() 
		{
			LoadItems();
			LoadProperties();
			
		}
		public List<Node> items = new List<Node>();
		public List<Node> properties = new List<Node>();
		public void LoadItems()
		{
			List<string> ids = Get<List<string>>(PROPERTY, LINKING, LinkType.DB_ITEM.ToString());
			foreach (string id in ids)
			{
				items.Add(new Node(id));
			}
		}
		public void LoadProperties()
		{
			List<string> ids = Get<List<string>>(PROPERTY, LINKING, LinkType.DB_PROPERTY.ToString());
			foreach (string id in ids)
			{
				properties.Add(new Node(id));
			}
		}
		public void AddProperty(Node propertyNode)
		{
			SetByAdd<string>(propertyNode.Id, PROPERTY, LINKING, LinkType.DB_PROPERTY.ToString());
			properties.Add(propertyNode);
			foreach(Node item in items)
			{
				
			}
		}


	}
}
