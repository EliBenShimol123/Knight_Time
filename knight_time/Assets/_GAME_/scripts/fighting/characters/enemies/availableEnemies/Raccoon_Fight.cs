using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raccoon_Fight : Enemy
{

    public Animator transition;

    public override void turn()
    {
        FightingManager.instance.spacePressed.Add(() =>
        {
            string damageString = "Its " + "'" + charName + "'" + " turn to attack." + '\n' +
                              "(press the space bar to continue)";
            TextManager.instance.changeText(damageString, 20);
        });
        bool moveFound = false;

        foreach (Move move in moves.Values)
        {
            if (move.canReach(place, FightingManager.instance.hero.place, LookDirection.EveryDir))
            {
                moveFound = true;
                FightingManager.instance.hero.setHealth(move.damage);
                if (FightingManager.instance.hero.getHealth() == 0)
                {
                    FightingManager.instance.hero.deathNote();
                }
                break;
            }

        }
        if (!moveFound)
        {
            FightingManager.instance.spacePressed.Add(() =>
            {
                string damageString = "'" + charName + "'" + " could not find a move to use." + '\n' +
                                  "(press the space bar to continue)";
                TextManager.instance.changeText(damageString, 20);
            });
        }
    }

    public override void Initialize(int addToName, Vector3 place)
    {
        this.canMove = false;
        enemyType = EnemyOptions.RACCOON;
        createCharacter("raccoon " + addToName, 3, 1, place);
        //add here to load moves from the game manager that will keep all the available moves
        List<Move> moves = new List<Move>
        {
            new KnifeStab()
        };
        addMoves(moves);
    }


    public override void setHealth(int damage)
    {

        base.setHealth(damage);

        transition.ResetTrigger("Idol");
        transition.SetTrigger("Attack");

        Debug.Log(charName + " " + "attacked");
    }

    public override void endHurtAnimation()
    {
        transition.ResetTrigger("Attack");
        transition.SetTrigger("Idol");
    }
}