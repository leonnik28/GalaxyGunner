using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksPool : MonoBehaviour
{
    [SerializeField] private ChunkItem[] _chunks;

    private List<Chunk> _chunksList;

    public Chunk GetChunk()
    {
        Chunk chunk = _chunksList[UnityEngine.Random.Range(0, _chunks.Length)];
        chunk.gameObject.SetActive(true);
        return chunk;
    }

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

    [Serializable]
    private struct ChunkItem
    {
        public Chunk Chunk;
        public int Count;
    }
}
