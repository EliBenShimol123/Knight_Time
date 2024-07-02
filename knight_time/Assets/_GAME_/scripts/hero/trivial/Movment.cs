using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Movment : MonoBehaviour
{
    public Rigidbody2D hero;
    private Vector2 move = Vector2.zero;
    private float moveSpeed = 2.5f;
    public SpriteRenderer hero_visuals;

    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    


    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        //if (move.x == 0)
        //{
            move.y = Input.GetAxis("Vertical");
       // }
       // else
       // {
       //     move.y = 0;
       // }

    }

    void FixedUpdate()
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
