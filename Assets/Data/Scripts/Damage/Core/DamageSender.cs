using UnityEngine;

public abstract class DamageSender : IDamageSender
{
    protected DamageData DamageData { get; set; }

    public void SetDamageData(DamageData DamageData) => this.DamageData = DamageData;
    public void Send(IDamageReceiver DamageReceiver, Vector3 HitPoint)
    {
        if (this.DamageData.SendObj == DamageReceiver.ReceiObj) return;
        if (this.DamageData.SendObj.tag == DamageReceiver.ReceiObj.tag) return;

        DamageReceiver.Deduct(this.DamageData.Damage, HitPoint);

    }
}