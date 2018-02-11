using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ArgumentText : MonoBehaviour{

    //Private variables
    private const string DialogueLocation = "\\Dialogue";

    private static ArgumentText instance;
    private static object syncRoot = new Object();

    private static int mConvoCounter;


    //--------------------------------------------------
    //Unity Stuff

    //private void Awake()
    //{
        //DontDestroyOnLoad(this);
    //}

    private static void OnSeanLoad(Scene scene, LoadSceneMode mode)
    {
    }

    //---------------------------------------------------
    //Singolton Stuff
    private ArgumentText()
    {
    }

    public static ArgumentText Instance
    {
        get
        {
            if(instance == null)
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ArgumentText();
                    }
                }
            }
            return instance;
        }
    }

    //The way this is called GetLine(The name of the file you want to pull from, The line you want to pull)
    public static string GetLine(string type, int headID, int line)
    {
        System.IO.StreamReader File = new System.IO.StreamReader(Application.dataPath + DialogueLocation + "\\" + headID + "\\" + type + ".txt");
        for(int i = 0; i < line - 1; ++i)
        {
            File.ReadLine();
        }

        return File.ReadLine();
    }
}
