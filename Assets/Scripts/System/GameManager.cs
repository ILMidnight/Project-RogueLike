using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            return _instance;
        }
    }
    SceneController sceneController;

    public AudioListenerController audioListenerController;

    [SerializeField]
    Button testBtn;
    Text testBtnText;

    public UnityEvent gameOverEvent;

    private void Awake() {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sceneController = new SceneController();
        audioListenerController = new AudioListenerController(
            transform.GetChild(0).gameObject
        );

        testBtnText = testBtn.transform.GetChild(0).GetComponent<Text>();

        testBtn.onClick.AddListener(() =>
        {
            bool nowLobby = SceneManager.GetActiveScene().buildIndex == 0;
            sceneController.loadScene(nowLobby ? 1 : 0);

            testBtnText.text = nowLobby ?
            "Go to Lobby" : "Play";
        });
    }

    public void GameOver()
    {
        gameOverEvent.Invoke();
        testBtn.onClick.Invoke();
    }
}
