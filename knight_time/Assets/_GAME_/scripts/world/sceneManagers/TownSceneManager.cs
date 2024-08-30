using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownSceneManager : MonoBehaviour
{
    public static TownSceneManager instance;

    private List<string> usedDialogs;

    public bool lakeSpoken = false;

    public LadyLake ladyLake;

    public bool ladyLakeSpoken = false;

    public bool policemanSpoken = false;

    public bool removeBlock = false;

    //for trash diving
    public int trashCount = 1;
    public Raccoon raccoon;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            usedDialogs = new List<string>();
            trashCount = 1;

        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("town");
    }

    public void triggerDialog(string name, Action action = null)
    {
        useDialog(name, "trigger", action);
    }

    public void trashDialog(string name)
    {
        string script = null;
        Action action = null;

        if (name.Equals("TrashDialog1"))
        {
            script = "TrashDialog1";
            action = () =>
            {
                ladyLake.gameObject.transform.position = GameObject.Find(name).transform.position;
                GameObject.Find(name).SetActive(false);
                WorldTextManager.instance.interactChar = ladyLake;
                WorldTextManager.instance.startInteract();
            };
        }
        else
        {
            action = () =>
            {
                GameObject.Find(name).SetActive(false);
            };
            script = "TrashEncounterDialog";
            if(trashCount == 2)
            {
                action = () =>
                {

                    raccoon.gameObject.transform.position = GameObject.Find(name).transform.position;
                    GameObject.Find(name).SetActive(false);
                    WorldTextManager.instance.interactChar = raccoon;
                    WorldTextManager.instance.startInteract();
                };
            }
            else if(trashCount == 3)
            {
                action = () =>
                {
                    GameObject.Find(name).SetActive(false);
                    MainHero.instance.stopArrow();
                    removeBlock = true;
                };
            }

            script = script + trashCount;
            trashCount = trashCount + 1;
        }
        useDialog(script, "trash", action);
    }

    private void useDialog(string name, string folderName, Action addAction = null)
    {
        if (!usedDialogs.Contains(name))
        {
            WorldCharacter.readFromScript(name, folderName);
            Manager.instance.spacePressed.Add(() =>
            {
                WorldTextManager.instance.clearDialog();
                if (addAction != null)
                {
                    addAction();
                }
            });
            usedDialogs.Add(name);
            if(name.Equals("LakeDialog1"))
                lakeSpoken = true;
        }
    }

}
