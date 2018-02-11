using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictDiamond : MonoBehaviour {
    
    private TalkingHead talkingHead;

    private const float MAX_DISTANCE = 50f;

    private float xPos; // Communication
    private float xInitial;
    private float yPos; // Aggression
    private float yInitial;

    public GameObject diamondPrefab;
    public Transform canvas;
    public GameObject Dot;

    // Use this for initialization
    void Start ()
    {
        talkingHead = gameObject.GetComponent<TalkingHead>();

        GameObject diamond = (GameObject)Instantiate(diamondPrefab, canvas);

        xInitial = Dot.transform.position.x;
        yInitial = Dot.transform.position.y;

    }

    // Update is called once per frame
    void Update () {

        xPos = xInitial + talkingHead.Communicativeness - MAX_DISTANCE;
        yPos = yInitial + talkingHead.Aggressiveness - MAX_DISTANCE;

        Dot.transform.position = new Vector2(xPos, yPos);
	}
}
