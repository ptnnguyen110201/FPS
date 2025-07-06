using Cysharp.Threading.Tasks;
using UnityEngine;

public class CharacterEquipHandler 
{
    protected CharacterAnimationSetting CharacterSetting;
    protected CharacterAnimatorCore CharacterAnimatorCore;

    public CharacterEquipHandler(CharacterAnimatorCore CharacterAnimatorCore, CharacterAnimationSetting CharacterSetting)
    {
        this.CharacterAnimatorCore = CharacterAnimatorCore;
        this.CharacterSetting = CharacterSetting;
    }

    public async UniTask<bool> PlayHolster()
    {
        Animator Animator = this.CharacterAnimatorCore.CharacterAnimator;
        Animator.SetBool(this.CharacterSetting.HolsterPara, true);
        int layer = Animator.GetLayerIndex(this.CharacterSetting.LayerHolster);

        await UniTask.WaitUntil(() =>
        {
            if (Animator == null || !Animator.isActiveAndEnabled)
                return true;
            var state = Animator.GetCurrentAnimatorStateInfo(layer);
            return state.IsName(this.CharacterSetting.Holster) && state.normalizedTime >= 1f;
        });

        return true;
    }

    public async UniTask<bool> PlayUnHolster()
    {
        Animator Animator = this.CharacterAnimatorCore.CharacterAnimator;
        Animator.SetBool(this.CharacterSetting.HolsterPara, false);
        int layer = Animator.GetLayerIndex(this.CharacterSetting.LayerHolster);

        await UniTask.WaitUntil(() =>
        {
            if (Animator == null || !Animator.isActiveAndEnabled)
                return true;
            var state = Animator.GetCurrentAnimatorStateInfo(layer);
            
            return state.IsName(this.CharacterSetting.UnHolster) && state.normalizedTime >= 0.5f;
        });

        return true;
    }
    
}
