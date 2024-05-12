using System.Collections.Generic;
using Zenject;
using static ChunksPoolBase;

public interface IChunksPoolFactory : IFactory
{
    ChunksPoolBase Create(List<ChunkItem> chunkItems);
}