using UnityEngine;

public class CharacterAnimatorCore 
{
    public Animator CharacterAnimator {  get; protected set; }
    public CharacterAnimationSetting CharacterAnimationSetting { get; protected set; }
    public bool isReloading { get; protected set; } = false;
    public void SetIsReloading(bool isReloading) => this.isReloading = isReloading; 

    public CharacterAnimatorCore(Animator CharacterAnimator, CharacterAnimationSetting CharacterAnimationSetting) 
    { 
        this.CharacterAnimator = CharacterAnimator;
        this.CharacterAnimationSetting = CharacterAnimationSetting;
        GameContext.Instance.InjectInto(this);
    }
    
    public void SetController(RuntimeAnimatorController controller)
        => this.CharacterAnimator.runtimeAnimatorController = controller;

    public void SetLayerWeight(string layerName, float weight)
    {
        int index = this.CharacterAnimator.GetLayerIndex(layerName);
        if (index >= 0)
            this.CharacterAnimator.SetLayerWeight(index, weight);
    }

    public void InitLayerWeights()
    {
        this.SetLayerWeight(this.CharacterAnimationSetting.LayerLocomotion, 1f);
        this.SetLayerWeight(this.CharacterAnimationSetting.LayerHolster, 1f);
        this.SetLayerWeight(this.CharacterAnimationSetting.LayerOverLay, 1f);
        this.SetLayerWeight(this.CharacterAnimationSetting.LayerAction, 1f);
        this.SetLayerWeight(this.CharacterAnimationSetting.LayerWiggles, 1f);
    }


}
