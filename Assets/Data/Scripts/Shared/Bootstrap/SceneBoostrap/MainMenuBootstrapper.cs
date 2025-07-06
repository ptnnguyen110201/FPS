

using Cysharp.Threading.Tasks;


public class MainMenuBootstrapper : IBootstrapper
{

    public async UniTask Initialize()
    {
        DIContainer container = new DIContainer();
        GameContext.Instance.SetDIContainer(container);


        await UniTask.Yield();
    }

}

