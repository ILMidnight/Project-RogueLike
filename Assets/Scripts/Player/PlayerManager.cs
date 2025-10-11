using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Dictionary<string, IPlayerState> states;

    [SerializeField]
    public Vector3 test;
    

    #region Player Movement Values
    [Header("Movement Settings")]
    public float walkingSpeed = 5.0f;
    public float runningSpeed = 10.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f * 2; // 더 빠르게 떨어지도록 중력값 조정

    [Header("Camera Look Settings")]
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float currentSpeed;
    #endregion

    private void Awake() {
        states = new Dictionary<string, IPlayerState>();

        states.Add("InputController", new InputController(this));
        states.Add("MoveController", new MovementController(this));
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var state in states)
        {
            state.Value.Tick();
        }
    }
}
