using UnityEngine;

public class EnemyHitHandler 
{
    protected EnemyAnimationCore EnemyAnimationCore;

    public EnemyHitHandler(EnemyAnimationCore EnemyAnimationCore) 
    { 
        this.EnemyAnimationCore = EnemyAnimationCore;
    }

    public void TakeDamage(Vector3 attackerPos)
    {
        Transform enemyTransform = this.EnemyAnimationCore.Animator.transform;

        Vector3 toAttacker = (attackerPos - enemyTransform.position).normalized;
        Vector3 right = enemyTransform.right;

        float dot = Vector3.Dot(right, toAttacker);
        float TakeDamage;
        if (dot > 0) TakeDamage = 1;
        else TakeDamage = 0;


        this.EnemyAnimationCore.Animator.SetTrigger("TakeDamage");
        this.EnemyAnimationCore.Animator.SetFloat("TakingDamage", TakeDamage);
    }

}