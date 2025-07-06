using UnityEngine;
public class CharacterShootHandler
{
    protected CharacterAnimationSetting characterAnimationSetting;
    protected CharacterAnimatorCore characterAnimatorCore;

    public CharacterShootHandler(CharacterAnimatorCore characterAnimatorCore, CharacterAnimationSetting characterAnimationSetting)
    {
        this.characterAnimatorCore = characterAnimatorCore;
        this.characterAnimationSetting = characterAnimationSetting;
    }

    public void PlayShoot()
    {
        Animator Animator = this.characterAnimatorCore.CharacterAnimator;
        int layer = Animator.GetLayerIndex(this.characterAnimationSetting.LayerOverLay);
        Animator.Play(this.characterAnimationSetting.Fire, layer, 0f);
    }
}
