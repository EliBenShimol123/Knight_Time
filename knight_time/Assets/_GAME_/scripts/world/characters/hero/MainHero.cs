using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class MainHero : WorldCharacter
{
    public static MainHero instance;

    public string selectedName;
    public int exp;

    public HealthBar healthBar;

    public Arrow arrow;

    public bool canMove;


    //for movement
    [SerializeField] public Rigidbody2D hero;
    private Vector2 move = Vector2.zero;
    private float moveSpeed = 2.5f;
    public SpriteRenderer hero_visuals;

    [SerializeField] public Sprite up;
    [SerializeField] public Sprite down;
    [SerializeField] public Sprite left;
    [SerializeField] public Sprite right;



    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");

    }

    void FixedUpdate()
    {
        if (MainHero.instance.canMove)
        {
            hero.transform.position += new Vector3(move.x, move.y, 0) * moveSpeed * Time.fixedDeltaTime;
            //hero.velocity = move * moveSpeed * Time.fixedDeltaTime;
            if (move.x < 0)
            {
                hero_visuals.sprite = left;
            }
            else if (move.x > 0)
            {
                hero_visuals.sprite = right;
            }
            else if (move.y < 0)
            {
                hero_visuals.sprite = down;
            }
            else if (move.y > 0)
            {
                hero_visuals.sprite = up;
            }
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            instance.Initialize();
            Destroy(gameObject);
        }
        Debug.Log("main hero");
    }

    private void Start()
    {

        Initialize();
    }




    public override void takeDamage(int damage)
    {
        health -= damage;
        healthBar.setHealth(health);
    }


    public void heal()
    {
        health = maxHealth;
        healthBar.setHealth(health);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        WorldInteractables character = collision.gameObject.GetComponent<WorldInteractables>();

        if (character != null)
        {
            character.showInteract();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Action action = null;
        if (collision.gameObject.name.Equals("MerlsInn1"))
            action = () =>
            {
                LevelLoader.instance.endScene();
            };
        TownSceneManager.instance.triggerDialog(collision.gameObject.name, action);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        WorldTextManager.instance.clearInteractText();
    }

    public override void Initialize()
    {
        mainSprite = down;
        canMove = true;
        charType = CharacterType.HERO;
        worldName = "Hero";

        //from level loader
        selectedName = LevelLoader.instance.heroName;
        maxHealth = LevelLoader.instance.heroMaxHealth;
        health = LevelLoader.instance.heroHealth;
        this.gameObject.transform.position = LevelLoader.instance.heroPosition;

        healthBar.setMaxHealth(maxHealth);
        healthBar.setHealth(health);
    }


    //for target objective

    public void pointArrow(List<GameObject> targets)
    {
        arrow.gameObject.SetActive(true); 
        arrow.changeTarget(targets);
    }

    public void stopArrow()
    {
        arrow.stopTarget();
    }


    //no need for hero
    public override void interact()
    {
        throw new System.NotImplementedException();
    }

    public override void showInteract()
    {
        throw new System.NotImplementedException();
    }

    internal void loadName(string enteredName)
    {
        selectedName = enteredName;
        LevelLoader.instance.heroName = selectedName;
    }
}
