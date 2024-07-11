using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] private SpriteRenderer skin;
    [SerializeField] private Vector3 position;
    [SerializeField] private GameObject highlight;
    public bool isOccupied;
    private SpriteRenderer spriteRenderer;

    public Sprite grass;
    public Sprite little_rock;
    public Sprite many_rock;

    public void buildTile(Vector3 tilePlace, int randomNumber)
    {
        this.position = tilePlace;
        System.Random random = new System.Random();

        if (randomNumber < 70)
            skin.sprite = grass;
        else if (randomNumber < 90)
            skin.sprite = little_rock;
        else
            skin.sprite = many_rock;

        spriteRenderer = highlight.GetComponent<SpriteRenderer>();
        isOccupied = false;

    }


    public void highlightAttack()
    {
        if (FightingManager.instance.hero.selectedMove != null)
        {
            spriteRenderer.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.5f);
            highlight.SetActive(true);
        }
    }

    public void stopHighlight()
    {
        highlight.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (FightingManager.instance.currMethod == Method.SpawnPlayer || FightingManager.instance.currMethod == Method.ChangePosition)
        {
            spriteRenderer.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
            highlight.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        if (FightingManager.instance.currMethod == Method.SpawnPlayer || FightingManager.instance.currMethod == Method.ChangePosition)
        {
            highlight.SetActive(false);
        }
    }


    private void OnMouseDown()
    {
        if (FightingManager.instance.currMethod == Method.SpawnPlayer && !isOccupied)
        {
            FightingManager.instance.hero.spawnPlayer(position);
        }

        else if (FightingManager.instance.currMethod == Method.ChangePosition)
        {
            FightingManager.instance.hero.changePosition(position);
        }
        highlight.SetActive(false);
    }

    public bool isOccupiedTile()
    {
        return isOccupied;
    }

    public void changeOccupied()
    {
        isOccupied = !isOccupied;
    }
}
