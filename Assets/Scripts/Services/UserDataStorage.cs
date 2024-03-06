using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase.Database;
using UnityEngine;
using UnityEngine.Networking;

public class UserDataStorage : MonoBehaviour
{
    [Serializable]
    public struct SaveData
    {
        public string username;
        public string id;
        public int credits;
        public string profileImage;
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

    public async Task SaveGame(SaveData saveData, Texture2D profileImage = null)
    {
        if (profileImage != null)
        {
            saveData.profileImage = await UploadImageToFirebase(saveData.id, profileImage);
        }
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
            Debug.LogError("No data exists for the given user ID");
            return default(SaveData);
        }
    }

    public IEnumerator LoadProfileImage(string imageUrl, Action<Texture2D> callback)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load image: " + www.error);
                callback(null);
            }
            else
            {
                callback(((DownloadHandlerTexture)www.downloadHandler).texture);
            }
        }
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate((result) =>
        {
            switch (result)
            {
                case SignInStatus.Success:
                    Debug.Log("Successfully signed in");
                    break;
                default:
                    Debug.LogError("Failed to sign in");
                    break;
            }
        });
    }

    private async Task<string> UploadImageToFirebase(string userId, Texture2D texture)
    {
        var storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        var storageRef = storage.GetReferenceFromUrl("https://galactic-gunner-default-rtdb.firebaseio.com/");
        var imageRef = storageRef.Child("images").Child(userId);

        byte[] imageData = texture.EncodeToPNG();
        var task = imageRef.PutBytesAsync(imageData);
        await task;

        if (task.IsFaulted)
        {
            Debug.LogError("Failed to upload image");
            return null;
        }
        else
        {
            Debug.Log("Image uploaded");
            return (await imageRef.GetDownloadUrlAsync()).ToString();
        }
    }
}
