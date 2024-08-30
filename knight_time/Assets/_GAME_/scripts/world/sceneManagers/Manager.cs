using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public List<Action> spacePressed = new List<Action>();


    public List<GameObject> sceneState = new List<GameObject>();
    public List<WorldInteractables> worldChars = new List<WorldInteractables>();

    public bool canSkip;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            canSkip = true;

        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("world");
    }


    public void startFight(string enemyName, List<EnemyOptions> options)
    {
        LevelLoader.instance.loadFighting(MainHero.instance.health, MainHero.instance.exp, MainHero.instance.gameObject.transform.position, enemyName, options);
    }



    void Update()
    {
        if (spacePressed.Count > 0)
        {
            MainHero.instance.canMove = false;
            if (Input.GetKeyDown(KeyCode.Space) && canSkip)
            {
                spacePressed[0]();
                spacePressed.RemoveAt(0);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !canSkip)
                WorldTextManager.instance.skipDialog = true;
        }


        if (Input.GetKeyDown(KeyCode.E) && !WorldTextManager.instance.inInteract && WorldTextManager.instance.interactChar != null)
        {
            WorldTextManager.instance.startInteract();
        }
    }

}
