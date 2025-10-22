using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Dictionary<string, IPlayerState> states;

    [SerializeField]
    public Vector3 test;


    #region Movement Values
    [Header("Movement Settings")]
    public float walkingSpeed = 5.0f;
    public float runningSpeed = 10.0f;
    public float airMoveSpeed = 3.5f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f * 2; // 더 빠르게 떨어지도록 중력값 조정

    [Header("Camera Look Settings")]
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public bool nowGrounded = false;
    bool dontCheckGrounded = false;
    #endregion

    #region Attack Values
    public LayerMask skipMask;
    public Transform attackPoolTrans;
    #endregion

    #region UI Methoad
    public GameObject StatusCanvas;
    #endregion

    private void Awake()
    {
        states = new Dictionary<string, IPlayerState>();
        
        states.Add("UIController", new PlayerUIController(this));
        states.Add("InputController", new InputController(this));
        states.Add("MoveController", new PlayerMovementController(this));
        states.Add("AttackController", new AttackController(this));
        states.Add("StatusController", new PlayerStatusController(this));
    }

    // Update is called once per frame
    void Update()
    {
        if (!dontCheckGrounded)
            nowGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        foreach (var state in states)
        {
            state.Value.Tick();
        }
    }

    public void DelayGround(float time)
    {
        nowGrounded = false;
        dontCheckGrounded = true;
        Invoke("InvokeDelay", time);
    }

    void InvokeDelay()
    {
        // nowGrounded = true;
        dontCheckGrounded = false;
    }

    Coroutine invulnerabilityRoot;
    float invulnerabilityTime = .5f;

    bool nowInvulnerability = false;

    public void Hit(int damage)
    {
        if (nowInvulnerability)
        {
            return;
        }
        else
        {
            if (invulnerabilityRoot != null)
                StopCoroutine(invulnerabilityRoot);
            invulnerabilityRoot = StartCoroutine(InvulnerabilityDelay(invulnerabilityTime));
        }

        Debug.Log("Hit");
        
        (states["StatusController"] as PlayerStatusController).AddDamage(damage);
    }

    IEnumerator InvulnerabilityDelay(float delayTime)
    {
        nowInvulnerability = true;
        yield return new WaitForSecondsRealtime(delayTime);
        nowInvulnerability = false;
    }
}
