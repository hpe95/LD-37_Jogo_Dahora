using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;

public class LevelManager : MonoBehaviour {

    public Text score;
    public Text checkList;

	// Use this for initialization
	void Start () {
        Load("Checklists/Checklist.txt");
	}
	
	// Update is called once per frame
	void Update () {
        IncrementScore();
	}

    public void IncrementScore()
    {
        int x = Int32.Parse(score.text);
        x += 1;
        score.text = x.ToString();
    }

    public void AddToScore(int amount)
    {
        int x = Int32.Parse(score.text);
        x += amount;
        score.text = x.ToString();
    }

    private bool Load(string fileName)
    {
        // Handle any problems that might arise when reading the text
        try
        {
            string line;
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            print(Application.dataPath + "/" + fileName);
            StreamReader theReader = new StreamReader(Application.dataPath + "/" + fileName, Encoding.Default);
            // Immediately clean up the reader after this block of code is done.
            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            // (Do not confuse this with the using directive for namespace at the 
            // beginning of a class!)
            using (theReader)
            {
                // While there's lines left in the text file, do this:
                do
                {
                    line = theReader.ReadLine();

                    if (line != null)
                    {
                        DoStuff(line);
                    }
                }
                while (line != null);
                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
                return true;
            }
        }
        // If anything broke in the try block, we throw an exception with information
        // on what didn't work
        catch (Exception e)
        {
            print("deu certo n");
            Console.WriteLine("{0}\n", e.Message);
            return false;
        }
    }
    void DoStuff(string entries)
    {
        checkList.text = string.Concat(checkList.text, entries + "\r\n");
    }
}
