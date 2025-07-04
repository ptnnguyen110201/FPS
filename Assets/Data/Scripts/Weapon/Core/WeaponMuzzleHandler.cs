using Cysharp.Threading.Tasks;

using UnityEngine;

public class WeaponMuzzleHandler : LoadComponents
{
    [Inject] protected IEffectManager EffectManager;

    protected void Start()
    {
         GameContext.Instance.InjectInto(this);
    }
   
    

    public async UniTask SpawnMuzzle(string MuzzleName) 
    {
        Vector3 Pos = this.transform.position;
        Quaternion Rot = Quaternion.LookRotation(-this.transform.up);
        EffectCtrl newEffect = await this.EffectManager.EffectSpawner.SpawnEffect(MuzzleName, Pos, Rot);
        newEffect.transform.SetParent(this.transform);
      
        
    }

}