using System;
using UnityEngine;

[Serializable]
public class WeaponData 
{

    [Header("Weapon Setting")]  
    public WeaponType WeaponType;
    public int Damage;
    public float FireRate;
    public int BulletCount;
    public string MuzzleName = "ShootMuzzle";

    [Header("Animation State")]

    public string Reload = "Reload";
    public string ReloadEmpty = "Reload Empty";
    public string Fire = "Fire";


}