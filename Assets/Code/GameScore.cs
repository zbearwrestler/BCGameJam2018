using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour {
    public enum EndResult { Anger, NotTalking, Win}
    [SerializeField]private float time;
    [SerializeField] private int winnerID;
    [SerializeField] private EndResult result;


    public float Time
    {
        get{ return time; }
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
                DontDestroyOnLoad(m_Instance.gameObject);
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
        Debug.Log("Save: " + winner + " " + res.ToString() + " in time " + t);
        time = t;
        winnerID = winner;
        result = res;
    }

    public string GetResultString()
    {
        return "";
    }


}
