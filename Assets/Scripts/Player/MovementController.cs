using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovementController : PlayerControllerBase
{
    CharacterController cController;
    PlayerStatusController pState;

    InputController inputController;
    Vector3 currentSpeed;

    Vector3 moveDirection;

    Transform cameraTrans;

    RaycastHit groundHit;

    public float gravity = -9.81f * 2;

    public PlayerMovementController(PlayerManager pMng) : base(pMng)
    {
        
    }

    float currentRotateX = 0;

    public override void InitController()
    {
        base.InitController();

        moveDirection = new Vector3();
        currentSpeed = new Vector3();

        cController = pMng.GetComponent<CharacterController>();
        inputController = pMng.states["InputController"] as InputController;

        pState = pMng.states["StatusController"] as PlayerStatusController;

        cameraTrans = Camera.main.transform;
    }

    void HandleLook()
    {
        Vector3 multipleLookSpeed = inputController.inputMouseDirection * pMng.lookSpeed * Time.deltaTime;
        pMng.transform.Rotate(Vector3.up * multipleLookSpeed.x);
        
        currentRotateX -= multipleLookSpeed.y;
        currentRotateX = Mathf.Clamp(currentRotateX, -pMng.lookXLimit, pMng.lookXLimit);
        cameraTrans.localRotation = Quaternion.Euler(currentRotateX, 0, 0);
    }

    void Movement()
    {
        currentSpeed = moveDirection;
        Vector3 targetDirection = (
            pMng.transform.forward * inputController.inputDirection.x +
            pMng.transform.right * inputController.inputDirection.z
        );

        targetDirection *= pMng.nowGrounded ? pState.baseStatus.movementSpeed : pState.baseStatus.movementSpeed / 2;
        currentSpeed.x = targetDirection.x;
        currentSpeed.z = targetDirection.z;

        if (pMng.nowGrounded)
        {
            if (inputController.inputJump)
            {
                currentSpeed.y = 1.5f + pState.baseStatus.movementSpeed/2;
                pMng.DelayGround(.3f);
            }
        }
        else
        {

        }

        currentSpeed.y += gravity * Time.deltaTime;
        currentSpeed.y = Mathf.Clamp(currentSpeed.y, -20, 20);
        moveDirection = currentSpeed;

        cController.Move(moveDirection * Time.deltaTime);
    }

    public override void Tick()
    {
        HandleLook();
        Movement();
    }
}
