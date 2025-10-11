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

    DataLoader loader;
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
        loader = new DataLoader();
        sceneController = new SceneController();
        audioListenerController = new AudioListenerController(
            transform.GetChild(0).gameObject
        );
        
        // CSV Data Loading
        // foreach (var data in loader.LoadExpCSV("expData.csv"))
        // {
        //     Debug.Log(data);
        // }

        // Scene Loading
        // sceneController.loadScene(1);

        
    }
}
