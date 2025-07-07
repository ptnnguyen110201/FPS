using UnityEngine;

public class EnemyHitHandler 
{
    [Inject] IEffectManager EffectManager;
    protected EnemyAnimationCore EnemyAnimationCore;

    public EnemyHitHandler(EnemyAnimationCore EnemyAnimationCore) 
    { 
        this.EnemyAnimationCore = EnemyAnimationCore;
        GameContext.Instance.InjectInto(this);
    }

    public void TakeDamage(Vector3 attackerPos, Vector3 hitPoint, Quaternion hitRot)
    {
        Transform enemyTransform = this.EnemyAnimationCore.Animator.transform;

        Vector3 toAttacker = (attackerPos - enemyTransform.position).normalized;
        Vector3 right = enemyTransform.right;

        float dot = Vector3.Dot(right, toAttacker);
        float TakeDamage;
        if (dot > 0) TakeDamage = 1;
        else TakeDamage = 0;

        this.TakeHitEffect(hitPoint, hitRot);
        this.EnemyAnimationCore.Animator.SetTrigger("TakeDamage");
        this.EnemyAnimationCore.Animator.SetFloat("TakingDamage", TakeDamage);

    }


    public void TakeHitEffect(Vector3 HitPoint, Quaternion Rot)
     => this.EffectManager.EffectSpawner.SpawnEffect(this.EnemyAnimationCore.EnemySetting.HitEffect, HitPoint, Rot);

}