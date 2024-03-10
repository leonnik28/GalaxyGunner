using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.Purchasing.MiniJSON;

public class StorageService : IStorageService
{
    public async Task SaveAsync(string key, object data)
    {
        string path = BuildPath(key);
        string json = JsonConvert.SerializeObject(data);

        using (var fileStream = new StreamWriter(path))
        {
            await fileStream.WriteAsync(json);
        }
    }

    public async Task<T> LoadAsync<T>(string key)
    {
        string path = BuildPath(key);
        if (!File.Exists(path))
        {
            return default(T);
        }

        using (var fileRead = new StreamReader(path))
        {
            var json = await fileRead.ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<T>(json);

            return data;
        }
    }

    private string BuildPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key);
    }
}

