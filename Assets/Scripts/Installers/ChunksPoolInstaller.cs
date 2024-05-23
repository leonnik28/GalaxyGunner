using static ChunksPoolBase;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChunksPoolInstaller : MonoInstaller
{
    [SerializeField] private Transform _chunksPoolTransform;

    [SerializeField] private List<ChunkItem> _whiteChunkItems;
    [SerializeField] private List<ChunkItem> _blackChunkItems;
    [SerializeField] private List<ChunkItem> _blueChunkItems;

    public override void InstallBindings()
    {
        InstallChunksPoolFactories();
        InstallChunksPoolManager();
    }

    private void InstallChunksPoolFactories()
    {
        ChunksPoolFactory chunksPoolFactory = Container.Instantiate<ChunksPoolFactory>(new object[] { _chunksPoolTransform });

        ChunksPoolBase whiteChunksPoolBase = chunksPoolFactory.Create(_whiteChunkItems);
        Container.Bind<ChunksPoolBase>().WithId("White").FromInstance(whiteChunksPoolBase);

        ChunksPoolBase blackChunksBase = chunksPoolFactory.Create(_blackChunkItems);
        Container.Bind<ChunksPoolBase>().WithId("Black").FromInstance(blackChunksBase);

        ChunksPoolBase blueChunksBase = chunksPoolFactory.Create(_blueChunkItems);
        Container.Bind<ChunksPoolBase>().WithId("Blue").FromInstance(blueChunksBase);
    }

    private void InstallChunksPoolManager()
    {
        List<ChunksPoolBase> chunksPools = new List<ChunksPoolBase>
        {
            Container.ResolveId<ChunksPoolBase>("White"),
            Container.ResolveId<ChunksPoolBase>("Black"),
            Container.ResolveId<ChunksPoolBase>("Blue")
        };

        Container.BindInterfacesAndSelfTo<ChunksPool>().AsSingle().WithArguments(chunksPools);
    }
}
