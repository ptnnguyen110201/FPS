using UnityEngine;

public class CharacterMovement
{
    [Inject] protected IInputProvider InputProvider;
    protected CharacterSetting CharacterSetting;
    protected CharacterLocomotionHandler CharacterLocomotionHandler;
    protected Vector3 velocity;
    protected float yaw;   
    protected float pitch;


    public CharacterMovement(CharacterSetting CharacterSetting)
    {
        this.CharacterSetting = CharacterSetting;
        GameContext.Instance.InjectInto(this);
    }
    public void LookAt(CharacterController characterController, Transform model)
    {
        Vector2 lookInput = this.InputProvider.LookInput;

        float mouseX = lookInput.x * this.CharacterSetting.mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * this.CharacterSetting.mouseSensitivity * Time.deltaTime;


        this.yaw += mouseX;
        characterController.transform.rotation = Quaternion.Euler(0f, this.yaw, 0f);


        this.pitch -= mouseY;
        this.pitch = Mathf.Clamp(this.pitch, this.CharacterSetting.pitchClampMin, this.CharacterSetting.pitchClampMax);
        model.localRotation = Quaternion.Euler(this.pitch, 0f, 0f);
    }
    public void Move(CharacterController characterController)
    {
        this.ApplyGravity(characterController);

        Vector2 inputDir = this.InputProvider.MoveInput;

        Vector3 forward = characterController.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = characterController.transform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 moveDirection = forward * inputDir.y + right * inputDir.x;
        moveDirection.Normalize();

        float speed;
        if(this.InputProvider.IsAiming) speed = this.CharacterSetting.aimingMoveSpeed;
        else speed = this.CharacterSetting.moveSpeed;
        Vector3 movement = moveDirection * speed;

        Vector3 finalMove = movement + Vector3.up * this.velocity.y;
        characterController.Move(finalMove * Time.deltaTime);
    }
    protected void ApplyGravity(CharacterController controller)
    {
        if (controller.isGrounded && this.velocity.y < 0)
        {
            this.velocity.y = -2f;
        }

        this.velocity.y += this.CharacterSetting.gravity * Time.deltaTime;
    }
}
