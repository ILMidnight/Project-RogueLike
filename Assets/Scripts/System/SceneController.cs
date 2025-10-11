using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController
{
    public async Task loadScene(int sceneIndex, LoadSceneMode mode = LoadSceneMode.Additive)
    {
        Debug.Log("Start Scene Load");

        Scene sceneToUnload = SceneManager.GetActiveScene();

        try
        {
            await SceneManager.LoadSceneAsync(sceneIndex, mode);

            Scene newScene = SceneManager.GetSceneByBuildIndex(1);

            Debug.Log($"{newScene.name} Scene Loaded.");

            if (mode == LoadSceneMode.Additive)
            {
                SceneManager.SetActiveScene(newScene);

                AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneToUnload);
                await unloadOperation;
            }

            GameManager.instance.audioListenerController.SetAudioListener();
        } catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }
}
