using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictDiamond : MonoBehaviour {

    public GameObject head;
    
    public TalkingHead talkingHead;

    public GameObject position;

    private const float MAX_DISTANCE = 50f;

    private float xPos; // Communication
    private float xInitial;
    private float yPos; // Aggression
    private float yInitial;

    // Use this for initialization
    void Start () {

        talkingHead = head.GetComponent<TalkingHead>();

        xInitial = position.transform.position.x;
        yInitial = position.transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {

        xPos = xInitial + talkingHead.Communicativeness - MAX_DISTANCE;
        yPos = yInitial + talkingHead.Aggressiveness - MAX_DISTANCE;

        position.transform.position = new Vector2(xPos, yPos);
        
	}
}
