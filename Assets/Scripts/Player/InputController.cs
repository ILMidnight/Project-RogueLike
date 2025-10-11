using UnityEngine;

public class InputController : PlayerControllerBase
{
    private Vector2 _inputDirection;
    public Vector2 inputDirection => _inputDirection;

    public InputController(PlayerManager pMng) : base(pMng)
    {
        _inputDirection = new Vector2();
    }

    public override void Tick()
    {
        _inputDirection = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")).normalized;
        // pMng.test = _inputDirection;
    }
}
