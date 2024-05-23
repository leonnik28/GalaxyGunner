using System.Collections.Generic;
using Zenject;

public class ChunksPool : IInitializable
{
    private readonly List<ChunksPoolBase> _chunkPools;

    private ChunksPool(List<ChunksPoolBase> chunkPools)
    {
        _chunkPools = chunkPools;
    }

    public void Initialize()
    {
        foreach (var chunkPool in _chunkPools)
        {
            chunkPool.InstantiateChunks();
        }
    }

    public Chunk GetChunk(int numberOfCunksBiom, bool standartChunk)
    {
        ChunksPoolBase chunkPool = _chunkPools[numberOfCunksBiom - 1];

        if (standartChunk)
        {
            return chunkPool.GetStandartChunk();
        }
        else
        {
            return chunkPool.GetChunk();
        }
    }
}
