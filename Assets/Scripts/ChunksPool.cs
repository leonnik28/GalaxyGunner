using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunksPool : MonoBehaviour
{

    [SerializeField] private ChunkItem[] _chunksCommon;
    [SerializeField] private ChunkItem[] _chunksSpecial;
    [SerializeField] private ChunkItem[] _chunksSpecial2;

    private List<Chunk> _chunksCommonList;
    private List<Chunk> _chunksSpecialList;
    private List<Chunk> _chunksSpecialList2;
    private List<Chunk> _chunksSpecialList3;
    private List<Chunk> _chunksSpecialList4;

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

        _chunksSpecialList2 = new List<Chunk>();

        foreach (var chunkItem in _chunksSpecial2)
        {
            for (int i = 0; i < chunkItem.Count; i++)
            {
                Chunk instantiatedChunk = Instantiate(chunkItem.Chunk, transform);
                instantiatedChunk.gameObject.SetActive(false);
                _chunksSpecialList2.Add(instantiatedChunk);
            }
        }
    }

    public Chunk GetChunk(int numberOfCunksBiom, bool standartChunk)
    {
        Chunk chunk = null;
        if (standartChunk)
        {
            switch (numberOfCunksBiom)
            {
                case 1:
                    chunk = GetStandartChunk(_chunksCommonList);
                    break;
                case 2:
                    chunk = GetStandartChunk(_chunksSpecialList);
                    break;
                case 3:
                    chunk = GetStandartChunk(_chunksSpecialList2);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (numberOfCunksBiom)
            {
                case 1:
                    chunk = GetChunk(_chunksCommonList);
                    break;
                case 2:
                    chunk = GetChunk(_chunksSpecialList);
                    break;
                case 3:
                    chunk = GetChunk(_chunksSpecialList2);
                    break;
                default:
                    break;
            }
        }

        return chunk;
    }

    public Chunk GetChunk(List<Chunk> chunks)
    {
        Chunk chunk = chunks[UnityEngine.Random.Range(0, chunks.Count)];

        if (chunk.gameObject.activeSelf)
        {
            return GetChunk(chunks);
        }

        chunk.gameObject.SetActive(true);

        return chunk;
    }

    public Chunk GetStandartChunk(List<Chunk> chunks)
    {
        Chunk chunk = chunks[UnityEngine.Random.Range(0, chunks.Count)];

        if (chunk.gameObject.activeSelf || chunk.CheckChunkToStartChunk() || chunk.CheckChunkToEndChunk() || chunk.CheckChunkToSpecialChunk())
        {
            return GetStandartChunk(chunks);
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
