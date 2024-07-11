using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Hero : WorldCharacter
{

    private int maxHealth = 5;
    private int currHealth;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(1);
        }
    }


    void takeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.setHealth(currHealth);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        WorldCharacter character = collision.gameObject.GetComponent<WorldCharacter>();

        if (character != null)
        {
            // If the Character component exists, print the name
            Debug.Log("Collision detected with Character: " + character.charType);
        }
    }

    public override void Initialize()
    {
        currHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        charType = CharacterType.HERO;
    }
}
