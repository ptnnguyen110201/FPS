using Cysharp.Threading.Tasks;

using UnityEngine;


public class EffectSpawner : Spawner<EffectCtrl>, IEffectSpawner
{
    public EffectSpawner(IEffectPrefabLoader prefabLoader) : base(prefabLoader)
    {
    }

    public UniTask<EffectCtrl> SpawnEffect(string prefabName, Vector3 pos, Quaternion rot)
    {
        EffectCtrl newEffect = this.Spawn(prefabName, pos, rot);
        return UniTask.FromResult(newEffect);
    }
}