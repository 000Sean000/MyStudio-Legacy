using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PKG;
using static System.Net.Mime.MediaTypeNames;

namespace Module
{
	public partial class Vault
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
		protected Dictionary<string, bool> _isNodeModified = new Dictionary<string, bool>();
		#endregion

		#region Resource Locks
		public object SettingLock = new object();
		public object NodesLock = new object();
		public object _isNodeModifiedLock = new object();
		#endregion

		#region Json keys
		public const string NODES = "Nodes";
		public const string MEDIA = "Media";
		public const string VOLATILE = "Volatile";
		public const string INVOLATILE = "Involatile";
		public const string PATH = "Node Pathes";
		public const string FREQ = "Modification counter";
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
			#region maintain missing keys
			MaintainSetting();
			SaveSetting();
			#endregion
			foreach (JProperty property in _setting[NODES].ToObject<JObject>().Properties())
			{
				_nodeIDs.Add(property.Name);
			}
			Node.nodesDir = nodesDir;
			Node.mediaDir = mediaDir;
			Node.vault = this;
			FirstLoadNodes();

		}
		#region Node operation

		public void FirstLoadNodes()
		{
			Debug.WriteLine(">>Vault first load nodes");
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
				_isNodeModified[id] = false;
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
				if (_isNodeModified[id])
				{
					thread = new Thread(_nodes[id].Save);
					threadList.Add(thread);
					thread.Start();
					_isNodeModified[id] = false;
					thread = new Thread(() =>
					{
						int freq;
						lock (SettingLock)
						{
							freq = _setting[NODES][id][FREQ].Value<int>();
							if (freq < 0)
							{
								freq = 1;
							}
							else
							{
								freq += 1;
							}
							_setting[NODES][id][FREQ] = freq.ToString();
						}
					});
					threadList.Add(thread);
					thread.Start();
				}
				else
				{
					thread = new Thread(() =>
					{
						int freq;
						lock (SettingLock)
						{
							freq = _setting[NODES][id][FREQ].Value<int>();
							freq--;
							_setting[NODES][id][FREQ] = freq.ToString();
						}
					});
					threadList.Add(thread);
					thread.Start();
				}
			}
			/*
			foreach (Thread thread in threadList)
			{
				thread.Join();
			}
			*/
		}
		public Node CreateNode()
		{
			Node node = new Node();
			string id = node.id;
			_nodeIDs.Add(id);
			lock (NodesLock)
			{
				_nodes[id] = node;
				return _nodes[id];
			}
		}
		public Node FetchNode(string id)
		{
			if (!_nodeIDs.Contains(id))
			{
				Node node = new Node(id);
				_nodeIDs.Add(id);
				lock (NodesLock)
				{
					_nodes[id] = node;
					return _nodes[id];
				}
			}
			lock (NodesLock)
			{
				return _nodes[id];
			}
		}
		#endregion
		
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
					if (!idObj.ContainsKey(FREQ))
					{
						idObj[FREQ] = new JObject();
					}
				}
			}
		}
		
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
		
		#endregion

		
	}
}
