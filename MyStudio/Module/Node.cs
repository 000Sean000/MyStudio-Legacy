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
        public const string ID = "id";
        public const string NAME = "name";
        public const string TITLE = "title";
        public const string SUMMARY = "summary";
        public const string DESCRIPTION = "description";
        
        public static List<string> jsonKeys = new List<string>()
        {
            ID, NAME, TITLE, SUMMARY, DESCRIPTION
        };
        protected JObject _info;
        protected string _fileName;
        protected string _filePath;
        public static string nodeDir;
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
            foreach (string key in jsonKeys)
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

            save();
        }
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
    }
}
