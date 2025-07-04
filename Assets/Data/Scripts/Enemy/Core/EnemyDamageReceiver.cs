using UnityEngine;
using Cysharp.Threading.Tasks;


public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] protected Renderer EnemyRenderer;
    [SerializeField] protected EnemyCtrl EnemyCtrl;
    [SerializeField] protected float sinkDistance = 1f;
    [SerializeField] protected float duration = 1.5f;

    [Inject] protected IEffectManager EffectManager;
    [Inject] protected IEnemySpawner EnemySpawner;

    protected string HitEffect = "BloodHit";

    protected override void Reset()
    {
        base.Reset();
        this.LoadComponent<EnemyCtrl>(ref this.EnemyCtrl, transform);
        this.LoadComponentInChild<Renderer>(ref this.EnemyRenderer);
    }

    protected override void OnDead()
    {
        this.SetColliderState();
        this.HandleDeathSequence().Forget();
    }

    public override void Deduct(int Damage, Vector3 HitPoint)
    {
        base.Deduct(Damage, HitPoint);
        this.TakeHitEffect(HitPoint);
        this.EnemyCtrl.EnemyHitHandler.TakeDamage(this.ShootObj.transform.position);
    }

    private async UniTaskVoid HandleDeathSequence()
    {
        await this.EnemyCtrl.EnemyDeadHandler.PlayerDeadAnimation(this.IsDead());
        await this.PlayDeathEffect();
        this.EnemySpawner.Despawn(this.EnemyCtrl);
    }

    public async UniTask PlayDeathEffect()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(0, -this.sinkDistance, 0);

        Material material = this.EnemyRenderer.material;
        Color startColor = material.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            material.color = Color.Lerp(startColor, endColor, t);

            transform.position = Vector3.Lerp(startPos, endPos, t);

            await UniTask.Yield();
        }
    }

    protected void TakeHitEffect(Vector3 HitPoint)
    {
        Quaternion Rot = transform.rotation;
        this.EffectManager.EffectSpawner.SpawnEffect(this.HitEffect, HitPoint, Rot);
    }

    protected void OnEnable()
    {
        GameContext.Instance.InjectInto(this);
    }
}
