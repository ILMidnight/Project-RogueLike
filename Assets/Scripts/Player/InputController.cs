using UnityEngine;

public class InputController : PlayerControllerBase
{
    private Vector3 _inputDirection;
    public Vector3 inputDirection => _inputDirection;

    private Vector2 _inputMouseDirection;
    public Vector2 inputMouseDirection => _inputMouseDirection;

    private bool _inputJump;
    public bool inputJump => _inputJump;

    private bool _inputClick;
    public bool inputClick => _inputClick;

    public InputController(PlayerManager pMng) : base(pMng)
    {

    }

    public override void InitController()
    {
        base.InitController();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _inputDirection = new Vector2();
    }

    public override void Tick()
    {
        _inputDirection = new Vector3(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal")).normalized;
        _inputMouseDirection = Input.mousePositionDelta;
        _inputJump = Input.GetKey(KeyCode.Space);

        if (Input.GetMouseButtonDown(0))
            _inputClick = true;
    }

    public void CheckClick()
    {
        _inputClick = false;
    }
}
