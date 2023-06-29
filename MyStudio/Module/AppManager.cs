using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PKG;
///<summary>
/// App in Local disk and cloud drive (now can all put in cloud drive)
/// </summary>
/// Developing: 
/// - only put program setting file in local disk
/// Release Version:
/// - install all program file in local disk
namespace Module
{
	public static class AppManager
	{
		public const string SETTING_FILE = "AppSetting.json";
		public const string PROGRAM_DATA_FOLDER = "ProgramData";

		#region setting json keys
		public const string VAULT_ADDR = "Vault location directories";
		public const string PREFERENCE = "Preference setting";
		public const string LANGUAGE = "Language setting";
		public const string THEME = "Theme setting";
        #endregion

        public static string deviceName;
        public static string rootDir;
        public static string programDataDir;
        public static string settingPath;
        private static JObject _setting;
		private static List<string> _vaultList = new List<string>();
		//private static JArray _vaultJArray = new JArray(_vaultList);
        public static void LoadSetting()
        {
            _setting = PKG.JsonPKG.ReadJsonObjectFromFile(settingPath);
        }
        public static void SaveSetting()
        {
            PKG.JsonPKG.SaveJsonObjectToFile(_setting, settingPath);
        }
        static AppManager()
        {
			rootDir = PKG.pathPKG.GetDirWithBackstep(2);
			programDataDir = Path.Combine(rootDir, PROGRAM_DATA_FOLDER);
			Directory.CreateDirectory(programDataDir);
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
			deviceName = System.Environment.MachineName;
			#region initialize or repair keys
			if (!_setting.ContainsKey(deviceName))
			{
				_setting[deviceName] = new JObject();
			}
			if (!_setting[deviceName].ToObject<JObject>().ContainsKey(VAULT_ADDR))
			{
				_setting[deviceName][VAULT_ADDR] = JToken.FromObject(_vaultList);
			}
			else
			{
				_vaultList = _setting[deviceName][VAULT_ADDR].ToObject<List<string>>();
			}
            if (!_setting[deviceName].ToObject<JObject>().ContainsKey(PREFERENCE))
            {
                _setting[deviceName][PREFERENCE] = new JObject();

            }
            SaveSetting();
			#endregion
		}
		public static void CreateVault()
		{
            language lang_selectVault = new language("Select a folder to be your Vault.");
            string? vaultPath = PKG.pathPKG.SelectDirectoryByDialog(lang_selectVault.Get());
            if (vaultPath != null)
            {
				_vaultList.Add(vaultPath);
                _setting[deviceName][VAULT_ADDR] = JToken.FromObject(_vaultList);
                SaveSetting();
                Vault vault = new Vault(vaultPath); // Vault initialization
            }
        }
		public static void RemoveVault(string vaultPath)
		{
			_vaultList.Remove(vaultPath);
            _setting[deviceName][VAULT_ADDR] = JToken.FromObject(_vaultList);
            SaveSetting();
        }
    }
}
