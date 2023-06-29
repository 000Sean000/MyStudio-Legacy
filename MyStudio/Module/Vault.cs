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
		protected JObject _cache;
		#endregion

		public object cacheLock = new object();
		public object settingLock = new object();

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
			else
			{
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
                    SaveSetting();   
                }
				else
				{
					LoadSetting();
				}
				if (!File.Exists(cachePath))
				{
					_cache = new JObject();
					SaveCache();
				}
				else
				{
					LoadCache();
				}
                #region maintain missing keys
                MaintainSetting();
                SaveSetting();
                MaintainCache();
				SaveCache();
                #endregion
                Node.nodesDir = nodesDir;
				Node.mediaDir = mediaDir;
			}
		}
		public void MaintainSetting()
		{
            if (!_setting.ContainsKey(NODE_FOLDER))
            {
                _setting[NODE_FOLDER] = new JArray() ;
            }
            if (!_setting.ContainsKey(MEDIA_FOLDER))
            {
                _setting[MEDIA_FOLDER] = new JArray();
            }
        }
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
				
			}
		}

        #region File Operation
        public void LoadSetting()
		{
			_setting = JsonPKG.ReadJsonObjectFromFile(settingPath);
		}
		public void SaveSetting()
		{
			JsonPKG.SaveJsonObjectToFile(_setting, settingPath);
		}
		public void LoadCache()
		{
			_cache = JsonPKG.ReadJsonObjectFromFile(cachePath);
		}
		public void SaveCache()
		{
			JsonPKG.SaveJsonObjectToFile(_cache, cachePath);
		}
        #endregion
    }
}
