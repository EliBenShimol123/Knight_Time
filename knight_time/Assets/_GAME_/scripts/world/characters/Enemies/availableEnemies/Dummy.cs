using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : WorldEnemy
{
    public override void interact()
    {
        string scriptName = "dummyDefault";
        if (StartingSceneManager.instance.merlinDone)
            scriptName = "dummyOpeningDialog";
        readFromScript(scriptName, name.ToLower());

        if (StartingSceneManager.instance.merlinDone)
        {
            Manager.instance.spacePressed.Add(() =>
            {
                WorldTextManager.instance.clearDialog();
                Manager.instance.startFight(worldName, enemyList);
            });
        }
        else
        {
            Manager.instance.spacePressed.Add(() =>
            {
                WorldTextManager.instance.clearDialog();
            });
        }

    }

    public override void showInteract()
    {
        WorldTextManager.instance.changeInteractText("Press E to Talk?", this);
    }

    public override void takeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }


    public override void Initialize()
    {
        worldName = "Dummy";
        base.Initialize();
    }

    public override void addEnemies()
    {
        // 2 dummies
        enemyList.Add(EnemyOptions.DUMMY);
        enemyList.Add(EnemyOptions.DUMMY);
    }
}
