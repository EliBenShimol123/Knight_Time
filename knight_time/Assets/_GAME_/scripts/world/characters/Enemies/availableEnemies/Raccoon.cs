using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccoon : WorldEnemy
{

    public override void interact()
    {
        string scriptName = "RaccoonDialog";
        readFromScript(scriptName, name.ToLower());
        Manager.instance.spacePressed.Add(() =>
        {
            WorldTextManager.instance.clearDialog();
            Manager.instance.startFight(worldName, enemyList);
        });
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
        worldName = "Raccoon";
        base.Initialize();
    }

    public override void addEnemies()
    {
        // 2 raccoons
        enemyList.Add(EnemyOptions.RACCOON);
        enemyList.Add(EnemyOptions.RACCOON);
        enemyList.Add(EnemyOptions.RACCOON);
    }
}