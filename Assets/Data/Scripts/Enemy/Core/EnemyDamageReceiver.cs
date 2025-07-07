using UnityEngine;
using Cysharp.Threading.Tasks;


public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] protected EnemyCtrl EnemyCtrl;


    [Inject] protected IEnemySpawner EnemySpawner;


    protected override void Reset()
    {
        base.Reset();
        this.LoadComponent<EnemyCtrl>(ref this.EnemyCtrl, transform);
    }

    protected override void OnDead()
    {
        this.SetColliderState();
        this.HandleDeathSequence().Forget();
    }

    public override void Deduct(int Damage, Vector3 HitPoint)
    {
        base.Deduct(Damage, HitPoint);
        this.EnemyCtrl.EnemyHitHandler.TakeDamage(this.ShootObj.transform.position, HitPoint, transform.rotation);
    }

    protected async UniTaskVoid HandleDeathSequence()
    {
        await this.EnemyCtrl.EnemyDeadHandler.PlayerDeadAnimation(
            this.IsDead(),
            this.EnemyCtrl.transform,
            () => this.EnemySpawner.Despawn(this.EnemyCtrl)
        );
    }



    protected void OnEnable()
    {
        GameContext.Instance.InjectInto(this);
    }
}
