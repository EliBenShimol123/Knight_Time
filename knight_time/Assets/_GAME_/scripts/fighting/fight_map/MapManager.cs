using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager : MonoBehaviour
{

    public static MapManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        Debug.Log("map");
    }

    [SerializeField] private Camera mainCamera;

    [SerializeField] private Tile tile;
    private Dictionary<Vector3, Tile> map = new Dictionary<Vector3, Tile>();
    public void BuildMap(int width, int height, int offset)
    {
        System.Random random = new System.Random();
        for (int x = 0; x < width; x++)
        {
            for (int y = offset; y < height; y++)
            {
                int randomNumber = random.Next(0, 101);
                Vector3 tilePlace = new Vector3(x, y);
                Tile sTile = Instantiate(tile, tilePlace, Quaternion.identity);
                sTile.buildTile(tilePlace, randomNumber);
                map[tilePlace] = sTile;
            }
        }
        mainCamera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -15);
    }

    public void changeOccupied(Vector3 place)
    {
        map[place].changeOccupied();
    }


    public Vector3 GetRandomTile()
    {
        System.Random random = new System.Random();
        List<Vector3> keys = new List<Vector3>(map.Keys);
        int maxTry = FightingManager.instance.width * (FightingManager.instance.height-FightingManager.instance.offset) / 2;
        int count = 0;
        while (count < maxTry)
        {
            // Generate a random index
            int randomIndex = random.Next(keys.Count);
            Vector3 place = keys[randomIndex];
            if (!map[place].isOccupiedTile())
                return place;
            count++;
        }
        return new Vector3(-1, -1, 0);
    }

    public void highlightAttackSpots()
    {
        stopHighlightSpots();
        List<Vector3> tilesCovered = FightingManager.instance.hero.getTilesCovered();
        foreach(Vector3 tilePlace in tilesCovered)
        {
            if (map.ContainsKey(tilePlace))
            {
                map[tilePlace].highlightAttack();
            }
        }
    }

    public void stopHighlightSpots()
    {
        foreach (var tile in map.Values)
            tile.stopHighlight();
    }
}
