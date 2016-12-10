﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;

// VEEEEEEEEEEEEEEEEEEEEEEI eu não sei pq isso ta funcionando e isso não me importa, só precisa utilizar o checkTask se quiser verificar
// Se o index do objeto em que você acabou de realizar a task foi uma das task que estava na checklist, caso contrário nem toca
// No resto dessa classe

public class ChecklistManager : MonoBehaviour
{
    public Text checkList;

    private List<string> savedLines = new List<string>();
    private string[] lines = null;
    private CharacterController player;

    // Use this for initialization
    void Start()
    {
        Load("Checklists/Checklist.txt");
        player = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        checkList.text = "";
        for (int i = 0; i < savedLines.Count; i++)
        {
            if (i != 0)
                checkList.text = string.Concat(checkList.text, "\r\n");
            checkList.text = string.Concat(checkList.text, savedLines[i]);
        }
    }

    private bool Load(string fileName)
    {
        // Handle any problems that might arise when reading the text
        try
        {
            lines = File.ReadAllLines(Application.dataPath + "/" + fileName);
            DoStuff(lines);
        }
        // If anything broke in the try block, we throw an exception with information
        // on what didn't work
        catch (System.Exception e)
        {
            print(e);
            return false;
        }
        return true;
    }
    void DoStuff(string[] entries)
    {
        for (int i = 0; i < 3; i++)
        {
            int x = UniqueRandomInt(0, entries.Length);
            savedLines.Add(entries[x]);
        }
    }

    List<int> usedValues = new List<int>();
    public int UniqueRandomInt(int min, int max)
    {
        int val = Random.Range(min, max);
        while (usedValues.Contains(val))
        {
            val = Random.Range(min, max);
        }
        usedValues.Add(val);
        return val;
    }

    // Checar se o index da task estiver na checklist
    public void checkTask(int index)
    {
        try
        {
            if (savedLines.Contains(lines[index]))
            {
                savedLines.Remove(lines[index]);
                player.score.AddToScore(500);
            }
        }
        catch (System.Exception e)
        {
            print(e);
            print("Véi. O erro que aconteceu ta de boas, não fudeo nada não.");
        }
    }

}