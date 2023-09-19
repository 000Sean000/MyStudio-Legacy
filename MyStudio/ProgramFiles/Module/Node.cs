using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PKG;
using System.Text.RegularExpressions;

namespace Module
{
	public partial class Node
	{
		#region static fields
		public static Vault vault; // to use node cache
		public static Regex regexNodeId = new Regex(@"\d{20}");
		public static Regex regexCitedNode = new Regex(@"\(Node\d{20}\)");
		public static Regex regexCitation = new Regex(@"\[\[\(Node\d{20}\)([\s\S]*?)\]\]");
		public static Regex regexCiteForm = new Regex(@"^\[\[\(Node\d{20}\)\]\]$");
		// regular expression of citations, it matches "[[citation]]", where citation can be any character or newline or no character.
		//public const string CITATION_MARK = "[[CITATION]]";
		public const string ID_MISSING = "ID not found";
		#endregion

		#region Json keys
		public const string METADATA = "Metadata";
		public const string ID = "id";
		////public const string SUB_PATH = "Relative path to nodesDir";
		public const string NODE_TYPE = "Type";
		public const string NODE_CLASS = "Class";
		public const string CONTENT = "Content";
		public const string TEXT = "Text";
		public const string CITE = "cited node id";
		public const string PROPERTY = "Property";
		public const string LINK = "Link to";
		public const string BACK_LINK = "be linked by";
		public const string MEDIA = "Media";
		public const string IMAGE = "Image";
		public const string HYPERLINK = "Hyperlink";
		public const string TAG = "Tag";
		public const string USER_DEF = "User-defined";
		public const string ATTRIBUTE = "Attributes";
		public const string LOGIC = "Logic";
		public const string USAGE = "Usage";
		public const string PHASE = "Phase";
		public const string VISUAL = "Visual";
		// ...

		#region NodeType
		public enum NodeType
		{
			Data, Citer
		}
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
		protected string _id;
		#endregion

		protected object _infoLock = new object(); // mutex lock for multi-threading

		#region Node Operations
		public Node() // new node
		{
			create();
			Save();
		}
		public Node(string id) // load existent node by id
		{
			_id = id;
			if (Directory.Exists(path))
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
			} while (File.Exists(path)); // avoid overwriting a existent file
			_info = new JObject();
			MaintainNode();
			lock (_infoLock)
			{
				_info[METADATA][ID] = _id;
			}
		}
		public void MaintainNode()
		{
			Maintain(new string[] { });
			Maintain(new string[] { METADATA });
			Maintain<List<string>>(new List<string>(), new string[] { CONTENT, CITE });
			Maintain(new string[] { PROPERTY, MEDIA });
			Maintain(new string[] { PROPERTY, USER_DEF });
			Maintain(new string[] { ATTRIBUTE, LOGIC });
			Maintain(new string[] { ATTRIBUTE, VISUAL });
		}
		public void Save()
		{
			lock (_infoLock)
			{
				JsonPKG.SaveJsonObjectToFile(_info, path);
			}
		}
		public void Load()
		{
			lock (_infoLock)
			{
				_info = JsonPKG.ReadJsonObjectFromFile(path);
			}
		}
		public void Maintain(params string[] keys)
		{
			lock (_infoLock)
			{
				JsonPKG.MaintainJObject(_info, keys);
			}
		}
		public void Maintain<T>(T? initValue, params string[] keys)
		{
			lock (_infoLock)
			{
				JsonPKG.MaintainJObject<T>(_info, initValue, keys);
			}
		}
		public void Set<T>(T value, params string[] keys)
		{
			lock (_infoLock)
			{
				JsonPKG.SetJObject<T>(value, _info, keys);
			}
		}
		public T Get<T>(params string[] keys)
		{
			lock (_infoLock) 
			{
				if (keys[0] == CONTENT && _info[NODE_TYPE].ToObject<NodeType>() == NodeType.Citer)
				{
					Node cited = new Node(_info[CONTENT][CITE].ToString());
					return JsonPKG.GetJObject<T>(cited._info, keys);
				}
				else
				{
					return JsonPKG.GetJObject<T>(_info, keys);
				}
			}
		}
		
		public void AddLink()
		{
			lock (_infoLock)
			{

			}
		}
		public void RemoveLink()
		{
			lock (_infoLock)
			{

			}
		}
		public enum CloneType
		{
			Simple, WithLinks, WithSubNodes
		}
		public Node Clone(CloneType cloneType) // ...
		{
			Node clone = new Node();
			lock (_infoLock)
			{
				clone._info = JToken.FromObject(_info).ToObject<JObject>();
			}
			clone.Set<string>(clone._id, new string[] { METADATA, ID });
			if (cloneType == CloneType.Simple)
			{
				clone.Set<JObject>(new JObject(), new string[] { PROPERTY, LINK });
				clone.Set<JObject>(new JObject(), new string[] { PROPERTY, BACK_LINK });
			}
			else if (cloneType == CloneType.WithLinks)
			{

			}
			else if (cloneType == CloneType.WithSubNodes)
			{

			}
			return clone;
		}
		public void Delete()
		{
			File.Delete(path);
		}
		public void WriteContent(string content)
		{
			List<string> texts = ParsingPKG.ParseWithPattern(content, regexCitation);
			List<string> citedNodeIds = new List<string>();
			int index = 0;
			foreach (string text in texts)
			{
				if (regexCitation.IsMatch(text))
				{
					string citedNode = ParsingPKG.GetMatchingSubstring(text, regexCitedNode);
					if (citedNode != null)
					{
						string citedNodeId = ParsingPKG.GetMatchingSubstring(citedNode, regexNodeId);
						if (citedNodeId != null)
						{
							texts[index] = $"[[{citedNode}]]"; // to match regexCiteForm
							citedNodeIds.Add(citedNodeId);
						}
					}
				}
				index++;
			}
			Set<List<string>>(texts, CONTENT, TEXT);
			Set<List<string>>(citedNodeIds, CONTENT, CITE);
		}
		public string ReadContent()
		{
			string content;
			object textsLock = new object();
			List<string> texts = Get<List<string>>(CONTENT, TEXT);
			List<Thread> threadList = new List<Thread>();
			for (int index = 0; index < texts.Count; index++)
			{
				string text;
				lock (textsLock)
				{
					text = texts[index];
				}
				if (regexCiteForm.IsMatch(text))
				{
					string citedNodeId = ParsingPKG.GetMatchingSubstring(text, regexNodeId);
					Thread thread = new Thread((index_) => { 
						int index = (int)index_;
						Node citedNode = vault.FetchNode(citedNodeId);
						string subContent = citedNode.ReadContent();
						lock (textsLock)
						{
							texts[index] = subContent;
						}
					});
					threadList.Add(thread);
					thread.Start(index);
				}
			}
			foreach (Thread thread in threadList)
			{
				thread.Join();
			}
			content = string.Join("", texts);
			return content;
		}
		#endregion

		#region Properties (Accessors)
		public string id { get { return _id; } } 
		public string fileName { get { return $"Node{_id}.json"; } }
		public string path { get { return Path.Combine(nodesDir, fileName); } }
		#endregion

		
	}
}
