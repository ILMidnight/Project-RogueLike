using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public Dictionary<string, IPlayerState> states;

    [SerializeField]
    public Vector3 test;
    bool nowControll = false;

    #region Exp Values
    DataLoader loader;
    int currentLevel = 0;
    float currentExp = 0;
    int[] nextLevelExpArr;

    public UnityEvent levelUpEvent;
    #endregion

    #region Movement Values
    public bool nowGrounded = false;
    bool dontCheckGrounded = false;
    #endregion

    #region View Values
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    #endregion

    #region Attack Values
    public LayerMask skipMask;
    public Transform attackPoolTrans;
    #endregion

    #region UI Values
    public GameObject StatusCanvas;
    public SkillSelectGroup skillSelectGroup;
    #endregion

    #region Hit Values 
    Coroutine invulnerabilityRoot;
    float invulnerabilityTime = .5f;


    bool nowInvulnerability = false;
    #endregion

    #region Regeneration Values
    Coroutine generationRoot;
    Coroutine generationDelayRoot;
    #endregion

    #region Setting Player Functions 
    private void Awake()
    {
        // SetCursor(false);

        SetPause(false);

        loader = new DataLoader();
        nextLevelExpArr = loader.LoadExpCSV("expData.csv");

        states = new Dictionary<string, IPlayerState>();

        states.Add("StatusController", new PlayerStatusController(this));
        states.Add("UIController", new PlayerUIController(this));
        states.Add("InputController", new InputController(this));
        states.Add("MoveController", new PlayerMovementController(this));
        states.Add("AttackController", new AttackController(this));
        

        StartCoroutine(InvokeInitControllers(.15f));
    }

    public void SetPause(bool nowPause)
    {
        if (nowPause)
        {
            SetCursor(true);
            Time.timeScale = 0;
        }
        else
        {
            SetCursor(false);
            Time.timeScale = 1;
        }
    }

    public void SetCursor(bool canControl)
    {
        if (canControl)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    IEnumerator InvokeInitControllers(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);
        foreach (var temp in states)
        {
            temp.Value.InitController();
        }

        skillSelectGroup.CustomAwake();

        nowControll = true;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (!nowControll)
            return;

        CheckedGround();

        foreach (var state in states)
        {
            state.Value.Tick();
        }
    }

    #region Grounded Functions
    void CheckedGround()
    {
        if (!dontCheckGrounded)
            nowGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
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
    #endregion

    #region Hit Functions
    public void Hit(int damage)
    {
        if (nowInvulnerability)
        {
            return;
        }
        else
        {
            if (generationDelayRoot != null)
                StopCoroutine(generationDelayRoot);
            generationDelayRoot = StartCoroutine((states["StatusController"] as PlayerStatusController).DelayRegenerationHP());

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
    #endregion

    
    #region Regeneration Functions
    public void RegenerationHP()
    {
        if (generationRoot != null)
            StopCoroutine(generationRoot);
        generationRoot = StartCoroutine((states["StatusController"] as PlayerStatusController).RegenerationHP());
    }
    #endregion

    #region Exp Functions
    public void AddExp(int addtiveExp)
    {
        currentExp += addtiveExp;
        CheckedLevelUp();
    }

    private void CheckedLevelUp()
    {
        if (nextLevelExpArr[currentLevel] <= currentExp)
        {
            currentExp -= nextLevelExpArr[currentLevel++];
            LevelUP();
        }
    }

    public void LevelUP()
    {
        levelUpEvent.Invoke();
        CheckedLevelUp();
    }
    #endregion
}
