using System.IO;
using UnityEngine;

public class PlayerUIController : PlayerControllerBase
{
    GameObject _statUI;
    public GameObject statUI => _statUI;

    public PlayerUIController(PlayerManager pMng) : base(pMng)
    {
        
    }

    public override void InitController()
    {
        base.InitController();

        _statUI = GameObject.Instantiate(
            Resources.Load<GameObject>(Path.Combine("UI", "StatusPanel")),
            pMng.StatusCanvas.transform
        );
        ResetPos(_statUI.GetComponent<RectTransform>());
    }

    private void ResetPos(RectTransform rect)
    {
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

}
