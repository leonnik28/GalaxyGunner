using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunksPool : MonoBehaviour
{
    public int PoolCount => _chunksCommonList.Count;

    [SerializeField] private ChunkItem[] _chunksCommon;
    [SerializeField] private ChunkItem[] _chunksSpecial;

    private List<Chunk> _chunksCommonList;
    private List<Chunk> _chunksSpecialList;

    private void Awake()
    {
        _chunksCommonList = new List<Chunk>();

        foreach (var chunkItem in _chunksCommon)
        {
            for (int i = 0; i < chunkItem.Count; i++)
            {
                Chunk instantiatedChunk = Instantiate(chunkItem.Chunk, transform);
                instantiatedChunk.gameObject.SetActive(false);
                _chunksCommonList.Add(instantiatedChunk);
            }
        }

        _chunksSpecialList = new List<Chunk>();

        foreach (var chunkItem in _chunksSpecial)
        {
            for (int i = 0; i < chunkItem.Count; i++)
            {
                Chunk instantiatedChunk = Instantiate(chunkItem.Chunk, transform);
                instantiatedChunk.gameObject.SetActive(false);
                _chunksSpecialList.Add(instantiatedChunk);
            }
        }
    }

    public Chunk GetChunk()
    {
        Chunk chunk = _chunksCommonList[UnityEngine.Random.Range(0, _chunksCommonList.Count)];

        if (chunk.gameObject.activeSelf)
        {
            return GetChunk();
        }

        chunk.gameObject.SetActive(true);

        return chunk;
    }

    public Chunk GetSpecialChunk()
    {
        Chunk chunk = _chunksSpecialList[UnityEngine.Random.Range(0, _chunksSpecialList.Count)];

        if (chunk.gameObject.activeSelf)
        {
            return GetSpecialChunk();
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
