using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {
    public float timer = 2f;
    public string nextLevel = "MainMenu";

	// Use this for initialization
	void Start () {
        StartCoroutine("DisplayScene");
	}
	
    IEnumerator DisplayScene()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
}
