using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class WeaponCtrl : LoadComponents
{
    [SerializeField] protected CharacterCtrl characterCtrl;
    [SerializeField] protected Animator weaponAnimator;
    [SerializeField] protected RuntimeAnimatorController characterAnimator;
    [SerializeField] protected WeaponMuzzleHandler weaponMuzzleHandler;

    [field: SerializeField] public WeaponData WeaponData { get; protected set; } = new WeaponData();
    public WeaponDamageSender WeaponDamageSender { get; protected set; }
    public WeaponShootHandler WeaponShootHandler { get; protected set; }
    public WeaponRaycastHandler WeaponRaycastHandler { get; protected set; }
    public WeaponReloadHandler WeaponReloadHandler { get; protected set; }


    protected void OnEnable()
    {
        this.LoadComponentInParent<CharacterCtrl>(ref this.characterCtrl);
        this.LoadComponent<Animator>(ref this.weaponAnimator, transform);
        this.LoadComponentInChild<WeaponMuzzleHandler>(ref this.weaponMuzzleHandler);
        this.Factory();
    }

    protected void Factory()
    {
        if (this.WeaponDamageSender == null)
            this.WeaponDamageSender = new();

        if (this.WeaponRaycastHandler == null)
            this.WeaponRaycastHandler = new(
                this.WeaponData,
                this.characterCtrl,
                this.WeaponDamageSender);

        if (this.WeaponShootHandler == null)
            this.WeaponShootHandler = new(
                this.characterCtrl,
                this.weaponAnimator,
                this.WeaponData);

        if (this.WeaponReloadHandler == null)
            this.WeaponReloadHandler = new(
                this.WeaponData,
                this.characterCtrl,
                this.weaponAnimator);
    }
    public void Reload() => this.WeaponReloadHandler.Reload().Forget();
    public async UniTask Shooting()
    {
        bool Shooted = await this.WeaponShootHandler.Shooting(
            token => this.WeaponReloadHandler.ReloadEmty(token));
          

        if (!Shooted) return;

        this.weaponMuzzleHandler.SpawnMuzzle(this.WeaponData.MuzzleName).Forget();
        this.WeaponRaycastHandler.ShootRay();
    }

    public async UniTask<bool> Equip()
    {
        this.SetRuntimeAnimator();   
        this.gameObject.SetActive(true);
        await this.characterCtrl.CharacterEquipHandler.PlayUnHolster();    

        return true;
    }

    public async UniTask<bool> UnEquip()
    {       
        this.WeaponShootHandler.CancelShooting();
        this.WeaponReloadHandler.CancelReload();
        await this.characterCtrl.CharacterEquipHandler.PlayHolster();
        this.gameObject.SetActive(false);
        return true;
    }

    public void SetRuntimeAnimator()
        => this.characterCtrl.CharacterAnimatorCore.SetController(this.characterAnimator);
}
