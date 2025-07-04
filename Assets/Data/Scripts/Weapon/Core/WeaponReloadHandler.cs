using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class WeaponReloadHandler
{
    protected CharacterCtrl CharacterCtrl;
    protected WeaponData WeaponData;
    protected Animator Animator;

    public WeaponReloadHandler(CharacterCtrl CharacterCtrl, WeaponData WeaponData, Animator Animator)
    {
        this.CharacterCtrl = CharacterCtrl;
        this.WeaponData = WeaponData;
        this.Animator = Animator;
    }

    public async UniTask<bool> Reload(CancellationToken token)
    {
        this.Animator.Play(this.WeaponData.Reload, 0, 0f);

        await UniTask.WhenAll(
          
            UniTask.WaitUntil(() =>
            {
                var state = this.Animator.GetCurrentAnimatorStateInfo(0);
                return state.IsName(this.WeaponData.Reload) && state.normalizedTime >= 0.95f;
            }, cancellationToken: token)
        );

        return true;
    }
}
