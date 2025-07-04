using InfimaGames.LowPolyShooterPack;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterCtrl : LoadComponents, IPoolable
{
    [Header("Character Setting")]
    [SerializeField]
    protected Transform CharacterModel;
    [SerializeField]
    protected CharacterController CharacterController;
    [SerializeField]
    protected Animator CharacterAnimator;

    public CharacterInventory CharacterInventory {  get; protected set; }
    public CharacterSetting CharacterSetting { get; protected set; }
    public CharacterAnimationSetting CharacterAnimationSetting { get; protected set; }
    public CharacterAnimatorCore CharacterAnimatorCore{ get; protected set; }
    public CharacterMovement CharacterMovement { get; protected set; }

    protected void Reset()
    {
        this.LoadComponent<Transform>(ref this.CharacterModel, transform.Find("Model"));
        this.LoadComponent<CharacterController>(ref this.CharacterController, transform);
        this.LoadComponentInChild<Animator> (ref this.CharacterAnimator);
    }

    public void OnDespawn()
    {
     
    }

    public void OnSpawn()
    {

        this.CharacterInventory = new();
        this.CharacterSetting = new();
        this.CharacterAnimationSetting = new();


        this.CharacterAnimatorCore = new CharacterAnimatorCore(this.CharacterAnimator, this.CharacterAnimationSetting);
        this.CharacterMovement = new(this.CharacterSetting);

    }

    protected void Update()
    {
        this.CharacterMovement.LookAt(this.CharacterController, this.CharacterModel);
        this.CharacterMovement.Move(this.CharacterController);
    }
}
