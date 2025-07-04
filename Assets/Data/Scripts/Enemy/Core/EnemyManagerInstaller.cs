using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

[Serializable]
public class EnemyManagerInstaller : GameSystemInstaller
{
    public override async UniTask Install(DIContainer container)
    {
        container.Bind<IEnemyPrefabLoader, EnemyPrefabLoader>();
        container.Bind<IEnemySpawner, EnemySpawner>();
        await container.Resolve<IEnemyPrefabLoader>().LoadPrefabs();

        EnemyManager EnemyManager = GameObject.FindFirstObjectByType<EnemyManager>();

        if (EnemyManager == null)
        {
            GameObject newObj = new GameObject("EnemyManager");
            EnemyManager = newObj.AddComponent<EnemyManager>();
        }

        container.BindInstance<IEnemyManager>(EnemyManager);

     
        Debug.Log("[DI] EnemyManager installed.");

      
    }
}
