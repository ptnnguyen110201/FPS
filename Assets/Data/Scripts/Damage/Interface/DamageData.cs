using UnityEngine;

public struct DamageData
{
    public Transform SendObj;
    public int Damage;

    public DamageData(Transform SendObj, int Damage) 
    {
        this.SendObj = SendObj;
        this.Damage = Damage;
    }
}