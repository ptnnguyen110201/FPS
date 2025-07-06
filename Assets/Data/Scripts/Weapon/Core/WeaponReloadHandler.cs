using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class WeaponReloadHandler
{
    protected CharacterCtrl CharacterCtrl;
    protected WeaponData WeaponData;
    protected Animator Animator;

    private CancellationTokenSource reloadTokenSource = new();

    public WeaponReloadHandler(WeaponData weaponData, CharacterCtrl characterCtrl, Animator animator)
    {
        this.CharacterCtrl = characterCtrl;
        this.WeaponData = weaponData;
        this.Animator = animator;
    }

    public void CancelReload()
    {
        if (this.reloadTokenSource != null)
        {
            this.reloadTokenSource.Cancel();
            this.reloadTokenSource.Dispose();
        }

        this.reloadTokenSource = new CancellationTokenSource();
    }

    public async UniTask<bool> ReloadEmty(CancellationToken externalToken)
    {
        if(this.CharacterCtrl.CharacterAnimatorCore.isReloading) return false;
        this.CancelReload();
        this.reloadTokenSource = new CancellationTokenSource();
        CancellationToken linkedToken = CancellationTokenSource.CreateLinkedTokenSource(
            externalToken, this.reloadTokenSource.Token).Token;

        this.CharacterCtrl.CharacterAnimatorCore.SetIsReloading(true);

        try
        {
            this.Animator?.Play(this.WeaponData.ReloadEmpty, 0, 0f);

            await UniTask.WhenAll(
                this.CharacterCtrl.CharacterReloadHandler.PlayReload(this.WeaponData.ReloadEmpty, linkedToken),
                UniTask.WaitUntil(() =>
                {
                    if (this.Animator == null || !this.Animator.isActiveAndEnabled) return true;
                    var state = this.Animator.GetCurrentAnimatorStateInfo(0);
                    return state.IsName(this.WeaponData.ReloadEmpty) && state.normalizedTime >= 0.95f;
                }, cancellationToken: linkedToken)
            );

            return true;
        }
        catch (OperationCanceledException)
        {
            this.CharacterCtrl.CharacterAnimatorCore.SetIsReloading(false);
            return false;
        }
        finally
        {
            this.CharacterCtrl.CharacterAnimatorCore.SetIsReloading(false);
        }
    }
    public async UniTask<bool> Reload()
    {
        if (this.CharacterCtrl.CharacterAnimatorCore.isReloading) return false;

        this.CancelReload();
        CancellationToken token = this.reloadTokenSource.Token;
        
        try
        {
            this.CharacterCtrl.CharacterAnimatorCore.SetIsReloading(true);

            this.Animator?.Play(this.WeaponData.Reload, 0, 0f);

            await UniTask.WhenAll(
                this.CharacterCtrl.CharacterReloadHandler.PlayReload(this.WeaponData.Reload, token),
                UniTask.WaitUntil(() =>
                {
                    if (this.Animator == null || !this.Animator.isActiveAndEnabled) return true;
                    var state = this.Animator.GetCurrentAnimatorStateInfo(0);
                    return state.IsName(this.WeaponData.Reload) && state.normalizedTime >= 0.95f;
                }, cancellationToken: token)
            );

            return true;
        }
        catch (OperationCanceledException)
        {
            return false;
        }
        finally
        {
            this.CharacterCtrl.CharacterAnimatorCore.SetIsReloading(false);
        }
    }

}
