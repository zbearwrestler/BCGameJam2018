using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingHeadManager : MonoBehaviour {
    private TalkingHead[] mTalkingHeads;
    private int talkingHeadCount = 0;

    private static TalkingHeadManager m_instance;
    public static TalkingHeadManager Instance
    {
        get
        {
            return m_instance;
        }
    }


    private void Awake()
    {
        m_instance = this;


        GameObject[] talkingHeadObjects = GameObject.FindGameObjectsWithTag("TalkingHead");
        talkingHeadCount = talkingHeadObjects.Length;

        mTalkingHeads = new TalkingHead[talkingHeadCount];

        for (int i = 0; i < talkingHeadCount; ++i)
        {
            mTalkingHeads[i] = talkingHeadObjects[i].GetComponent<TalkingHead>();
        }
    }

    public void StopAllShooting()
    {
        for (int i = 0; i < talkingHeadCount; ++i)
        {
            mTalkingHeads[i].StopShooting();
        }
    }

    public void NotifyWasInterrupted(int headID)
    {
        
        TalkingHead headWithID = null;
        for (int i = 0; i < talkingHeadCount; ++i)
        {
            if (mTalkingHeads[i].HeadID == headID)
            {
                headWithID = mTalkingHeads[i];
                break;
            }
        }
        if (headWithID != null)
        {
            headWithID.NotifyWasInterrupted();
        }
    }

    
}
