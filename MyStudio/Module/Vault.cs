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
		public const string VAULT_FILE = "VaultSetting.json";
		public const string PROGRAM_FOLDER = "VaultProgramData";
		public const string NODE_FOLDER = "Nodes";
		public string vaultDir;
		public string programDir;
        public string settingFilePath;
		public string nodeDir;
		
        public JObject setting;
		
		public Vault(string directoryPath)
		{
			vaultDir = directoryPath;
			if (!Directory.Exists(directoryPath))
			{
				throw new Exception("Vault not exist!");
			}
			else
			{
				
				programDir = Path.Combine(vaultDir, PROGRAM_FOLDER);
				Directory.CreateDirectory(programDir);
				nodeDir = Path.Combine(programDir, NODE_FOLDER);
				Directory.CreateDirectory(nodeDir);
				settingFilePath = Path.Combine(programDir, VAULT_FILE);;
				if (!File.Exists(settingFilePath))
				{
					setting = new JObject();
					saveSetting();
					setting[NODE_FOLDER] = new JObject();
				}
				loadSetting();

			}
		}
		

		public void loadSetting()
		{
			setting = PKG.JsonPKG.ReadJsonObjectFromFile(settingFilePath);
		}
		public void saveSetting()
		{
			PKG.JsonPKG.SaveJsonObjectToFile(setting, settingFilePath);
		}

	}
}
