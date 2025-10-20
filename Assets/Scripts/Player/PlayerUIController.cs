using System.IO;
using UnityEngine;

public class PlayerUIController : PlayerControllerBase
{
    GameObject _statUI;
    public GameObject statUI => _statUI;

    public PlayerUIController(PlayerManager pMng) : base(pMng)
    {
        InitUI();
    }
    
    void InitUI()
    {
        _statUI = GameObject.Instantiate(
            Resources.Load<GameObject>(Path.Combine("UI", "StatusPanel")),
            pMng.StatusCanvas.transform
        );

        var temp = _statUI.GetComponent<RectTransform>();
        temp.offsetMin = Vector2.zero;
        temp.offsetMax = Vector2.zero;
    }
}
