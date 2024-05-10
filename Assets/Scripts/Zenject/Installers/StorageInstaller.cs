using Zenject;

public class StorageInstaller : MonoInstaller<StorageInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StorageService>().FromNew().AsSingle();
        Container.Bind<UserDataStorage>().FromNew().AsSingle();
    }
}