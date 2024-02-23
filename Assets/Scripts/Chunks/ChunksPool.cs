using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunksPool : MonoBehaviour
{

    [SerializeField] private ChunkItem[] _chunksWhiteLocation;
    [SerializeField] private ChunkItem[] _chunksBlackLocation;
    [SerializeField] private ChunkItem[] _chunksBlueLocation;

    private List<Chunk> _chunksWhiteLocationList;
    private List<Chunk> _chunksBlackLocationList;
    private List<Chunk> _chunksBlueLocationList;

    private void Awake()
    {
        _chunksWhiteLocationList = new List<Chunk>();

        foreach (var chunkItem in _chunksWhiteLocation)
        {
            for (int i = 0; i < chunkItem.Count; i++)
            {
                Chunk instantiatedChunk = Instantiate(chunkItem.Chunk, transform);
                instantiatedChunk.gameObject.SetActive(false);
                _chunksWhiteLocationList.Add(instantiatedChunk);
            }
        }

        _chunksBlackLocationList = new List<Chunk>();

        foreach (var chunkItem in _chunksBlackLocation)
        {
            for (int i = 0; i < chunkItem.Count; i++)
            {
                Chunk instantiatedChunk = Instantiate(chunkItem.Chunk, transform);
                instantiatedChunk.gameObject.SetActive(false);
                _chunksBlackLocationList.Add(instantiatedChunk);
            }
        }

        _chunksBlueLocationList = new List<Chunk>();

        foreach (var chunkItem in _chunksBlueLocation)
        {
            for (int i = 0; i < chunkItem.Count; i++)
            {
                Chunk instantiatedChunk = Instantiate(chunkItem.Chunk, transform);
                instantiatedChunk.gameObject.SetActive(false);
                _chunksBlueLocationList.Add(instantiatedChunk);
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
                    chunk = GetStandartChunk(_chunksWhiteLocationList);
                    break;
                case 2:
                    chunk = GetStandartChunk(_chunksBlackLocationList);
                    break;
                case 3:
                    chunk = GetStandartChunk(_chunksBlueLocationList);
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
                    chunk = GetChunk(_chunksWhiteLocationList);
                    break;
                case 2:
                    chunk = GetChunk(_chunksBlackLocationList);
                    break;
                case 3:
                    chunk = GetChunk(_chunksBlueLocationList);
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
