using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LadyLake : NPC
{
    public int dialiogNumber = 1;

    private int numDialogs = 2;
    public override void interact()
    {
        Action action = null;

        string scriptName = "LadyLakeDefault";
        if (dialiogNumber <= numDialogs)
        {
            if (dialiogNumber == 1)
            {
                scriptName = "LadyLakeDialog" + dialiogNumber;
                dialiogNumber = dialiogNumber + 1;
            }
            else if (TownSceneManager.instance.policemanSpoken)
            {
                scriptName = "LadyLakeDialog" + dialiogNumber;
                dialiogNumber = dialiogNumber + 1;

                action = () =>
                {

                    List<TrashCan> trashCans = FindObjectsOfType<TrashCan>().ToList();
                    List<GameObject> trashObjects = new List<GameObject>();
                    foreach (TrashCan trash in trashCans)
                        trashObjects.Add(trash.gameObject);
                    MainHero.instance.pointArrow(trashObjects);
                    TrashCan.canContinue = true;
                };
            }

        }

        readFromScript(scriptName, name.ToLower());
        Manager.instance.spacePressed.Add(() =>
        {
            WorldTextManager.instance.clearDialog();
            TownSceneManager.instance.ladyLakeSpoken = true;
            if(action != null)
                action();
        });
    }

    public override void Initialize()
    {
        base.Initialize();
        worldName = "LadyLake";
    }
}
