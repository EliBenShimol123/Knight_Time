using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class HeroFighting : Character
{

    public Move selectedMove = null;
    private bool endTurn = false;
    private bool secondTurn = false;

    public bool choosingPlace = false;

    public void signalSpwanPlayer()
    {
        TextManager.instance.changeText("your turn, choose a tile to spawn", 24);
        //Debug.Log("your turn, choose a tile to spawn");

    }

    public void spawnPlayer(Vector3 place)
    {
        this.gameObject.SetActive(true);
        createCharacter(LevelLoader.instance.heroName, LevelLoader.instance.heroHealth, 0, place);
        //hero_tile.transform.position =  place;
        //add here to load moves from the game manager that will keep all the available moves
        List<Move> moves = new List<Move>
        {
            new BladeSpin(),
            new KnifeStab(),
            new SwordSwing()

        };
        addMoves(moves);
        FightingManager.instance.switchMethod(Method.ChooseOption);
    }

    public void startTurn()
    {
        endTurn = false;
        secondTurn = false;
        turn();
    }

    public override void turn()
    {
        TextManager.instance.hideMoves();
        makeTurn();
    }

    public void makeTurn()
    {
        selectedMove = null;
        MapManager.instance.stopHighlightSpots();

        TextManager.instance.showChoices();
        TextManager.instance.changeText("please select your next action.", 24);
    }



    public void showMoves()
    {
        MapManager.instance.stopHighlightSpots();
        selectedMove = null;
        TextManager.instance.changeText("please select a move from your available moves.", 24);
        TextManager.instance.createMoves(getMoves());
    }

    public void attack()
    {
        askIfMoveOk();
        Debug.Log("end attack");
    }

    public void askIfMoveOk()
    {
        TextManager.instance.changeText(selectedMove.getDesc(), 15);
        FightingManager.instance.getAnswer("sounds good", "change", doMove, showMoves);
    }

    public void doMove()
    {
        Debug.Log("do move");
        //MapManager.instance.highlightAttackSpots();
        choosingPlace = true;
        TextManager.instance.changeText("Use arrow keys to select the attack location you want(that is also available)" + '\n' +
            "When you are ready to attack press 'attack', to return press 'abort'", 20);

        FightingManager.instance.getAnswer("attack", "abort", attackEnemies, showMoves);
    }

    public void attackEnemies()
    {
        Debug.Log("attack enemies");
        TextManager.instance.hideMakeSure();
        //MapManager.instance.stopHighlightSpots();
        choosingPlace = false;
        MapManager.instance.stopHighlightSpots();
        EnemiesManager.instance.takeDamage(place, selectedMove);
        if (selectedMove.type != Type.LIGHT || secondTurn)
            endTurn = true;
        else
            secondTurn = true;
    }


    public List<Vector3> getTilesCovered()
    {
        List<Vector3> covered = selectedMove.getTilesCovered((int)place.x, (int)place.y, FightingManager.instance.currLookDirection);
        return covered;
    }

    public bool SelectedMove(string moveName)
    {
        Move chosenMove = selectMove(moveName);
        if (secondTurn && chosenMove.type != Type.LIGHT)
        {
            TextManager.instance.changeText("in the second turn you can only attack with LIGHT moves.", 20);
            return false;
        }
        else
        {
            TextManager.instance.hideMoves();
            selectedMove = chosenMove;
            return true;
        }

    }


    public void EndTurn()
    {
        Debug.Log(secondTurn + " " + endTurn);
        MapManager.instance.stopHighlightSpots();
        TextManager.instance.clearText();
        if (endTurn || EnemiesManager.instance.enemies.Count == 0)
        {
            FightingManager.instance.switchMethod(Method.EnemiesTurn);
        }
        else if (secondTurn)
        {
            turn();
        }
    }


    public override void deathNote()
    {
        string death = "Your health has reached 0, game over.";
        FightingManager.instance.spacePressed.Add(() =>
        {
            TextManager.instance.changeText(death, 24);
            FightingManager.instance.switchMethod(Method.Death);
        });
    }


    //for option selection
    public void chooseAttack()
    {
        //FightingManager.instance.switchMethod(Method.Attack);
        TextManager.instance.clearChoices();
        showMoves();
    }

    public void chooseHeal()
    {
        TextManager.instance.changeText("Option not availabe yet.", 24);
        //TextManager.instance.clearChoices();
        //TODO
        //selectedOption = Option.ITEM;
    }

    public void chooseDefend()
    {
        TextManager.instance.changeText("Option not availabe yet.", 24);
        //TextManager.instance.clearChoices();
        //TOOD
        //selectedOption = Option.DEFEND;
    }

    public void choosePosition()
    {
        if (!secondTurn)
        {
            TextManager.instance.clearChoices();
            FightingManager.instance.switchMethod(Method.ChangePosition);
            TextManager.instance.returnButton(() => FightingManager.instance.hero.turn());
            //TODO
            //selectedOption = Option.MOVE;
        }
        else
        {
            TextManager.instance.changeText("Option not availabe at second turn, only LIGHT attacks.", 24);
        }
    }

    public void changePosition(Vector3 position)
    {
        //TextManager.instance.changeText("pick the tile you want to move into.", 24);
        this.transform.position = position;
        place = position;
        endTurn = true;
        EndTurn();

    }

    public override void endHurtAnimation()
    {
        //TODO
    }
}



public enum Option
{
    ATTACK,
    ITEM,
    DEFEND,
    MOVE,
    IDOL
}
