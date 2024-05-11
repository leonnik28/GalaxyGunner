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
        InstallChunkPoolBindings();
        InstallChunksPoolBinding();
    }

    private void InstallChunkPoolBindings()
    {
        ChunksPoolFactory chunksPoolFactory = Container.Instantiate<ChunksPoolFactory>(new object[] { _chunksPoolTransform });

        ChunksPoolBase chunksPoolBase = chunksPoolFactory.Create(_whiteChunkItems);
        Container.Bind<ChunksPoolBase>().WithId("White").FromInstance(chunksPoolBase);

        ChunksPoolBase chunksPoolBase1 = chunksPoolFactory.Create(_blackChunkItems);
        Container.Bind<ChunksPoolBase>().WithId("Black").FromInstance(chunksPoolBase1);

        ChunksPoolBase chunksPoolBase2 = chunksPoolFactory.Create(_blueChunkItems);
        Container.Bind<ChunksPoolBase>().WithId("Blue").FromInstance(chunksPoolBase2);
    }

    private void InstallChunksPoolBinding()
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
