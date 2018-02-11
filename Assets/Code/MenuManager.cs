using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public string sceneName = "Test_TalkingHeads";

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void TangentialLearning()
    {

    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
