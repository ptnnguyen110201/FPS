using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class CharacterReloadHandler
{
    protected CharacterAnimationSetting CharacterSetting;
    protected CharacterAnimatorCore CharacterAnimatorCore;
    public CharacterReloadHandler(CharacterAnimatorCore CharacterAnimatorCore, CharacterAnimationSetting CharacterSetting)
    {
        this.CharacterAnimatorCore = CharacterAnimatorCore;
        this.CharacterSetting = CharacterSetting;
    }

    public async UniTask PlayReload(string ReloadType, CancellationToken token)
    {
        if (this.CharacterAnimatorCore.isReloading) return;
        Animator animator = this.CharacterAnimatorCore.CharacterAnimator;
        int layer = animator.GetLayerIndex(this.CharacterSetting.LayerAction);

        if (animator == null || !animator.isActiveAndEnabled)
            return;

        animator.Play(ReloadType, layer, 0f);

        try
        {
            await UniTask.WaitUntil(() =>
            {
                if (animator == null || !animator.isActiveAndEnabled)
                    return true;

                var state = animator.GetCurrentAnimatorStateInfo(layer);
                return state.IsName(ReloadType) && state.normalizedTime >= 0.95f;
            }, cancellationToken: token);
        }
        catch (OperationCanceledException)
        {

           
            
        }
    }

}
