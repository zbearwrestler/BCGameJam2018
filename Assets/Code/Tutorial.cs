using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    public float timer = 5f;
    public Sprite[] comics;
    public int current = 0;
    public int max = 5;
    public GameObject viewer;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("DisplayScene");
    }

    IEnumerator DisplayScene()
    {
        yield return new WaitForSeconds(timer);
        current++;
        if (current == max)
        {
            SceneManager.LoadScene("TalkingHeads", LoadSceneMode.Single);
        }
        viewer.GetComponent<Image>().sprite = comics[current];
        StartCoroutine("DisplayScene");
    }
}
