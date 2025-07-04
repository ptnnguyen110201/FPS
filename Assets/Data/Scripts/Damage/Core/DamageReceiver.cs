using UnityEngine;

[RequireComponent (typeof(Collider))]
public abstract class DamageReceiver : LoadComponents, IDamageReceiver
{
 
    [field: SerializeField] public int maxHp {  get; protected set; }
    [field: SerializeField] public int currentHp { get; protected set; }
    
    [SerializeField] protected Collider Collider;
    public Transform ReceiObj => this.transform;
    protected Transform ShootObj;

    protected virtual void Reset()
    {
        this.LoadComponent<Collider>(ref this.Collider, transform);
    }
    public void Add(int Health)
    {
        if (this.IsDead()) return;
        this.currentHp += Health;
    }

    public virtual void Deduct(int Damage, Vector3 HitPoint)
    {
        this.currentHp -= Damage;
        if (this.IsDead())
        {
            this.currentHp = 0;
            this.OnDead(); 
        }
    }

    public bool IsDead()
    {
        if(this.currentHp <= 0) return true;
        return false;
    }

    protected abstract void OnDead();
    public void Reborn()
    {
        this.currentHp = this.maxHp;
        this.SetColliderState();
    }

    public void SetHp(int currentHp, int maxHp)
    {
        this.currentHp = currentHp;
        this.maxHp = maxHp;
    }

    public void SetShootObj(Transform ShootObj) => this.ShootObj = ShootObj;
    protected void SetColliderState() => this.Collider.enabled = !this.IsDead();

}
