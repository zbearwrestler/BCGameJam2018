using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerCreater : MonoBehaviour {

    public GameObject musicManager;
    public Transform m_MainCamera;

	// Use this for initialization
	void Start () {

        musicManager = (GameObject)Instantiate(musicManager, m_MainCamera);

	}
	
}
