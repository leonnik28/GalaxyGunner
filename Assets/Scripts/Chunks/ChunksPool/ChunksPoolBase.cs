using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChunksPoolBase : MonoBehaviour
{
    [Serializable]
    public struct ChunkItem
    {
        public Chunk Chunk;
        public Biome Biome;
        public int Count;
    }

    public enum Biome
    {
        White,
        Black,
        Blue
    };

    protected List<ChunkItem> _chunkItems;
    private List<Chunk> _chunksList;

    public void Initialize(List<ChunkItem> chunkItems)
    {
        _chunkItems = chunkItems;
    }

    public virtual void InstantiateChunks()
    {
        _chunksList = new List<Chunk>();

        foreach (var chunkItem in _chunkItems)
        {
            for (int i = 0; i < chunkItem.Count; i++)
            {
                Chunk instantiatedChunk = Instantiate(chunkItem.Chunk, transform);
                instantiatedChunk.gameObject.SetActive(false);
                _chunksList.Add(instantiatedChunk);
            }
        }
    }

    public virtual Chunk GetChunk()
    {
        Chunk chunk = _chunksList[UnityEngine.Random.Range(0, _chunksList.Count)];

        if (chunk.gameObject.activeSelf)
        {
            return GetChunk();
        }

        chunk.gameObject.SetActive(true);

        return chunk;
    }

    public virtual Chunk GetStandartChunk()
    {
        Chunk chunk = _chunksList[UnityEngine.Random.Range(0, _chunksList.Count)];

        if (chunk.gameObject.activeSelf || chunk.IsEndChunk || chunk.IsSpecialChunk)
        {
            return GetStandartChunk();
        }

        chunk.gameObject.SetActive(true);

        return chunk;
    }
}
