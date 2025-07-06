using Cysharp.Threading.Tasks;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterCtrl : LoadComponents, IPoolable
{
    [Header("Character Setting")]
    [Inject] protected IInputProvider InputProvider;
    [SerializeField]
    protected Transform CharacterModel;
    [SerializeField]
    protected CharacterController CharacterController;
    [SerializeField]
    protected Animator CharacterAnimator;
   
    public CharacterWeapon CharacterWeapon;
    public CharacterSetting CharacterSetting { get; protected set; }
    public CharacterAnimationSetting CharacterAnimationSetting { get; protected set; }
    public CharacterAnimatorCore CharacterAnimatorCore{ get; protected set; }
    public CharacterMovement CharacterMovement { get; protected set; }
    public CharacterLocomotionHandler CharacterLocomotionHandler { get; protected set; }
    public CharacterEquipHandler CharacterEquipHandler { get; protected set; }
    public CharacterShootHandler CharacterShootHandler { get; protected set; }
    public CharacterReloadHandler CharacterReloadHandler { get; protected set; }
    protected void Reset()
    {
        this.LoadComponent<Transform>(ref this.CharacterModel, transform.Find("Model"));
        this.LoadComponent<CharacterController>(ref this.CharacterController, transform);
        this.LoadComponentInChild<Animator> (ref this.CharacterAnimator);
        this.LoadComponentInChild<CharacterWeapon>(ref this.CharacterWeapon);
    }




    protected void OnEnable()
    {
        GameContext.Instance.InjectInto(this);
        this.InputProvider.OnWeaponChanged += () =>
        {
            this.CharacterWeapon.SwapWeapon().Forget();
        };
        this.InputProvider.OnReloadPressed += () =>
        {
            this.CharacterWeapon.CurrentWeapon.Reload();
        };
    }

    public void OnDespawn()
    {
     
    }

    public void OnSpawn()
    {

        this.CharacterSetting = new();
        this.CharacterAnimationSetting = new();


        this.CharacterAnimatorCore = new(this.CharacterAnimator, this.CharacterAnimationSetting);
        this.CharacterAnimatorCore.InitLayerWeights();
        this.CharacterLocomotionHandler = new(this.CharacterAnimatorCore, this.CharacterAnimationSetting);
        this.CharacterEquipHandler = new(this.CharacterAnimatorCore,this.CharacterAnimationSetting);
        this.CharacterMovement = new(this.CharacterSetting);
        this.CharacterShootHandler = new(this.CharacterAnimatorCore, this.CharacterAnimationSetting);
        this.CharacterReloadHandler = new(this.CharacterAnimatorCore, this.CharacterAnimationSetting);

        this.CharacterWeapon.LoadWeapon();
    }

    protected void Update()
    {
        this.CharacterMovement.LookAt(this.CharacterController, this.CharacterModel);
        this.CharacterMovement.Move(this.CharacterController);

        this.CharacterLocomotionHandler.UpdateMovement();
        this.CharacterLocomotionHandler.UpdateAiming();
    }
}
