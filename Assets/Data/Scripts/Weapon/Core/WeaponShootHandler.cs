using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class WeaponShootHandler
{
    [Inject] protected IInputProvider inputProvider;

    protected CharacterCtrl characterCtrl;
    protected WeaponData weaponData;
    protected Animator animator;

    protected float lastShootTime;
    protected int currentBullet;

    private CancellationTokenSource reloadTokenSource = new();

    public WeaponShootHandler(CharacterCtrl characterCtrl, Animator animator, WeaponData weaponData)
    {
        this.characterCtrl = characterCtrl;
        this.animator = animator;
        this.weaponData = weaponData;
        this.currentBullet = weaponData.BulletCount;

        GameContext.Instance.InjectInto(this);
    }

    public async UniTask<bool> Shooting(Func<CancellationToken, UniTask<bool>> reloadCallback)
    {
        bool isReloading = this.characterCtrl.CharacterAnimatorCore.isReloading;
        if (isReloading) return false;
        if (!this.inputProvider.isAttacking) return false;

        if (this.currentBullet <= 0)
        {
            if (!isReloading)
            {
                this.CancelReloadToken();
                this.reloadTokenSource = new CancellationTokenSource();

                try
                {
                    bool reloaded = await reloadCallback.Invoke(this.reloadTokenSource.Token);
                    if (reloaded) this.ResetBullet();
                }
                catch (OperationCanceledException)
                {
                    return false;
                }
                finally
                {
                }
            }

            return false;
        }

        float secondsPerShot = 60f / this.weaponData.FireRate;
        if (Time.time - this.lastShootTime < secondsPerShot) return false;

        this.currentBullet--;
        this.lastShootTime = Time.time;

        this.animator?.Play(this.weaponData.Fire);
        this.characterCtrl?.CharacterShootHandler?.PlayShoot();

        return true;
    }

    public int CurrentBullet => this.currentBullet;

    public void ResetBullet() => this.currentBullet = this.weaponData.BulletCount;

    public void CancelShooting()
    {
        this.characterCtrl.CharacterAnimatorCore.SetIsReloading(false);
        this.CancelReloadToken();
    }

    private void CancelReloadToken()
    {
        if (this.reloadTokenSource != null)
        {
            this.reloadTokenSource.Cancel();
            this.reloadTokenSource.Dispose();
        }

        this.reloadTokenSource = new CancellationTokenSource();
    }
}
