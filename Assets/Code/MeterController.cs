using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterController : MonoBehaviour {

    [Header("Right Side")]
    public Image rightAggMeter;
    public Image rightCommMeter;
    public GameObject rightHead;

    [Header("Left Side")]
    public Image leftAggMeter;
    public Image leftCommMeter;
    public GameObject leftHead;

    private TalkingHead rightTalkingHead;
    private TalkingHead leftTalkingHead;


	// Use this for initialization
	void Start () {

        rightTalkingHead = rightHead.GetComponent<TalkingHead>();
        leftTalkingHead = leftHead.GetComponent<TalkingHead>();

    }
	
	// Update is called once per frame
	void Update () {

        //Right Meters
        rightAggMeter.fillAmount = rightTalkingHead.Aggressiveness / 100;
        rightCommMeter.fillAmount = rightTalkingHead.Communicativeness / 100;

        //Left Meters
        leftAggMeter.fillAmount = leftTalkingHead.Aggressiveness / 100;
        leftCommMeter.fillAmount = leftTalkingHead.Communicativeness / 100;

        Debug.Log(leftTalkingHead.Communicativeness);

	}
}
