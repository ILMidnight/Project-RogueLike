using UnityEngine;

public class MovementController : PlayerControllerBase
{
    CharacterController cController;

    InputController inputController;
    Vector2 currentSpeed;

    Vector3 moveDirection;

    public MovementController(PlayerManager pMng) : base(pMng)
    {
        moveDirection = new Vector2();

        cController = pMng.GetComponent<CharacterController>();
        inputController = pMng.states["InputController"] as InputController;
    }

    public override void Tick()
    {
        // base.Tick();

        if (cController.isGrounded)
        {
            currentSpeed = inputController.inputDirection;

            // pMng.test = pMng.transform.forward;

            moveDirection =
            (
                pMng.transform.forward * currentSpeed.x +
                pMng.transform.right * currentSpeed.y
            );
        }
        else
        {
            moveDirection.y += -9.81f;
        }
        
        cController.Move(moveDirection * Time.deltaTime);
    }
}
