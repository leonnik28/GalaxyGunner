using UnityEngine;
using Zenject;

public class PlayerDataInstaller : MonoInstaller
{
    [SerializeField] private GameSession _gameSession;
    [SerializeField] private Avatars _avatars;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ProfileData>().FromNew().AsSingle().WithArguments(_gameSession, _avatars);
        Container.BindInterfacesAndSelfTo<Credits>().FromNew().AsSingle().WithArguments(_gameSession);
        Container.BindInterfacesAndSelfTo<TopScore>().FromNew().AsSingle().WithArguments(_gameSession);
    }
}

