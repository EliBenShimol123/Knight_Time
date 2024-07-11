using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingManager : MonoBehaviour
{
    public static FightingManager instance;
    public int width;
    public int height;
    public int offset;
    public Method currMethod;
    public LookDirection currLookDirection;
    public List<EnemyOptions> fightEnemies;
    [SerializeField] public HeroFighting hero;
    public List<Action> spacePressed = new List<Action>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            fightEnemies = new List<EnemyOptions>();
            offset = 2;
        }
        Debug.Log("fight");
    }

    void Start()
    {
        currLookDirection = LookDirection.RIGHT;
        fightEnemies.Add(EnemyOptions.DUMMY);
        fightEnemies.Add(EnemyOptions.DUMMY);
        width = 16;
        height = 9;
        switchMethod(Method.BuildMap);
    }

    public void switchMethod(Method method)
    {
        this.currMethod = method;
        switch (method)
        {
            case Method.BuildMap:
                MapManager.instance.BuildMap(width, height, offset);
                switchMethod(Method.SpawnEnemies);
                break;
            case Method.SpawnEnemies:
                //SpawnEnemies();
                EnemiesManager.instance.spawnEnemies(fightEnemies);
                switchMethod(Method.SpawnPlayer);
                break;
            case Method.SpawnPlayer:
                hero.signalSpwanPlayer();
                //PlayerTurn();
                break;
            case Method.ChooseOption:
                hero.startTurn();
                break;
            case Method.Attack:
                hero.attack();
                break;
            case Method.ChooseItem:
                break;
            case Method.ChangePosition:
                TextManager.instance.changeText("pick the tile you want to move into.", 24);
                break;
            case Method.EnemiesTurn:
                EnemiesManager.instance.enemyTurns();
                break;
            case Method.EndFight:
                if (EnemiesManager.instance.enemies.Count > 0)
                    switchMethod(Method.ChooseOption);
                else
                {
                    FightingManager.instance.spacePressed.Add(() =>
                    {
                        string expString = "you got " + EnemiesManager.instance.totalExp + " exp points." + '\n' +
                                          "(press the space bar to continue)";
                        TextManager.instance.changeText(expString, 20);
                    });

                    FightingManager.instance.spacePressed.Add(() =>
                    {
                        string endString = "fight ended." + '\n' +
                                          "(press the space bar to continue)";
                        TextManager.instance.changeText(endString, 20);
                        //TODO switch back to the main map screen
                    });
                }
                break;
            case Method.Death:
                break;
            default:
                Debug.LogWarning("Unhandled method: " + method);
                break;
        }
    }

    public void getAnswer(string yesTxt, string noTxt, Action yesAction, Action noAction)
    {
        TextManager.instance.showMakeSure(yesTxt, noTxt, yesAction, noAction);
    }



    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (currMethod == Method.Attack && hero.selectedMove != null)
        {
            if (horizontal > 0)
            {
                currLookDirection = LookDirection.RIGHT;
            }
            else if (horizontal < 0)
            {
                currLookDirection = LookDirection.LEFT;
            }

            if (vertical > 0)
            {
                currLookDirection = LookDirection.UP;
            }
            else if (vertical < 0)
            {
                currLookDirection = LookDirection.DOWN;
            }

            MapManager.instance.highlightAttackSpots();
        }

        if (spacePressed.Count>0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spacePressed[0]();
                spacePressed.RemoveAt(0);
            }
        }
    }
}


public enum Method
{
    BuildMap,
    SpawnEnemies,
    SpawnPlayer,
    ChooseOption,
    Attack,
    Defend,
    ChooseItem,
    ChangePosition,
    EnemiesTurn,
    EndFight,
    Death
}


//for choices
public enum Choice
{
    YES,
    NO,
    THINKING
}

