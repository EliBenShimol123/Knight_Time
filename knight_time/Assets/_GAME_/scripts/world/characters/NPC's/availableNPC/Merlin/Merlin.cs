using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Merlin : NPC
{
    public int dialiogNumber = 1;

    private int numDialogs = 2;
    public override void interact()
    {
        if (findByName("Dummy") == null)
        {
            string scriptName = "EndDialog";
            WorldCharacter.readFromScript(scriptName, name.ToLower());
            Manager.instance.spacePressed.Add(() =>
            {
                WorldTextManager.instance.clearDialog();
                LevelLoader.instance.loadTownScene(MainHero.instance.health, MainHero.instance.exp);
            });

        }
        else
        {
            string scriptName = "merlinDefault";
            if (dialiogNumber <= numDialogs)
            {
                scriptName = "MerlinOpeningDialog" + dialiogNumber;
                dialiogNumber = dialiogNumber + 1;
            }

            readFromScript(scriptName, name.ToLower());
            if (MainHero.instance.selectedName == null)
            {
                Manager.instance.spacePressed.Add(() =>
                {
                    WorldTextManager.instance.clearDialog();
                    WorldTextManager.instance.showEnterName();
                });
            }
            else
            {
                Manager.instance.spacePressed.Add(() =>
                {
                    WorldTextManager.instance.clearDialog();
                });
            }
            if (dialiogNumber == numDialogs + 1)
                StartingSceneManager.instance.merlinDone = true;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        worldName = "Merlin";
    }
}
