using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IEffectSpawner : ISpawner<EffectCtrl>
{
   UniTask<EffectCtrl> SpawnEffect(string PrefabName, Vector3 Pos, Quaternion Rot);
}