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

namespace Module
{
	public class Node
	{
		#region Constant strings
		#region 1. Program related keys
		public const string ID = "id";
		public const string NAME = "name";
		public const string PATH = "path";
		#endregion
		#region 2. Content related keys
		public const string TITLE = "title";
		public const string SUMMARY = "summary";
		public const string DESCRIPTION = "description";
		public const string LINK = "link";
		public const string LINK_TYPE = "relation type";
		public const string LINK_NAME = "relation name";
		public const string LINK_PATH = "link path";
		public const string LINK_OBJ = "related object";
		public static List<string> PROPERTYs = new List<string>()
		{
			TITLE, SUMMARY, DESCRIPTION
		};
        #endregion
        #endregion

        #region Pathes
        protected string _fileName; // xxx.json
		protected string _filePath; // xx/Nodes/xxx.json
		public static string nodeDir; // directory to save nodes: xx/Nodes
		#endregion

		#region Data variables
		protected JObject _info;
		//protected static JObject? _linkInfoTemplate = null;
		protected JObject? _linkInfo = null;
		protected List<Node> _linkNodes = new List<Node>();
		#endregion
		public static void init()
		{
			//_linkInfoTemplate = new JObject();
		
		}
		
		public Node(string nameOrPath) 
		{ 
			_info = new JObject(); 
			if (File.Exists(nameOrPath))
			{
				_filePath = nameOrPath;
				load();
			}
			else
			{
				create(nameOrPath);
			}
		}
		public void create(string name)
		{
			/*
			foreach (string key in PROPERTYs)
			{
				if (_info[key] == null)
				{
					_info[key] = " ";
				}
			}
			*/
			_info[NAME] = name;
			do
			{
				DateTime currentDateTime = DateTime.UtcNow;

				// 获取时间戳的字符串表示形式
				string timestampString = currentDateTime.ToString("yyyy-MM-dd HH-mm-ss");
				_info[ID] = name + ' ' + timestampString;
				_fileName = _info[ID] + ".json";
				_filePath = Path.Combine(nodeDir, _fileName);
			} while (File.Exists(_filePath));
			_linkInfo = new JObject();
			_info[LINK] = _linkInfo;
			save();
		}

        #region Linked Node operation 
        public void addLink(string nodeName, string linkPath, string? linkType = null, string? linkName = null)
        {
            _linkInfo[LINK][nodeName] = new JObject();
            _linkInfo[LINK][nodeName][LINK_PATH] = linkPath;
            _linkInfo[LINK][nodeName][LINK_TYPE] = linkType;
            _linkInfo[LINK][nodeName][LINK_NAME] = linkName;
			save();
        }
        public void removeLink(string nodeName)
        {

			_info[LINK].ToObject<JObject>().Remove(nodeName);
            if (_info[LINK].ToObject<JObject>().ContainsKey(nodeName))
			{
				throw new Exception("remove key failed");
			}
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
            _linkInfo[LINK][nodeName][LINK_TYPE] = linkType;
            _linkInfo[LINK][nodeName][LINK_NAME] = linkName;
			save();
        }
		public void modifyLinkName(string nodeName, string linkName)
		{
            _linkInfo[LINK][nodeName][LINK_NAME] = linkName;
			save();
        }
        public void addNode(string nodeName, string linkType, string linkName)
        {
            Node newNode = new Node(nodeName);
            _linkNodes.Add(newNode);
            string linkPath = newNode._filePath;
            addLink(nodeName, linkType, linkName, linkPath);

			save();
        }
        #endregion

        #region Node json file operations
        public void save()
		{
			PKG.JsonPKG.SaveJsonObjectToFile(_info, _filePath);
		}
		public void load()
		{
			_info = PKG.JsonPKG.ReadJsonObjectFromFile(_filePath);
		}
		public void write(string key, string value)
		{
			_info[key] = value;
			save();
		}
		public string read(string key)
		{
			return _info[key].Value<string>();
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
