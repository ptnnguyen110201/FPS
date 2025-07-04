using UnityEngine;

public class WeaponAnimatorCore
{
    public Animator Animator { get; protected set; }
    public WeaponCtrl WeaponCtrl { get; protected set; }

    public WeaponAnimatorCore(Animator Animator, WeaponCtrl WeaponCtrl) 
    {
        this.Animator = Animator;
        this.WeaponCtrl = WeaponCtrl;   
    }
}
