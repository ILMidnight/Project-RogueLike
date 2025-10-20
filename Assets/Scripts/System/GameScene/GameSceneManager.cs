using Unity.Mathematics;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public static float GameTime;

    private void Awake()
    {
        GameTime = 0;
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
    }

    public static float GetCurrentDifficulty()
    {
        return math.abs((GameTime * math.sin(GameTime)) / 8) + (GameTime / 5);
    }
}

