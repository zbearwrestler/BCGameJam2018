using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TalkingHeadManager : MonoBehaviour {
    private TalkingHead[] mTalkingHeads;
    private int talkingHeadCount = 0;
    public float WinErrorMargin = 2;

    private bool m_GameIsOver = false;

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

    public void CheckForGameWin()
    {
        bool everybodyHappy = true;
        foreach (TalkingHead head in mTalkingHeads)
        {
            if (head.Aggressiveness > (100-WinErrorMargin) && head.Communicativeness < WinErrorMargin){
                everybodyHappy = false;
            }
        }


        if (everybodyHappy)
        {
            EndGame(0, GameScore.EndResult.Win);
            
        }
    }


    public void EndGame(int headID, GameScore.EndResult result)
    {
        //TalkingHeadManager.Instance.StopAllShooting();
        if (!m_GameIsOver)
        {
            m_GameIsOver = true;
            GameScore.Instance.Save(Time.timeSinceLevelLoad, headID, result);
            //TODO delay
            StartCoroutine(WaitAndLoad("EndScreen"));
        }
        
    }


    IEnumerator WaitAndLoad(string sceneName)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName);
    }


}
