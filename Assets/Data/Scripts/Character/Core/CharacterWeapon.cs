using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    [SerializeField] protected List<WeaponCtrl> Weapons = new();
    [SerializeField] public WeaponCtrl CurrentWeapon { get; protected set; }

    protected int WeaponIndex = 0;
    protected bool isSwaping = false;
    public bool IsSwaping => isSwaping;
    protected void Update()
    {
        if (this.isSwaping) return;
        this.CurrentWeapon.Shooting().Forget();
    }
    public void LoadWeapon()
    {
        foreach (Transform child in transform)
        {
            WeaponCtrl weapon = child.GetComponent<WeaponCtrl>();
            if (weapon != null)
            {
                weapon.gameObject.SetActive(false);
                this.Weapons.Add(weapon);
            }
        }
        this.SetCurrentWeapon(this.WeaponIndex).Forget();
    }

    protected async UniTask<bool> SetCurrentWeapon(int index)
    {
        if (this.Weapons.Count == 0) return false;
        if(this.CurrentWeapon != null) 
        {
           await this.CurrentWeapon.UnEquip();
        }
        this.CurrentWeapon = this.Weapons[index];
        await this.CurrentWeapon.Equip();
        return true;
    }

    public async UniTask SwapWeapon()
    {

        if (this.Weapons.Count <= 0) return;   
        if(this.isSwaping) return;
        this.isSwaping = true;
        int index = this.WeaponIndex;
        index += 1;

        if (index < 0) index = this.Weapons.Count - 1;
        else if (index > this.Weapons.Count - 1) index = 0;
        await this.SetCurrentWeapon(index);

        this.WeaponIndex = index;
        this.isSwaping = false;
    }
}
