using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GooglePlayGames;
using Firebase.Database;
using UnityEngine;

public class UserDataStorage : MonoBehaviour
{
    [Serializable]
    public struct SaveData
    {
        public string username;
        public string id;
        public int credits;
        public int topScore;
        public int avatarIndex;
        public List<int> gunIndex;
    }

    private DatabaseReference _databaseReference;

    private void Awake()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveUsername(string username)
    {
        string userId = PlayGamesPlatform.Instance.localUser.id;
        _databaseReference.Child("users").Child(userId).Child("username").SetValueAsync(username);
    }

    public async Task SaveGame(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        await _databaseReference.Child("users").Child(saveData.id).SetRawJsonValueAsync(json);
    }

    public async Task<SaveData> LoadGame(string userId)
    {
        var dataSnapshot = await _databaseReference.Child("users").Child(userId).GetValueAsync();
        if (dataSnapshot.Exists)
        {
            string json = dataSnapshot.GetRawJsonValue();
            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);
            return loadedData;
        }
        else
        {
            return default(SaveData);
        }
    }
}
