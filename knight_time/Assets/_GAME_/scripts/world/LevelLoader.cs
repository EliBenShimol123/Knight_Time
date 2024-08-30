using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public int heroMaxHealth;
    public int heroHealth;
    public int heroExp;
    public List<EnemyOptions> fightEnemies;
    public Vector3 heroPosition;

    public string heroName;

    public Animator transition;

    public static LevelLoader instance;

    public string enemyFighting;

    public string beforeFightingScene;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);

            instance = this;
            fightEnemies = new List<EnemyOptions>();

            heroName = null;
            heroMaxHealth = 10;
            heroHealth = heroMaxHealth;
            heroExp = 0;
            heroPosition = new Vector3(-9, 2, 0);
            enemyFighting = null;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void loadStartingScene()
    {
        StartCoroutine(loadScene("Starting_Scene"));
    }

    public void loadTownScene(int health, int exp)
    {
        heroHealth = health;
        heroExp = exp;
        heroPosition = new Vector3(-10.3f, 2, 0);

        StartCoroutine(loadScene("Town_Scene"));


    }

    public void endScene()
    {
        StartCoroutine(loadScene("End_Scene"));
    }

    public void deathScene()
    {
        StartCoroutine(loadScene("DeathScene"));
    }

    public void loadFighting(int health, int exp, Vector3 position, string enemyName, List<EnemyOptions> attack)
    {
        heroHealth = health;
        heroExp = exp;

        fightEnemies.Clear();
        foreach(EnemyOptions enemy in attack)
        {
            fightEnemies.Add(enemy);
        }
        heroPosition = position;

        enemyFighting = enemyName;
        
        beforeFightingScene = SceneManager.GetActiveScene().name;

        StartCoroutine(loadScene("Fighting_Scene"));
    }


    public void returnFromFighting(int health, int exp)
    {
        heroHealth = health;
        heroExp = exp;

        StartCoroutine(loadScene(beforeFightingScene));
    }

    private IEnumerator loadScene(string sceneName)
    {
        transition.ResetTrigger("End");
        transition.SetTrigger("Start");

        string previousName = SceneManager.GetActiveScene().name;

        yield return new WaitForSeconds(1f);


        SceneManager.LoadScene(sceneName);

        transition.ResetTrigger("Start");
        transition.SetTrigger("End");

        if (previousName.Equals("Fighting_Scene"))
        {
            WorldTextManager.instance.healthBar.SetActive(true);
            MainHero.instance.gameObject.SetActive(true);
        }
        else if (sceneName.Equals("Fighting_Scene"))
        {
            WorldTextManager.instance.healthBar.SetActive(false);
            MainHero.instance.gameObject.SetActive(false);
        }
        //    enemyFighting = null;




    }
}
