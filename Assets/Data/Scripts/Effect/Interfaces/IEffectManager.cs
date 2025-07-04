using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IEffectManager 
{
    IEffectSpawner EffectSpawner { get; }
    IEffectPrefabLoader PrefabsLoader { get; }
    UniTask Initialize();
}