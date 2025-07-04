using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
public abstract class EffectCtrl : MonoBehaviour, IPoolable
{
    [Inject] public IEffectManager EffectManager;

    protected abstract float DespawnTime();

    public void OnDespawn() 
    
    {
    }

    public void OnSpawn()
    {
        GameContext.Instance.Container.InjectInto(this);
        this.Despawn().Forget();
    }

    protected async UniTask Despawn()
    {
        float delay = this.DespawnTime();
        await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: this.GetCancellationTokenOnDestroy());
        this.EffectManager.EffectSpawner.Despawn(this);
    }
}
