using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class WorldInteractables : MonoBehaviour
{
    public Sprite mainSprite;

    public string worldName;
    public abstract void Initialize();
    public abstract void showInteract();

    public abstract void interact();


    public static Tuple<Sprite, string> GetSpriteAndString(string line)
    {
        string charName = line.Split(':')[0];
        string say = line.Split(":")[1];
        Sprite talking = null;


        WorldInteractables worldCharacter = findByName(charName);

        if (worldCharacter != null)
            talking = worldCharacter.mainSprite;
        else if (WorldTextManager.instance.interactChar != null &&
            WorldTextManager.instance.interactChar.worldName.ToLower().Equals(charName.ToLower()))
            talking = WorldTextManager.instance.interactChar.mainSprite;
        return Tuple.Create(talking, say);
    }

    public static WorldInteractables findByName(string name)
    {
        foreach (var character in GameObject.FindObjectsOfType(typeof(WorldInteractables)))
        {
            WorldInteractables worldCharacter = (WorldInteractables)character;
            Debug.Log(worldCharacter.worldName + " " + name.ToLower());
            if (name.ToLower().Equals(worldCharacter.worldName.ToLower()))
                return worldCharacter;
        }

        return null;
    }

}
