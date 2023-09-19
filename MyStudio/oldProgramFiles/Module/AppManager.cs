using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PKG;
using Ui;
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
		private static List<string> _VaultList { get { return _setting[deviceName][VAULT_ADDR].ToObject<List<string>>(); } }
		//private static JArray _vaultJArray = new JArray(_vaultList);
		public static Vault workingVault;
		public static AppMenu appMenu;
		static AppManager()
		{
			init();
		}
		public static void init()
		{

			//rootDir = PKG.pathPKG.GetDirWithBackstep(backStep);
			rootDir = pathPKG.GetMainDir();
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
			MaintainSetting();
			SaveSetting();
			#endregion
		}
		#region Vault Operation
		public static Vault? CreateVault()
		{
			language lang_selectVault = new language("Select a folder to be your Vault.");
			string? vaultPath = PKG.pathPKG.SelectDirectoryByDialog(lang_selectVault.Get());
			if (vaultPath != null)
			{
				List<string> _vaultList = _setting[deviceName][VAULT_ADDR].ToObject<List<string>>();
				if (!_vaultList.Contains(vaultPath))
				{
					_vaultList.Add(vaultPath);
				}
				_setting[deviceName][VAULT_ADDR] = JToken.FromObject(_vaultList);
				SaveSetting();
				Vault vault = new Vault(vaultPath); // Vault initialization
				return vault;
			}
			return null;
		}
		public static List<string> ListVaultPathes()
		{
			return _setting[deviceName][VAULT_ADDR].ToObject<List<string>>();
		}
		public static Vault OpenVault(string vaultPath)
		{
			Debug.WriteLine($"Open Vault: {vaultPath}");
			workingVault = new Vault(vaultPath);
			return workingVault;
		}
		public static void RemoveVault(string vaultPath)
		{
			List<string> _vaultList = _VaultList;
			_vaultList.Remove(vaultPath);
			_setting[deviceName][VAULT_ADDR] = JToken.FromObject(_vaultList);
			SaveSetting();
		}
		#endregion
		
		#region File Operation
		public static void LoadSetting()
		{
			_setting = PKG.JsonPKG.ReadJsonObjectFromFile(settingPath);
		}
		public static void SaveSetting()
		{
			PKG.JsonPKG.SaveJsonObjectToFile(_setting, settingPath);
		}
		public static void MaintainSetting()
		{
			if (!_setting.ContainsKey(deviceName))
			{
				_setting[deviceName] = new JObject();
			}
			if (!_setting[deviceName].ToObject<JObject>().ContainsKey(VAULT_ADDR))
			{
				_setting[deviceName][VAULT_ADDR] = JToken.FromObject(new List<string>());
			}
			else
			{
				List<string> _vaultList = _VaultList;
				// remove invalid vault path
				foreach (string vaultPath in _vaultList)
				{
					if (!Directory.Exists(vaultPath))
					{
						_vaultList.Remove(vaultPath);
					}
				}
				_setting[deviceName][VAULT_ADDR] = JToken.FromObject(_vaultList);
			}
			if (!_setting[deviceName].ToObject<JObject>().ContainsKey(PREFERENCE))
			{
				_setting[deviceName][PREFERENCE] = new JObject();

			}
			//...
		}
		#endregion


	}
}
