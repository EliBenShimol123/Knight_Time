using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Hero : MonoBehaviour
{

    public int maxHealth = 5;
    public int currHealth;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
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
}
