using UnityEngine;

public class InputController : PlayerControllerBase
{
    private Vector3 _inputDirection;
    public Vector3 inputDirection => _inputDirection;

    private Vector2 _inputMouseDirection;
    public Vector2 inputMouseDirection => _inputMouseDirection;

    private bool _inputJump;
    public bool inputJump => _inputJump;

    public InputController(PlayerManager pMng) : base(pMng)
    {
        _inputDirection = new Vector2();
    }

    public override void Tick()
    {
        _inputDirection = new Vector3(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal")).normalized;
        _inputMouseDirection = Input.mousePositionDelta;
        _inputJump = Input.GetKey(KeyCode.Space);
        
        // pMng.test = _inputMouseDirection;
    }
}
