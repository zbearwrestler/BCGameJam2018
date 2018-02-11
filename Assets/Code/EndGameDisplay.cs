using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameDisplay : MonoBehaviour {
    public Text displayText;
    public GameObject winBackground;
    public GameObject loseBackground;

    private string messageString = "";
    private string[] actionStrings =
    {
        "held off the inevitable",
        "maintained peace",
        "fought for peace",
        "prevented violence",
        "fought human nature",
        "kept the peace"
    };

    private string[] violenceStrings =
    {
        "grabbed an axe.",
        "started suplexed you through a table.",
        "punched you in the face.",
        "flew into a bloody rage.",
        "went postal.",
        "gave in to violent impulses."
    };

    private string[] nameStrings = { "Rob", "Alfie"};


    private string[] notListeningStrings =
   {
        "stopped listening to you.",
        "stormed out screaming.",
        "gave up on talking.",
        "refused to listen further.",
        "stopped listening to reason.",
        "slapped his hands over his ears."
    };

    private void Start()
    {
        //set up first part
        Debug.Log(GameScore.Instance.Time);
        messageString = "You " + actionStrings[Random.Range(0, 6)]
            + " for " + GameScore.Instance.Time + " seconds until ";
        switch (GameScore.Instance.Result)
        {
            case GameScore.EndResult.Win:
                messageString += "peace was achieved";
                break;
            case GameScore.EndResult.Anger:
                messageString += nameStrings[GameScore.Instance.WinnerID] + " " + violenceStrings[Random.Range(0, 6)];
                break;
            case GameScore.EndResult.NotTalking:
                messageString += nameStrings[GameScore.Instance.WinnerID] + " " + notListeningStrings[Random.Range(0, 6)];
                break;
            default:
                messageString += "something impossible happened";
                break;
        }
        bool won = (GameScore.Instance.Result == GameScore.EndResult.Win);
        winBackground.SetActive(won);
        loseBackground.SetActive(!won);

        displayText.text = messageString;

    }

    // Update is called once per frame
    void Update () {
		if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Main Menu");
        }
	}
}
