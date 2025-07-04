using UnityEngine;

public interface IDamageReceiver 
{
    Transform ReceiObj { get; }
    void SetShootObj(Transform ShootObj);
    void SetHp(int currentHp, int maxHp);
    void Reborn();
    void Deduct(int Damage, Vector3 HitPoint);
    void Add(int Health);
    bool IsDead();

}