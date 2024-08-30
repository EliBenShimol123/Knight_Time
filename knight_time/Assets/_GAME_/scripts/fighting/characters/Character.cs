using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string charName;
    protected int maxHealth;
    protected int currHealth;
    protected int exp;
    protected Dictionary<string, Move> moves; //can have up to 5 moves
    public Vector3 place;
    private string whenShowDesc;
    private int lastFont;

    public TextMeshProUGUI title;

    public void createCharacter(string name, int health, int exp, Vector3 place)
    {
        this.charName = name;
        this.maxHealth = health;
        this.currHealth = health;
        this.exp = exp;
        this.place = place;
        this.gameObject.transform.position = place;
        MapManager.instance.changeOccupied(place);
        moves = new Dictionary<string, Move>();

        title = this.GetComponentInChildren<TextMeshProUGUI>();
        title.text = this.charName;
        title.gameObject.SetActive(false);

        this.gameObject.AddComponent<BoxCollider2D>();
    }

    public int getHealth()
    {
        return currHealth;
    }
    public int getExp()
    {
        return exp;
    }

    public virtual void setHealth(int damage)
    {
        Debug.Log(charName + " taken damage " + damage);

        FightingManager.instance.spacePressed.Add(() =>
        {
            string damageString = "'" + charName + "'" + " was dealt " + damage + " damage points." + '\n' +
                              "(press the space bar to continue)";
            TextManager.instance.changeText(damageString, 20);
        });


        this.currHealth = this.currHealth - damage;
        if (this.currHealth <= 0)
        {
            this.currHealth = 0;
        }
        else
        {
            FightingManager.instance.spacePressed.Add(() =>
            {
                string healthString = "'" + charName + "'" + " has " + currHealth + " health points left." + '\n' +
                                  "(press the space bar to continue)";
                TextManager.instance.changeText(healthString, 20);
                endHurtAnimation();
            });
        }

        if (this.currHealth == 0)
            Destroy(gameObject);
    }

    public abstract void turn();
    public abstract void deathNote();

    public abstract void endHurtAnimation();


    public string getDesc()
    {
        string desc = "name: " + charName + '\n' +
                      "health: " + currHealth + '/' + maxHealth + '\n';
        return desc;
    }


    // to display description
    private void OnMouseEnter()
    {
        whenShowDesc = TextManager.instance.getText();
        lastFont = TextManager.instance.getFontSize();
        TextManager.instance.changeText(getDesc(), 20);

        title.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        TextManager.instance.changeText(whenShowDesc, lastFont);
        title.gameObject.SetActive(false);
    }


    //moves functions
    public void addMove(Move move)
    {
        moves[move.name] = move;
    }

    public void addMoves(List <Move> addMoves)
    {
        foreach (Move move in addMoves)
        {
            addMove(move);
        }
    }

    public Move selectMove(string name)
    {
        return moves[name];
    }

    public List<string> getMoves()
    {
        List<string> moveNames = new List<string>();
        foreach(Move move in moves.Values)
            moveNames.Add(move.name);
        return moveNames;
    }
}
