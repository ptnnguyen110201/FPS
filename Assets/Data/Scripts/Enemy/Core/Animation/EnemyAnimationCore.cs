using UnityEngine;

public class EnemyAnimationCore 
{
    protected Animator animator;
    protected EnemySetting enemySetting;

    public EnemyAnimationCore(Animator animator, EnemySetting enemySetting)
    {
        this.animator = animator;
        this.enemySetting = enemySetting;
    }
    public void InitLayerWeights()
    {
        this.SetLayerWeight(this.enemySetting.LayerAction, 1f);

    }
    public void SetLayerWeight(string layerName, float weight)
    {
        int index = this.animator.GetLayerIndex(layerName);
        if (index >= 0)
            this.animator.SetLayerWeight(index, weight);
    }
    public Animator Animator => this.animator;
    public EnemySetting EnemySetting => this.enemySetting;
}
