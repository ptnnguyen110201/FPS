using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

public class EffectInstaller : GameSystemInstaller
{
    public override async UniTask Install(DIContainer container)
    {

        container.Bind<IEffectPrefabLoader, EffectPrefabLoader>();
        container.Bind<IEffectSpawner, EffectSpawner>();
        container.Bind<IEffectManager, EffectManager>();


        await container.Resolve<IEffectManager>().Initialize();
    }
}