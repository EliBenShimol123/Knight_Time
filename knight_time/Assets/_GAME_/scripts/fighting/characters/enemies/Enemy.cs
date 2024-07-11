using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class Enemy : Character
{
    public EnemyOptions enemyType;
    protected bool canMove = false;

    public abstract void Initialize(int addToName, Vector3 place);

    public override void deathNote()
    {
        string death = "'" + charName + "'" + " was killed.";
        FightingManager.instance.spacePressed.Add(() =>
        {
            TextManager.instance.changeText(death, 24);
        });
    }
}