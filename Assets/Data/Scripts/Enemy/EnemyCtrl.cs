using UnityEngine;

[RequireComponent(typeof(EnemyDamageReceiver))]
[RequireComponent(typeof(Collider))]
public class EnemyCtrl : LoadComponents, IPoolable
{
    [SerializeField] protected Animator Animator;
    [SerializeField] protected EnemySetting EnemySetting = new();
    [SerializeField] protected EnemyDamageReceiver EnemyDamageReceiver;


    public EnemyAnimationCore EnemyAnimationCore { get; protected set; }
    public EnemyHitHandler EnemyHitHandler { get; protected set; }
    public EnemyDeadHandler EnemyDeadHandler { get; protected set; }

    public void OnDespawn()
    {

    }
    protected void Reset()
    {
        this.LoadComponent<Animator>(ref this.Animator, transform);
        this.LoadComponent<EnemyDamageReceiver>(ref this.EnemyDamageReceiver, transform);
        
    }
    public void OnSpawn()
    {

        this.EnemyAnimationCore = new EnemyAnimationCore(this.Animator, this.EnemySetting);
        this.EnemyAnimationCore.InitLayerWeights();
        this.EnemyHitHandler = new EnemyHitHandler(this.EnemyAnimationCore);     
        this.EnemyDeadHandler = new EnemyDeadHandler(this.EnemyAnimationCore);


        this.EnemyDamageReceiver.Reborn();
    }
}
