
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public string name;
    public int damage;
    public Element element;
    public Direction direction;
    public Type type;
    public int additionalTiles;
    public string additionalInfo;

    public Move(string name, int damage, Element element, Direction direction, Type type, int additionalTiles, string additionalInfo)
    {
        this.name = name;
        this.damage = damage;
        this.element = element;
        this.direction = direction;
        this.type = type;
        this.additionalTiles = additionalTiles;
        this.additionalInfo = additionalInfo;
    }

    public int getNumOfTilesCovered()
    {
        int tilesCovered = 0;
        if (direction == Direction.ROW || direction == Direction.DIAGONAL)
        {
            tilesCovered = 1 + additionalTiles;
        }
        else if (direction == Direction.COLUMN)
        {
            tilesCovered = 1 + 2 * additionalTiles;
        }
        else if (direction == Direction.AllAround)
        {
            tilesCovered = 8;
        }
        return tilesCovered;
    }

    public List<Vector3> getTilesCovered(int x, int y, LookDirection look)
    {
        List<Vector3> places = new List<Vector3>();
        if (direction == Direction.ROW)
        {
            for (int i = 0; i <= additionalTiles; i++)
            {
                if(look == LookDirection.RIGHT || look == LookDirection.EveryDir)
                    places.Add(new Vector3(x + 1 + i, y));
                if (look == LookDirection.LEFT || look == LookDirection.EveryDir)
                    places.Add(new Vector3(x - 1 - i, y));
                if (look == LookDirection.UP || look == LookDirection.EveryDir)
                    places.Add(new Vector3(x, y + 1 + i));
                if (look == LookDirection.DOWN || look == LookDirection.EveryDir)
                    places.Add(new Vector3(x, y - 1 - i));
            }
        }
        else if(direction == Direction.DIAGONAL)
        {
            for (int i = 0; i <= additionalTiles; i++)
            {
                if (look == LookDirection.RIGHT || look == LookDirection.EveryDir)
                {
                    places.Add(new Vector3(x + 1 + i, y + 1 + i));
                    places.Add(new Vector3(x + 1 + i, y - 1 - i));
                }
                if (look == LookDirection.LEFT || look == LookDirection.EveryDir)
                {
                    places.Add(new Vector3(x - 1 - i, y + 1 + i));
                    places.Add(new Vector3(x - 1 - i, y - 1 - i));
                }
                if (look == LookDirection.UP || look == LookDirection.EveryDir)
                {
                    places.Add(new Vector3(x + 1 + i, y + 1 + i));
                    places.Add(new Vector3(x - 1 - i, y + 1 + i));
                }
                if (look == LookDirection.DOWN || look == LookDirection.EveryDir)
                {
                    places.Add(new Vector3(x + 1 + i, y - 1 - i));
                    places.Add(new Vector3(x - 1 - i, y - 1 - i));
                }
            }
        }
        else if (direction == Direction.COLUMN)
        {
            for (int i = 0; i <= additionalTiles; i++)
            {
                if (look == LookDirection.RIGHT || look == LookDirection.EveryDir)
                {
                    places.Add(new Vector3(x + 1, y + i));
                    places.Add(new Vector3(x + 1, y - i));
                }
                if (look == LookDirection.LEFT || look == LookDirection.EveryDir)
                {
                    places.Add(new Vector3(x - 1, y + i));
                    places.Add(new Vector3(x - 1, y - i));
                }
                if (look == LookDirection.UP || look == LookDirection.EveryDir)
                {
                    places.Add(new Vector3(x + i, y + 1));
                    places.Add(new Vector3(x - i, y + 1));
                }
                if (look == LookDirection.DOWN || look == LookDirection.EveryDir)
                {
                    places.Add(new Vector3(x + i, y - 1));
                    places.Add(new Vector3(x - i, y - 1));
                }
            }

        }
        else if (direction == Direction.AllAround)
        {
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    places.Add(new Vector3(x + i, y + j));
        }
        return places;
    }
    public string getDesc()
    {
        string ans = "name: " + name + '\n' + 
                     "damage: " + damage + '\n' + 
                     "element: " + element.ToString() + '\n' +
                     "type: " + type.ToString() + '\n' +
                     "direction: " + direction.ToString() + '\n' + 
                     "tiles affected: " + getNumOfTilesCovered() + '\n';
        if (additionalInfo != null)
        {
            ans += "info: " + additionalInfo;
        }
        return ans;
    }

    public bool canReach(Vector3 myPlace, Vector3 charPlace, LookDirection currLookDirection)
    {
        Debug.Log("my place: " + myPlace + ", attacked place: " +  charPlace);
        List<Vector3> tiles = getTilesCovered((int)myPlace.x, (int)myPlace.y, currLookDirection);
        foreach(Vector3 tile in tiles) 
            Debug.Log("hero? " + tile);
        return tiles.Contains(charPlace);
    }
}
