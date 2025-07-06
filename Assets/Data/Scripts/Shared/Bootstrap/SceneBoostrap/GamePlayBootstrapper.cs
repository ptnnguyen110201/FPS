using Cysharp.Threading.Tasks;
using UnityEngine;

public class GamePlayBootstrapper : IBootstrapper
{
    public UniTask Initialize()
    {
        DIContainer container = new DIContainer();
        GameContext.Instance.SetDIContainer(container);
       
        InputProviderInstaller InputProviderInstaller = new InputProviderInstaller();
        InputProviderInstaller.Install(container).Forget();

        CharacterInstaller CharacterInstaller = new CharacterInstaller();
        CharacterInstaller.Install(container).Forget();

        CameraInstaller CameraInstaller = new CameraInstaller();
        CameraInstaller.Install(container).Forget();

        EffectInstaller EffectInstaller = new EffectInstaller();
        EffectInstaller.Install(container).Forget();

        EnemyManagerInstaller enemyManagerInstaller = new EnemyManagerInstaller();
        enemyManagerInstaller.Install(container).Forget();

        UIInstaller UIInstaller = new UIInstaller();
        UIInstaller.Install(container).Forget();


        return UniTask.CompletedTask;
    }


 
}
