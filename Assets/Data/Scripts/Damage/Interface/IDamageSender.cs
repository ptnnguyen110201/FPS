using UnityEngine;

public interface IDamageSender 
{
    void Send(IDamageReceiver DamageReceiver, Vector3 HitPoint);
}