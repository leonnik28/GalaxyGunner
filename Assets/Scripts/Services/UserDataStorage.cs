using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
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

    public async Task SaveGame(SaveData saveData)
    {
        string filename = $"savedata_{saveData.id}";

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        if (savedGameClient == null)
        {
            return;
        }

        var game = await OpenSavedGame(savedGameClient, filename);
        string json = JsonUtility.ToJson(saveData);

        byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
        if (data == null || data.Length == 0)
        {
            return;
        }

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        await WriteSavedGame(savedGameClient, game, updatedMetadata, data);
    }

    private Task<ISavedGameMetadata> OpenSavedGame(ISavedGameClient savedGameClient, string filename)
    {
        var tcs = new TaskCompletionSource<ISavedGameMetadata>();
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, (status, game) => {
            if (status == SavedGameRequestStatus.Success)
            {
                tcs.SetResult(game);
            }
            else
            {
                tcs.SetResult(null);
            }
        });
        return tcs.Task;
    }

    private Task WriteSavedGame(ISavedGameClient savedGameClient, ISavedGameMetadata game, SavedGameMetadataUpdate updatedMetadata, byte[] data)
    {
        var tcs = new TaskCompletionSource<bool>();
        savedGameClient.CommitUpdate(game, updatedMetadata, data, (status, updatedGame) => {
            if (status == SavedGameRequestStatus.Success)
            {
                tcs.SetResult(true);
            }
            else
            {
                tcs.SetResult(false);
            }
        });
        return tcs.Task;
    }

    public async Task<SaveData> LoadGame(string userId)
    {
        string filename = $"savedata_{userId}";

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        if (savedGameClient == null)
        {
            return default;
        }

        var game = await OpenSavedGame(savedGameClient, filename);
        if (game == null)
        {
            return default;
        }

        var data = await ReadSavedGame(savedGameClient, game);
        if (data == null || data.Length == 0)
        {
            return default;
        }

        string json = System.Text.Encoding.UTF8.GetString(data);
        SaveData loadedData = JsonUtility.FromJson<SaveData>(json);
        return loadedData;
    }


    private Task<byte[]> ReadSavedGame(ISavedGameClient savedGameClient, ISavedGameMetadata game)
    {
        var tcs = new TaskCompletionSource<byte[]>();
        savedGameClient.ReadBinaryData(game, (status, data) => {
            if (status == SavedGameRequestStatus.Success)
            {
                tcs.SetResult(data);
            }
            else
            {
                tcs.SetResult(null);
            }
        });
        return tcs.Task;
    }
}