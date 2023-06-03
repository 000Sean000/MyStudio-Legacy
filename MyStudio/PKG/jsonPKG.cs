using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace PKG
{
    public static class JsonPKG
    {
        

        

        // 将 JSON 对象保存到 JSON 文件
        public static void SaveJsonObjectToFile(JObject jsonObject, string filePath)
        {
            string jsonString = jsonObject.ToString();
            File.WriteAllText(filePath, jsonString);
            Logger.WriteLine("JSON object has been save to: " + filePath);
        }

        // 从 JSON 文件读取为 JSON 对象
        public static JObject ReadJsonObjectFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                try
                {
                    return JObject.Parse(jsonString);
                }
                catch 
                { 
                    throw new Exception("Parsing Error: " + filePath); 
                }
            }
            else
            {
                throw new FileNotFoundException("JSON file not exist");
            }
        }

        // 修改 JSON 对象（插入和删除操作）
        public static void demoModifyJsonObject(JObject jsonObject)
        {
            // 插入新属性
            jsonObject["email"] = "john@example.com";

            // 删除属性
            jsonObject.Remove("age");
        }
        public static void demoJson_(string filePath = "data.json")
        {
            // 创建 JSON 对象
            JObject jsonObject = new JObject();
            jsonObject["name"] = "John";
            jsonObject["age"] = 30;

            // 保存对象到 JSON 文件
            //string filePath = "data.json";
            SaveJsonObjectToFile(jsonObject, filePath);

            // 从 JSON 文件读取为 JSON 对象
            JObject readJsonObject = ReadJsonObjectFromFile(filePath);
            Logger.WriteLine("读取的 JSON 对象:");
            Logger.WriteLine(readJsonObject);

            // 修改 JSON 对象（插入和删除操作）
            demoModifyJsonObject(readJsonObject);
            Logger.WriteLine("修改后的 JSON 对象:");
            Logger.WriteLine(readJsonObject);
        }

    }
}

