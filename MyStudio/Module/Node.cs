using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PKG;

namespace Module
{
	class Node
	{
        #region Json keys
		public const string METADATA = "Metadata";
		public const string ID = "id";
        public const string PATH = "Relative path to nodesDir";
        public const string NODE_TYPE = "type";
        public const string NODE_CLASS = "class";
        public const string CONTENT = "content";
        public const string TEXT = "text";
		public const string CITE = "cite node id";
		public const string PROPERTY = "Property";
        public const string LINK = "Link to";
		public const string BACK_LINK = "be linked by";
		public const string MEDIA = "Media";
        public const string IMAGE = "image";
		public const string HYPERLINK = "Hyperlink";
		public const string TAG = "Tag";
		public const string USER_DEF = "User-defined";
        public const string ATTRIBUTE = "attributes";
		public const string LOGIC = "Logic";
		public const string USAGE = "Usage";
		public const string PHASE = "Phase";
		public const string VISUAL = "Visual";
        // ...

        #region NodeType
		public const string DATA = "data node";
		public const string CITER = "citer node";
        #endregion
        #region NodeClass
        #endregion
        #region LinkType
        #endregion
        #endregion

        #region Pathes
        public static string nodesDir;
		public static string mediaDir;
		#endregion

		#region Data fields
		protected JObject _info;
		protected string _fileName;
		protected string _id;
		protected string _path;
        #endregion

        #region Node Operations
		Node() // new node
		{
			create();
			MaintainNode();
			Save();
		}
		Node(string id) // load existent node by id
		{
			_id = id;
            _fileName = "Node" + _id + ".json";
            _path = Path.Combine(nodesDir, _fileName);
			if (Directory.Exists(_path))
			{
                Load();
                MaintainNode();
                Save();
            }
			else
			{
				throw new Exception("Node not exist");
			}
		}
        public void create()
        {
            do
            {
                DateTime currentDateTime = DateTime.UtcNow;
                _id = currentDateTime.ToString("yyyyMMddHHmmssffffff");
                _fileName = "Node" + _id + ".json";
                _path = Path.Combine(nodesDir, _fileName);
            } while (File.Exists(_path)); // avoid overwriting a existent file
            _info = new JObject();
			
        }
		public void MaintainNode()
		{

			
		}
        public void Save()
        {
            JsonPKG.SaveJsonObjectToFile(_info, _path);
        }
        public void Load()
        {
            _info = JsonPKG.ReadJsonObjectFromFile(_path);
        }
        public void Set<T>(T value, params string[] keys)
		{
			JsonPKG.SetJObject(value, _info, keys);
		}
        public T Get<T>(params string[] keys)
		{
			if (keys[0] == CONTENT && _info[NODE_TYPE].ToString() == CITER)
			{
				Node cited = new Node(_info[CONTENT][CITE].ToString());
                return JsonPKG.GetJObject<T>(cited._info, keys);
            }
			else
			{
                return JsonPKG.GetJObject<T>(_info, keys);
            }

			
		}
		public void AddLink()
		{

		}
		public void RemoveLink()
		{

		}
		public Node Clone() // ...
		{
			Node cloned = new Node();
			cloned._info = _info;
			cloned.Set<string>(cloned._id, new string[] {METADATA, ID});
			cloned.Set<string>(cloned._path, new string[] {METADATA, PATH});
			return cloned;
		}
		#endregion

		#region Quick Accessors
		public string id { get { return _id; } }
		public string fileName { get { return _fileName; } }
		public string path { get { return _path; } }
        #endregion

        #region
        #endregion
    }
}
/*
namespace Module
{
	
	public class Node
	{
		#region keys or indexes to access node info
		public enum NodeType
		{
			Data, Citer
		}
		public enum NodeClass
		{
			Unclassified, Tag, VisualFormat, Template, Module, Group, View
		}
		public enum LinkType
		{
			Unclassified, Component, SubPart, Next, Related
		}
		public enum InfoKey
		{
			Metadata,
				Id, Path, NodeType, NodeClass,
			Content,
				Text, cite,
			Property
		}
		#endregion

		#region Necessary initialization of Node Class
		public static string? NODE_DIR;
		public static string? MEDIA_DIR;

		public static void Cache_init()
		{

		}
		public static void Create_raw_node()
		{

		}
		#endregion

		protected JObject _info;

		#region Node operation

		#endregion



	}
}
*/