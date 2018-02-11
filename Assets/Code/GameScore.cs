using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour {
    public enum EndResult { Anger, NotTalking, Win}
    private float time;
    private int winnerID;
    private EndResult result;


    public int Time
    {
        get{ return (int)time; }
    }

    public int WinnerID { get { return winnerID; } }
    public EndResult Result { get { return result; } }

    private static GameScore m_Instance;
    public static GameScore Instance
    {
        get
        {
            if (m_Instance){
                return m_Instance;
            }
            else
            {
                m_Instance = new GameObject().AddComponent<GameScore>();
                return m_Instance;
            }
            
            
        }
    }


    //----------------------------------------------------------------------------------------------------


    void Awake () {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(gameObject);
        }
        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    //----------------------------------------------------------------------------------------------------
    public void Save(float t, int winner, EndResult res)
    {
        Instance.time = time;
        Instance.winnerID = winner;
        Instance.result = res;
    }

    public string GetResultString()
    {
        return "";
    }


}
