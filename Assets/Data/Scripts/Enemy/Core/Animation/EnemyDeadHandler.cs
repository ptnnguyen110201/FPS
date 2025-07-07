using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDeadHandler
{
    protected EnemyAnimationCore EnemyAnimationCore;
    [SerializeField] protected float sinkDistance = 1f;
    [SerializeField] protected float duration = 1.5f;
    public EnemyDeadHandler(EnemyAnimationCore EnemyAnimationCore)
    {
        this.EnemyAnimationCore = EnemyAnimationCore;
    }

    public async UniTask PlayerDeadAnimation(bool isDead, Transform deadObj, Action onDespawn)
    {
        if (!isDead) return;

        int random = UnityEngine.Random.Range(0, 2);
        this.EnemyAnimationCore.Animator.SetBool("IsDead", true);
        this.EnemyAnimationCore.Animator.SetFloat("DeadV", random);

        await UniTask.Delay(2000);
        await this.PlayDeathEffect(deadObj);

        onDespawn?.Invoke(); 
    }

    public async UniTask PlayDeathEffect(Transform ObjDead)
    {
        Vector3 startPos = ObjDead.position;
        Vector3 endPos = startPos + new Vector3(0, -this.sinkDistance, 0);


        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;


            ObjDead.position = Vector3.Lerp(startPos, endPos, t);

            await UniTask.Yield();
        }
    }


}