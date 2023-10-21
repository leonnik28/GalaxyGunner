using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunksPool : MonoBehaviour
{
    public int PoolCount => _chunksList.Count;

    [SerializeField] private ChunkItem[] _chunks;

    private List<Chunk> _chunksList;

    private void Awake()
    {
        _chunksList = new List<Chunk>();

        foreach (var chunkItem in _chunks)
        {
            for (int i = 0; i < chunkItem.Count; i++)
            {
                Chunk instantiatedChunk = Instantiate(chunkItem.Chunk, transform);
                instantiatedChunk.gameObject.SetActive(false);
                _chunksList.Add(instantiatedChunk);
            }
        }
    }

    public Chunk GetChunk()
    {
        Chunk chunk = _chunksList[UnityEngine.Random.Range(0, _chunksList.Count)];

        if (chunk.gameObject.activeSelf)
        {
            return GetChunk();
        }

        chunk.gameObject.SetActive(true);

        return chunk;
    }

    public void ReturnChunk(Chunk chunk)
    {
        chunk.gameObject.SetActive(false);
    }

    [Serializable]
    private struct ChunkItem
    {
        public Chunk Chunk;
        public int Count;
    }
}
