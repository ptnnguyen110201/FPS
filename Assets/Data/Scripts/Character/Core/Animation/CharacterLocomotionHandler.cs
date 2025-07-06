using UnityEngine;

public class CharacterLocomotionHandler
{
    [Inject] protected IInputProvider inputProvider;
    protected CharacterAnimatorCore CharacterAnimatorCore;
    protected CharacterAnimationSetting CharacterSetting;
    protected float aimingValue = 0f;
    protected float movementValue = 0f;
    protected float locomotionWeight = 1f;
    protected float targetWeight = 1f;

    public CharacterLocomotionHandler(CharacterAnimatorCore CharacterAnimatorCore, CharacterAnimationSetting CharacterSetting)
    {
        this.CharacterAnimatorCore = CharacterAnimatorCore;
        this.CharacterSetting = CharacterSetting;
        GameContext.Instance.InjectInto(this);
    }

    public void UpdateMovement()
    {
        if (inputProvider.isAttacking) this.targetWeight = 0.5f;
        else this.targetWeight = 1f;
       
        this.locomotionWeight = Mathf.Lerp(this.locomotionWeight, this.targetWeight, Time.deltaTime * 20f);
        this.CharacterAnimatorCore.SetLayerWeight(this.CharacterSetting.LayerLocomotion, this.locomotionWeight);


        if (inputProvider.MoveInput != Vector2.zero) this.movementValue += Time.deltaTime * 5f;
        else this.movementValue -= Time.deltaTime * 5f;
        
        this.movementValue = Mathf.Clamp01(this.movementValue);

        bool isRunning = inputProvider.IsSprinting;

        if (inputProvider.isAttacking)  isRunning = false;
        if (inputProvider.IsAiming) isRunning = false;
        
        this.CharacterAnimatorCore.CharacterAnimator.SetFloat(this.CharacterSetting.MovementPara, this.movementValue);
        this.CharacterAnimatorCore.CharacterAnimator.SetBool(this.CharacterSetting.RunningPara, isRunning);
    }

    public void UpdateAiming()
    {
        bool CanAim = inputProvider.IsAiming;
        if (this.CharacterAnimatorCore.isReloading) CanAim = false;
        if (CanAim) this.aimingValue += Time.deltaTime * 3f;
        else  this.aimingValue -= Time.deltaTime * 3f;

        this.aimingValue = Mathf.Clamp01(this.aimingValue);

        this.CharacterAnimatorCore.CharacterAnimator.SetFloat(this.CharacterSetting.AimingPara, this.aimingValue);
        this.CharacterAnimatorCore.CharacterAnimator.SetBool(this.CharacterSetting.AimPara, CanAim);
    }
}
