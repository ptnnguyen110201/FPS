using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

public class EffectManager : IEffectManager

{
    public IEffectSpawner EffectSpawner { get; private set; }

    public IEffectPrefabLoader PrefabsLoader { get; private set; }



    public EffectManager(IEffectSpawner effectSpawner, IEffectPrefabLoader prefabsLoader)
    {
        this.EffectSpawner = effectSpawner;
        this.PrefabsLoader = prefabsLoader;
    }

    public async UniTask Initialize()
    {
        await this.PrefabsLoader.LoadPrefabs();

        Debug.Log("Effect Manager Initialized");
    }
}