using UnityEngine;
public class CharacterShootHandler
{
    protected CharacterAnimationSetting CharacterAnimationSetting;
    protected CharacterAnimatorCore CharacterAnimatorCore;

    public CharacterShootHandler(CharacterAnimatorCore CharacterAnimatorCore, CharacterAnimationSetting CharacterSetting)
    {
        this.CharacterAnimatorCore = CharacterAnimatorCore;
        this.CharacterAnimationSetting = CharacterSetting;
    }

    public void PlayShoot(string StateName)
    {
        if (this.CharacterAnimatorCore.isReloading) return;
        Animator Animator = this.CharacterAnimatorCore.CharacterAnimator;
        int layer = Animator.GetLayerIndex(this.CharacterAnimationSetting.LayerOverLay);
        Animator.Play(StateName, layer, 0f);
    }
}
