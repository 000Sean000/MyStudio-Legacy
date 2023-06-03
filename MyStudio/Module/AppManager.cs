using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PKG;
///<summary>
/// App Localization
/// </summary>
/// developing: 
/// - only install localization folder in local disk
/// - all put at cloud drive



namespace Module
{
    public static class AppManager
    {
        public const string DEFAULT = "Default";
        public const string LOCAL_FOLDER = "AppLocalization"; 
        public const string SETTING_FILE = "AppLocalSetting.json";
        public const string VAULT_ADDR = "Vault locations";

        public static JObject setting;

        public static string rootDir;
        public static string localDir;
        public static string settingFilePath;
        public static void init()
        {
            rootDir = PKG.pathPKG.getDir(2);
            localDir = Path.Combine(rootDir, LOCAL_FOLDER);
            Directory.CreateDirectory(localDir);
            settingFilePath = Path.Combine(localDir, SETTING_FILE);

            if (!File.Exists(settingFilePath)) 
            {
                setting = new JObject();    
                saveSetting();
            }
            loadSetting();
            
            
            if (!setting.ContainsKey(VAULT_ADDR))
            {
                setting[VAULT_ADDR] = new JObject();
                saveSetting();
            }
            createVault();
            listValidVault();
        }
        public static void createVault()
        {
            language lang_selectVault = new language("Select a folder to be your Vault.");
            string? vaultPath = PKG.pathPKG.selectDir(lang_selectVault.show());
            if (vaultPath != null )
            {
                setting[VAULT_ADDR][vaultPath] = vaultPath;
                saveSetting();
                Module.Vault vault = new Vault(vaultPath);
            }
        }
        public static List<string>? listValidVault()
        {
            JObject vaultPathes = setting[VAULT_ADDR].Value<JObject>();
            string vaultPath;
            List<string> vaultList = new List<string>();
            foreach (JProperty property in setting[VAULT_ADDR].Value<JObject>().Properties())
            {
                vaultPath = property.Value.ToString();
                Logger.WriteLine("vault: "+vaultPath);
                if (Directory.Exists(vaultPath)) 
                {
                    vaultList.Add(vaultPath);
                    Logger.WriteLine("Valid:" + vaultPath);
                }
            }
            foreach (string path in vaultList)
            {
                Logger.WriteLine("Valid Vault:"+path);
            }
            return vaultList;
        }
        public static void loadSetting()
        {
            setting = PKG.JsonPKG.ReadJsonObjectFromFile(settingFilePath);
        }
        public static void saveSetting() 
        {
            PKG.JsonPKG.SaveJsonObjectToFile(setting, settingFilePath);
        }
        

    }
}
