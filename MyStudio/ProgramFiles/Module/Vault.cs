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
	public class Vault
	{
		#region Path names
		public const string SETTING_FILE = "VaultSetting.json";
		public const string CACHE_FILE = "NodeCache.json";
		public const string PROGRAM_DATA_FOLDER = "ProgramData";
		public const string NODE_FOLDER = "Nodes";
		public const string MEDIA_FOLDER = "Media";
		#endregion

		#region Path variables
		public string vaultDir;
		public string programDataDir;
		public string settingPath;
		public string cachePath;
		public string nodesDir;
		public string mediaDir;
		#endregion

		#region Data fields
		protected JObject _setting;
		////protected JObject _cache;
		protected List<string> _nodeIDs = new List<string>();
		protected Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
		#endregion

		#region Resource Locks
		public object SettingLock = new object();
		public object NodesLock = new object();
		#endregion

		#region Json keys
		public const string NODES = "Nodes";
		public const string MEDIA = "Media";
		public const string VOLATILE = "Volatile";
		public const string INVOLATILE = "Involatile";
		public const string PATH = "Node Pathes";
		public const string MODIFIED = "Modification counter";
		#endregion

		public static int? cachFreqLim;
		public static int? cachNumLim;

		public Vault(string directory)
		{
			if (!Directory.Exists(directory))
			{
				throw new Exception("Vault not exist!");
			}
			
			vaultDir = directory;
			programDataDir = Path.Combine(vaultDir, PROGRAM_DATA_FOLDER);
			Directory.CreateDirectory(programDataDir);
			nodesDir = Path.Combine(programDataDir, NODE_FOLDER);
			Directory.CreateDirectory(nodesDir);
			mediaDir = Path.Combine(programDataDir, MEDIA_FOLDER);
			Directory.CreateDirectory(mediaDir);
			settingPath = Path.Combine(programDataDir, SETTING_FILE);
			if (!File.Exists(settingPath))
			{
				_setting = new JObject();
			}
			else
			{
				LoadSetting();
			}
			/*
			if (!File.Exists(cachePath))
			{
				_cache = new JObject();
				SaveCache();
			}
			else
			{
				LoadCache();
			}
			*/
			#region maintain missing keys
			MaintainSetting();
			SaveSetting();
            /*
			MaintainCache();
			SaveCache();
			*/
            #endregion
            foreach (JProperty property in _setting[NODES].ToObject<JObject>().Properties())
            {
                _nodeIDs.Add(property.Name);
            }
            Node.nodesDir = nodesDir;
			Node.mediaDir = mediaDir;
		}
		public void FirstLoadNodes()
		{
			List<Thread> threadList = new List<Thread>();
			foreach (string id in _nodeIDs)
			{
				Node node;
				Thread thread;
				thread = new Thread(() => { 
					node = new Node(id); 
					lock (NodesLock)
					{
						_nodes[id] = node;
					}
				});
				threadList.Add(thread);
				thread.Start();
			}
			foreach (Thread thread in threadList)
			{
				thread.Join();
			}
		}
		public void SaveNodes()
		{
			List<Thread> threadList = new List<Thread>();
			foreach (string id in _nodeIDs)
			{
				Thread thread;
				thread = new Thread(_nodes[id].Save);
				threadList.Add(thread);
				thread.Start();
				thread = new Thread(() => 
				{
					lock (SettingLock)
					{
						int freq = _setting[NODES][id][MODIFIED].Value<int>();

                        if (freq < 0)
						{
							freq = 1;
						}
						else
						{
							freq += 1;
						}
						_setting[NODES][id][MODIFIED] = freq.ToString();
                    }
				});
				threadList.Add(thread);
				thread.Start();
			}
			foreach (Thread thread in threadList)
			{
				thread.Join();
			}
		}
		public Node FetchNode(string id)
		{
			if (!_nodeIDs.Contains(id))
			{
				Node node = new Node(id);
				_nodeIDs.Add(id);
				_nodes[id] = node;
			}
            return _nodes[id];
        }
		#region File Operation
		public void MaintainSetting()
		{
            
            lock (SettingLock)
			{
                if (!_setting.ContainsKey(NODES))
				{
					_setting[NODES] = new JObject();
				}
                JObject nodeObj = _setting[NODES].ToObject<JObject>();
                foreach (string id in _nodeIDs)
				{
					JObject idObj = nodeObj[id].ToObject<JObject>();
                    if (!idObj.ContainsKey(MODIFIED))
					{
						idObj[MODIFIED] = new JObject();
					}
				}
            }
		}
		/*
		public void MaintainCache()
		{
			if (!_cache.ContainsKey(VOLATILE))
			{
				_cache[VOLATILE] = new JObject();
			}
			if (!_cache[VOLATILE].ToObject<JObject>().ContainsKey(NODES))
			{
				_cache[VOLATILE][NODES] = new JObject();
			}
			if (!_cache.ContainsKey(INVOLATILE))
			{
				_cache[INVOLATILE] = new JObject();
			}
			if (!_cache[INVOLATILE].ToObject<JObject>().ContainsKey(PATH))
			{
				_cache[INVOLATILE][PATH] = new JObject();
			}
			if (!_cache[INVOLATILE].ToObject<JObject>().ContainsKey(MODIFIED))
			{
				_cache[INVOLATILE][MODIFIED] = new JObject();
			}
		}
		
		public Node GetNodeFromCache(string id)
		{
			if (_cache[VOLATILE][NODES].ToObject<JObject>().ContainsKey(id))
			{
				int modified_times = _cache[INVOLATILE][MODIFIED][id].Value<int>();
				if (modified_times > 0)
				{
					_cache[INVOLATILE][MODIFIED][id] = JToken.FromObject(modified_times + 1);
				}
				else
				{
					_cache[INVOLATILE][MODIFIED][id] = JToken.FromObject(1);
				}
				return _cache[VOLATILE][NODES][id].ToObject<Node>();
			}
			else
			{
				Node node = new Node(id);

				return node;
			}
		}
		*/
		public void LoadSetting()
		{
			lock (SettingLock)
			{
				_setting = JsonPKG.ReadJsonObjectFromFile(settingPath);
			}
		}
		public void SaveSetting()
		{
			lock (SettingLock)
			{
				JsonPKG.SaveJsonObjectToFile(_setting, settingPath);
			}
		}
		/*
		public void LoadCache()
		{
			_cache = JsonPKG.ReadJsonObjectFromFile(cachePath);
		}
		public void SaveCache()
		{
			JsonPKG.SaveJsonObjectToFile(_cache, cachePath);
		}
		*/
		#endregion
	}
}
