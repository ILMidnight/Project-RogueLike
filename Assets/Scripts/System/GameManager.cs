using UnityEngine;

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
    }
}
