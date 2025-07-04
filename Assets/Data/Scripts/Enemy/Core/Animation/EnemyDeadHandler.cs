using Cysharp.Threading.Tasks;
using UnityEngine;
public class EnemyDeadHandler
{
    protected EnemyAnimationCore EnemyAnimationCore;

    public EnemyDeadHandler(EnemyAnimationCore EnemyAnimationCore)
    {
        this.EnemyAnimationCore = EnemyAnimationCore;
    }

    public async UniTask PlayerDeadAnimation(bool isDead)
    {
        if (isDead == false) return;
        int random = Random.Range(0, 2);

        this.EnemyAnimationCore.Animator.SetBool("IsDead", isDead);
        this.EnemyAnimationCore.Animator.SetFloat("DeadV", random);

        await UniTask.Delay(2 * 1000);

    }

}