using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PKG;

/// <summary>
/// Data structure
/// </summary>
/// Module concept: 
///		class		(module template)
///		instance	(module data object)
///		set			(instances)
///	Design plan
///		let node contain the data real need to show
///		then the key and sub-key for linked-node can be omit in visual
///		node type: 
///			module for catelogue
///			thought for design thread
///		use derived class as template of nodes for different usage
///		customize visibility of linked nodes by link type
/// Module contents
///		Meta data
///			id
///			file path
///		Content
///			brief rank:
///			{
///				1. title
///				2. brief 
///				3. summary
///				4. complete description
///			}
///		Property (all optional)
///			relation link info[...][LINK][TYPE][NAME][...][targetID] = targetPath
///				type: 
///				{
///					unclassified
///					components
///					property
///					reference
///					detail	(unordered/ordered set) : {futher info, tell, show}
///					next	
///					design flow
///					instance
///					log: {cause, requirement, plan}
///				}
///			back-link
///			image
///			tag
///			customized...
///		Attribute
///			status of usage
///			rank of preference
///				rank r = integer (~, -1, 0, +1, ~)	the liker the bigger
///				scale base b = (0,1)
///				node scale s = b^(-r)
///				version score = product of nodes = s1*s2*...*sN
///			progress stage/phase
///			state for logic operation
///				show or not
///				rank of content
///		Others
///			Version/selection, rank/level, definition
///	Data structure plan
///		1. Basic node:	{ description, links }
///		2. 
///			

/// <summary>
/// Issues
/// </summary>
/// update issue, remember to load() again when a node is modified by other => passing node by reference
/// is nodes with same ID in same memory? if not: be care to update node when it is modified by other nodes
/// remove link when delete
/// protected _info operation: remove key
/// change file path and name
/// clone by copy .json text
/// backup and recover

/*

namespace Module_developing
{
	public class Node
	{
		#region Constant strings

		#region 1. Keys
		public const string CONTENT = "content";
		public const string PROPERTY = "property";
			public const string LINK = "link";
				public const string UNCLASSIFIED = "unclassified";
					public const string UNNAMED = "unnamed";
			public const string BACK_LINK = "back link";
        public const string IMAGE = "image";
			public const string ICON = "icon";
			public const string PROFILE = "profile"; // 頭像
			public const string PORTRAIT = "portrait"; // 肖像
			public const string ILLUSTRATION = "illustration"; // 插圖
			public const string LOGO = "logo";
		public const string ATTRIBUTE = "attribute";
		public const string USAGE = "usage";
		public const string 
		public static List<string> PROPERTYs = new List<string>()
		{
			TITLE, SUMMARY, DESCRIPTION, LINK, BACK_LINK, IMAGE
		};
		#endregion
		#endregion

		#region Pathes &　Meta data
		protected string _id;　// timestamp
        protected string _fileName; // xxx.json
		protected string _filePath; // xx/Nodes/xxx.json
		public static string nodeDir; // directory to save nodes: xx/Nodes
		#endregion

		#region Data variables
		private JObject _info;
		public List<Node> _linkNodes = new List<Node>();
		#endregion
		public Node() // create a new node
		{
			create();
		}
		public Node(string filePath) // load a existent node
		{ 
			_info = new JObject(); 
			if (File.Exists(filePath))
			{
				_filePath = filePath;
				load();
			}
			else
			{
				throw new Exception("Node file path does not exist!");
			}
		}
		public void test()
		{
			
		}
		public void create()
		{
			do
			{
				DateTime currentDateTime = DateTime.UtcNow;
				_info[ID] = currentDateTime.ToString("yyyy/MM/dd-HH:mm:ss");
                _fileName = currentDateTime.ToString("yyyy-MM-dd--HH-mm-ss") + ".json";
				_filePath = Path.Combine(nodeDir, _fileName);
			} while (File.Exists(_filePath)); // avoid overwriting a existent file
			_info[LINK] = new JObject();
			_info[BACK_LINK] = new JObject();
			save();
		}

        #region Linked Node operations 
        public void addLink(Node targetNode, string? linkType = null, string? linkName = null)
        {
			string targetID = targetNode._filePath;
            _info[LINK][targetID] = new JObject();
            _info[LINK][targetID][LINK_PATH] = targetNode._filePath;
            _info[LINK][targetID][LINK_TYPE] = linkType;
            _info[LINK][targetID][LINK_NAME] = linkName;
			save();

            targetNode.setInfo<string>(_info[PATH].ToString(), BACK_LINK, _info[ID].ToString());
            //targetNode._info[BACK_LINK][_info[NAME]] = _info[PATH];
			//targetNode.save();

			_linkNodes.Add(targetNode);

        }
        public void removeLink(string nodeName)
        {

            string linkPath = _info[LINK][nodeName][LINK_PATH].ToString();
            Node targetNode = new Node(linkPath);
            targetNode._info[BACK_LINK][_info[NAME]] = _info[PATH];
            targetNode.save();

            _info[LINK].ToObject<JObject>().Remove(nodeName);
            save();
            foreach (Node node in _linkNodes)
			{
				if (node._info[NAME].ToString() == nodeName)
				{
                    _linkNodes.Remove(node);
                }
			}
        }
		public void modifyLinkType(string nodeName, string linkType, string linkName)
		{
            _info[LINK][nodeName][LINK_TYPE] = linkType;
            _info[LINK][nodeName][LINK_NAME] = linkName;
			save();
        }
		public void modifyLinkName(string nodeName, string linkName)
		{
            _info[LINK][nodeName][LINK_NAME] = linkName;
			save();
        }
        public void addNode(string nodeName, string? linkType = null, string? linkName = null)
        {
            Node newNode = new Node();
			addLink(newNode, linkType, linkName);
			//save();
        }
        #endregion

        #region Node json object & file operations
        public void save()
		{
			PKG.JsonPKG.SaveJsonObjectToFile(_info, _filePath);
		}
		public void load()
		{
			_info = PKG.JsonPKG.ReadJsonObjectFromFile(_filePath);
		}
		public T getInfo<T>(params string[] keys)
		{
			int len = keys.Length;
			JObject obj = _info;
			for (int i = 0; i < len - 1; i++)
			{
				obj = obj[keys[i]].Value<JObject>();
			}
			JToken jtoken = obj[keys[len - 1]];
			T value = PKG.JsonPKG.JTokenToValue<T>(jtoken);
			return value;
		}
		public void setInfo<T>(T value, params string[] keys)
		{
            int len = keys.Length;
            JObject obj = _info;
            for (int i = 0; i < len - 1; i++)
            {
				
				if (!obj.ContainsKey(keys[i]) || obj[keys[i]] == null)
				{
					obj[keys[i]] = new JObject();
					obj = obj[keys[i]].Value<JObject>();
				}
				else
				{
					obj = obj[keys[i]].Value<JObject>();
				}
			}
			obj[keys[len - 1]] = JToken.FromObject(value);
			save();
		}
		public void removeKeyFromInfo(string key, params string[] keys)
		{

		}
		public void delete()
		{
			if (File.Exists(_filePath))
			{
				
				File.Delete(_filePath);
				if (File.Exists(_fileName))
				{
					Logger.WriteLine(String.Format("Failed to delete {0}", _filePath));
				}
			}
		}
		#endregion
	}
}
*/