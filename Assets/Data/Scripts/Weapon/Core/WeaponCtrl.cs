using Cysharp.Threading.Tasks;
using UnityEngine;

public class WeaponCtrl : LoadComponents
{    
    [SerializeField] 
    protected Animator WeaponAnimator;
    [SerializeField]
    protected RuntimeAnimatorController CharacterAnimator;
    [SerializeField]
    protected WeaponMuzzleHandler WeaponMuzzleHandler;


    public WeaponData WeaponData { get; protected set; } = new WeaponData();
    protected void OnEnable()
    {
        this.LoadComponent<Animator>(ref this.WeaponAnimator, transform);
        this.LoadComponentInChild<WeaponMuzzleHandler>(ref this.WeaponMuzzleHandler);
    }



  
   
 
}
