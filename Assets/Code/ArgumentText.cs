using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ArgumentText : MonoBehaviour{

    //Private variables
    private const string DiologeLocation = "..\\BCGameJam2018\\Assets\\Dialogue";

    private static ArgumentText instance;
    private static object syncRoot = new Object();

    private static int mConvoCounter;

    //Public variables
    public static int convoCounter
    {
        get
        {
            return mConvoCounter;
        }
    }



    //--------------------------------------------------
    //Unity Stuff

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSeanLoad;
    }

    private static void OnSeanLoad(Scene scene, LoadSceneMode mode)
    {
        //Put if statment to control the reset baised on sean name
        Debug.Log("CpnvoCounterReset");
        mConvoCounter = 0;
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
    public static string GetLine(string type, int line)
    {
        System.IO.StreamReader File = new System.IO.StreamReader(DiologeLocation + "\\" + type + ".txt");
        for(int i = 0; i < line - 1; ++i)
        {
            File.ReadLine();
        }

        return File.ReadLine();
    }

    public static string NextLine(string type)
    {
        mConvoCounter++;
        System.IO.StreamReader File = new System.IO.StreamReader(DiologeLocation + "\\" + type + ".txt");
        for (int i = 0; i < mConvoCounter - 1; ++i)
        {
            File.ReadLine();
        }

        return File.ReadLine();
    }
}
