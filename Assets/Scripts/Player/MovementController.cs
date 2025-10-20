using UnityEngine;
using UnityEngine.UIElements;

public class MovementController : PlayerControllerBase
{
    CharacterController cController;

    InputController inputController;
    Vector3 currentSpeed;

    Vector3 moveDirection;

    Transform cameraTrans;

    RaycastHit groundHit;

    public MovementController(PlayerManager pMng) : base(pMng)
    {
        moveDirection = new Vector3();
        currentSpeed = new Vector3();

        cController = pMng.GetComponent<CharacterController>();
        inputController = pMng.states["InputController"] as InputController;

        cameraTrans = Camera.main.transform;
    }

    float currentRotateX = 0;

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

        targetDirection *= pMng.nowGrounded ? pMng.walkingSpeed : pMng.airMoveSpeed;
        currentSpeed.x = targetDirection.x;
        currentSpeed.z = targetDirection.z;

        if (pMng.nowGrounded)
        {
            if (inputController.inputJump)
            {
                currentSpeed.y = pMng.jumpHeight;
                pMng.DelayGround(.3f);
            }
        }
        else
        {

        }

        currentSpeed.y += pMng.gravity * Time.deltaTime;
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
