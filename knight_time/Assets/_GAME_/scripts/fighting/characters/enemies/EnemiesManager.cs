using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] public List<Enemy> enemyPrefabs;

    public static EnemiesManager instance;
    public List<Enemy> enemies = new List<Enemy>();
    public int totalExp;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Debug.Log("enemies");
    }

    public void spawnEnemies(List<EnemyOptions> availableEnemies)
    {
        totalExp = 0;
        int count = 1;
        foreach (EnemyOptions enemy in availableEnemies)
        {
            Vector3 place = MapManager.instance.GetRandomTile();
            if (place.x > -1) // means the tile is legal
            {
                Enemy enemyPrefab = enemyPrefabs.Find(prefab => prefab.enemyType == enemy);

                if (enemyPrefab != null)
                {
                    Enemy enemyTile = Instantiate(enemyPrefab, place, Quaternion.identity);
                    enemyTile.GetComponent<Enemy>().Initialize(count, place);//remember to use when wanting to get Enemy component
                    enemyTile.gameObject.SetActive(true);
                    enemies.Add(enemyTile);
                }
            }
            count = count + 1;
        }
        /*
        Enemy enemyPrefab = enemyPrefabs.Find(prefab => prefab.enemyType == EnemyOptions.DUMMY);
        Enemy enemyTile = Instantiate(enemyPrefab, new Vector3(3, 7), Quaternion.identity);
        enemyTile.GetComponent<Enemy>().Initialize(1, new Vector3(3, 7));//remember to use when wanting to get Enemy component
        enemyTile.gameObject.SetActive(true);
        enemies.Add(enemyTile);

        Enemy enemyTile2 = Instantiate(enemyPrefab, new Vector3(4, 7), Quaternion.identity);
        enemyTile2.GetComponent<Enemy>().Initialize(2, new Vector3(4, 7));//remember to use when wanting to get Enemy component
        enemyTile2.gameObject.SetActive(true);
        enemies.Add(enemyTile2);
        */
    }

    public void enemyTurns()
    {
        if (enemies.Count > 0)
        {
            Debug.Log("enmies turn");
            string damageString = "the Enemies turn:" + '\n' +
                          "(press the space bar to continue)";
            TextManager.instance.changeText(damageString, 20);
            foreach (Enemy enemy in enemies)
            {
                enemy.turn();
            }
            FightingManager.instance.spacePressed.Add(() =>
            {
                TextManager.instance.changeText("", 24);
                FightingManager.instance.switchMethod(Method.EndFight);

            });
        }

    }

    public void takeDamage(Vector3 heroPlace, Move selectedMove)
    {
        bool attackedEnemy = false;
        string enemiesAttacked = "the following enemies where hurt:" + '\n' +
                              "(press the space bar to continue)";
        TextManager.instance.changeText(enemiesAttacked, 20);

        foreach (Enemy enemy in enemies)
            if (selectedMove.canReach(heroPlace, enemy.place, FightingManager.instance.currLookDirection))
            {
                attackedEnemy = true;
                enemy.setHealth(selectedMove.damage);
                if (enemy.getHealth() == 0)
                {
                    totalExp = totalExp + enemy.getExp();
                    enemy.deathNote();
                }
            }
        if (!attackedEnemy)
        {
            FightingManager.instance.spacePressed.Add(() =>
            {
                string noHit = "the move did not hit any enemies." + '\n' +
                      "(press the space bar to continue)";
                TextManager.instance.changeText(noHit, 20);
            });
        }

        FightingManager.instance.spacePressed.Add(() =>
        {
            TextManager.instance.changeText("", 24);
            FightingManager.instance.hero.EndTurn();

        });
        enemies.RemoveAll(enemy => enemy.getHealth() <= 0);
    }
}
