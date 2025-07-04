using Cysharp.Threading.Tasks;

using UnityEngine;


public class EnemySpawner : Spawner<EnemyCtrl>, IEnemySpawner
{
    public EnemySpawner(IEnemyPrefabLoader prefabLoader) : base(prefabLoader)
    {
    }

   
}