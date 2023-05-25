using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Infrastructure.SaveLoad
{
    public class JsonToFileStorageService: ISaveLoadService
    {
        private bool isInProgress;

        public void Save(string key, object data, Action<bool> callback = null)
        {
            if (!isInProgress)
            {
               isInProgress = true;
                SaveAsync(key,data,callback);
            }
            else
            {
                 callback?.Invoke(false);
            }
           
        }

        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);
            
            if (File.Exists(path))
            {
                using (var fileStream = new StreamReader(path))
                {
                    var json = fileStream.ReadToEnd();
                    var data = JsonConvert.DeserializeObject<T>(json);
                
                    callback?.Invoke(data);
                }
            }
        }

        public void Delete(string key)
        {
            File.Delete(BuildPath(key));
        }

        private string BuildPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key);
        }

        private async void SaveAsync(string key, object data, Action<bool> callback)
        {
            string path = BuildPath(key);
            string json = JsonConvert.SerializeObject(data);
            using (var fileStream = new StreamWriter(path))
            {
              await  fileStream.WriteAsync(json);
            }
            callback?.Invoke(true);
            isInProgress = false;
        }
    }
}
