using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMan : NPC
{

    private int countDefault = 1;
    private int countDialog = 1;
    //private int numDialogs = 2;

    private GameObject blockage;
    public override void interact()
    {
        Action action = null;
        string scriptName = "PoliceManDefault" + countDefault;
        if (TownSceneManager.instance.ladyLakeSpoken && countDialog == 1)
        {
            scriptName = "PoliceManDialog" + 1;
            TownSceneManager.instance.ladyLakeSpoken = false;
            TownSceneManager.instance.policemanSpoken = true;
        }
        else if (TownSceneManager.instance.removeBlock && countDialog == 2)
        {
            blockage = GameObject.Find("road_block");
            scriptName = "PoliceManDialog" + 2;
            action = () =>
            {
                blockage.SetActive(false);
            };
        }

        countDialog = countDialog + 1;
        countDefault = countDefault + 1;

        readFromScript(scriptName, name.ToLower());
        Manager.instance.spacePressed.Add(() =>
        {
            WorldTextManager.instance.clearDialog();
            if (action != null)
                action();
        });
    }

    public override void Initialize()
    {
        base.Initialize();
        worldName = "PoliceMan";
    }
}
