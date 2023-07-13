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
		public static Regex regexCitation = new Regex(@"\[\[([\s\S]*?)\]\]");
		// regular expression of citations, it matches "[[citation]]", where citation can be any character or newline or no character.
		public const string CITATION_MARK = "[[CITATION]]";
		#endregion

		#region Json keys
		public const string METADATA = "Metadata";
		public const string ID = "id";
		////public const string SUB_PATH = "Relative path to nodesDir";
		public const string NODE_TYPE = "type";
		public const string NODE_CLASS = "class";
		public const string CONTENT = "content";
		public const string TEXT = "text";
		public const string CITE = "cited node id";
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
			/*
			if (!_info.ContainsKey(METADATA))
			{
				_info[METADATA] = new JObject();
			}
			if (!_info.ContainsKey(CONTENT))
			{
				_info[CONTENT] = new JObject();
			}
			if (!_info.ContainsKey(PROPERTY))
			{
				_info[PROPERTY] = new JObject();
			}
			if (!_info.ContainsKey(ATTRIBUTE))
			{
				_info[ATTRIBUTE] = new JObject();
			}
			*/
			Maintain(new string[] { });
			Maintain(new string[] { METADATA });
			Maintain(new string[] { CONTENT });
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
		#endregion

		#region Properties (Accessors)
		public string id { get { return _id; } }
		public string fileName { get { return "Node" + _id + ".json"; } }
		public string path { get { return Path.Combine(nodesDir, fileName); } }
		#endregion

		#region Ui-related operation
		public static List<string> SeparateSubstringsAndCitations(string input)
		{
			List<string> substrings = new List<string>();
			List<string> citations = new List<string>();

			int currentIndex = 0;

			MatchCollection matches = regexCitation.Matches(input);

			foreach (Match match in matches)
			{
				if (match.Index > currentIndex)
				{
					string substring = input.Substring(currentIndex, match.Index - currentIndex);
					substrings.Add(substring);
				}

				string citation = match.Groups[1].Value;
				citations.Add(citation);
				substrings.Add(CITATION_MARK);
				currentIndex = match.Index + match.Length;
			}

			if (currentIndex < input.Length)
			{
				string remainingSubstring = input.Substring(currentIndex);
				substrings.Add(remainingSubstring);
			}

			return substrings;
		}
		#endregion
	}
}
