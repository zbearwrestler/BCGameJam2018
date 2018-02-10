using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class ArgumentText{

    private const string DiologeLocation = "..\\BCGameJam2018\\Assets\\Dialogue";
    private static List<string> Angerys;

    static ArgumentText()
    {
    }

    //The way this is called GetLine(The name of the file you want to pull from, The line you want to pull)
    public static string getLine(string type, int line)
    {
        System.IO.StreamReader File = new System.IO.StreamReader(DiologeLocation + "\\" + type + ".txt");
        for(int i = 0; i < line - 1; ++i)
        {
            File.ReadLine();
        }

        return File.ReadLine();
    }
}
