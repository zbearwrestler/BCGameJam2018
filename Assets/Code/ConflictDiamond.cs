using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictDiamond : MonoBehaviour {
    
    private TalkingHead talkingHead;

    public GameObject position;

    private const float MAX_DISTANCE = 50f;

    private float xPos; // Communication
    private float xInitial;
    private float yPos; // Aggression
    private float yInitial;

    public GameObject diamondPrefab;
    public Transform canvas;

    // Use this for initialization
    void Start ()
    {
        talkingHead = gameObject.GetComponent<TalkingHead>();

        xInitial = position.transform.position.x;
        yInitial = position.transform.position.y;

        GameObject diamond = (GameObject)Instantiate(diamondPrefab, canvas);

    }
	
	// Update is called once per frame
	void Update () {

        xPos = xInitial + talkingHead.Communicativeness - MAX_DISTANCE;
        yPos = yInitial + talkingHead.Aggressiveness - MAX_DISTANCE;

        position.transform.position = new Vector2(xPos, yPos);
	}
}
