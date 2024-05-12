using static ChunksPoolBase;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ChunksPoolFactory : IChunksPoolFactory
{
    private readonly Transform _parent;

    public ChunksPoolFactory(Transform parent)
    {
        _parent = parent;
    }

    public ChunksPoolBase Create(List<ChunkItem> chunkItems)
    {
        GameObject chunkPoolObject = new GameObject();
        chunkPoolObject.transform.SetParent(_parent);

        switch (chunkItems[0].Biome)
        {
            case Biome.White:
                chunkPoolObject.name = "WhiteChunkPool";
                WhiteChunksPool whiteChunkPool = chunkPoolObject.AddComponent<WhiteChunksPool>();
                whiteChunkPool.Initialize(chunkItems);
                return whiteChunkPool;
            case Biome.Black:
                chunkPoolObject.name = "BlackChunkPool";
                BlackChunksPool blackChunkPool = chunkPoolObject.AddComponent<BlackChunksPool>();
                blackChunkPool.Initialize(chunkItems);
                return blackChunkPool;
            case Biome.Blue:
                chunkPoolObject.name = "BlueChunkPool";
                BlueChunksPool blueChunkPool = chunkPoolObject.AddComponent<BlueChunksPool>();
                blueChunkPool.Initialize(chunkItems);
                return blueChunkPool;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

}
