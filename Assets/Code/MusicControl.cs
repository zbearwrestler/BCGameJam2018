using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControl : MonoBehaviour {

    public AudioMixerSnapshot quiet;
    public AudioMixerSnapshot low;
    public AudioMixerSnapshot medium;
    public AudioMixerSnapshot Intense;
    public GameObject rightHead;
    public GameObject leftHead;
    public float bpm = 120;

    private float m_TransitionIn;

    private float m_Quarternote;

    private TalkingHead rightHeadScript;
    private TalkingHead leftHeadScript;

    private const float THREE_QUARTERS = 75;
    private const float HALF = 50;
    private const float ONE_QUARTER = 25;

    private int frame;

	// Use this for initialization
	void Start () {
        m_Quarternote = 60 / bpm;
        m_TransitionIn = m_Quarternote;

        
        rightHeadScript = rightHead.GetComponent<TalkingHead>();
        leftHeadScript = leftHead.GetComponent<TalkingHead>();

        /* TEST CODE
        rightHeadScript.Aggressiveness = 10;
        rightHeadScript.Communicativeness = 90;
        leftHeadScript.Aggressiveness = 10;
        leftHeadScript.Communicativeness = 90;
        */
    }

    // Update is called once per frame
    void Update () {

        float agg;
        float comm;

        if(rightHeadScript.Aggressiveness > leftHeadScript.Aggressiveness)
        {
            agg = rightHeadScript.Aggressiveness;
        }
        else
        {
            agg = leftHeadScript.Aggressiveness;
        }

        if(rightHeadScript.Communicativeness < leftHeadScript.Communicativeness)
        {
            comm = rightHeadScript.Communicativeness;
        }
        else
        {
            comm = leftHeadScript.Communicativeness;
        }

        bool aggOrComm = true; // true means use aggression, false means use communication

        if(agg == comm)
        {
            aggOrComm = true;
        }else if(100 - agg < comm)
        {
            aggOrComm = true;
        }
        else
        {
            aggOrComm = false;
        }

        if (aggOrComm) //use agg
        {
            if(agg > THREE_QUARTERS)
            {
                Intense.TransitionTo(m_TransitionIn);
            }
            else if(agg > HALF)
            {
                medium.TransitionTo(m_TransitionIn);
            }
            else if(agg > ONE_QUARTER)
            {
                low.TransitionTo(m_TransitionIn);
            }
            else
            {
                quiet.TransitionTo(m_TransitionIn);
            }
        }
        else // use comm
        {
            if (comm < ONE_QUARTER)
            {
                Intense.TransitionTo(m_TransitionIn);
            }
            else if (comm < HALF)
            {
                medium.TransitionTo(m_TransitionIn);
            }
            else if (comm < THREE_QUARTERS)
            {
                low.TransitionTo(m_TransitionIn);
            }
            else
            {
                quiet.TransitionTo(m_TransitionIn);
            }
        }
		
        /* TEST CODE
        frame++;
        

        if(frame%10 == 0)
        {
            rightHeadScript.Aggressiveness += 1;
            rightHeadScript.Communicativeness -= 1;
            leftHeadScript.Aggressiveness += 1;
            leftHeadScript.Communicativeness -= 1;
            Debug.Log(rightHeadScript.Aggressiveness);
        }

        */

	}
}
