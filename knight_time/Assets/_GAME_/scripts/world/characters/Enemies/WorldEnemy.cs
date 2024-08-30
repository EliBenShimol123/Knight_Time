using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class WorldEnemy : WorldCharacter
{

    public List<EnemyOptions> enemyList;

    public override void Initialize()
    {
        charType = CharacterType.ENEMY;
        mainSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        addEnemies();
    }

    void Start()
    {
        Initialize();
        if (LevelLoader.instance.enemyFighting != null && LevelLoader.instance.enemyFighting.Equals(worldName))
        {
            Debug.Log("dead");
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unregister the method when the object is destroyed to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method is called every time a new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("name: " + LevelLoader.instance.enemyFighting);
        if (LevelLoader.instance.enemyFighting != null && LevelLoader.instance.enemyFighting.Equals(worldName))
        {
            Debug.Log("dead");
            Destroy(gameObject);
        }
    }

    public abstract void addEnemies();
}
