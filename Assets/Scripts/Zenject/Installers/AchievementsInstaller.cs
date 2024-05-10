using Zenject;

public class AchievementsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Achievements>().FromNew().AsSingle();
    }
}
