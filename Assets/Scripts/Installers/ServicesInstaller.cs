using Zenject;

public class ServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Achievements>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<Analytics>().FromNew().AsSingle();
    }
}
