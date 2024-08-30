using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : WorldInteractables
{
    // Start is called before the first frame update

    public static bool canContinue = false;
    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        mainSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        worldName = "Trash";
    }

    public override void showInteract()
    {
        WorldTextManager.instance.changeInteractText("Press E to Interact", this);
    }

    public override void interact()
    {
        Debug.Log(canContinue + " " + TownSceneManager.instance.trashCount);
        if (TownSceneManager.instance.lakeSpoken)
        {
            TownSceneManager.instance.trashDialog(this.gameObject.name);
            TownSceneManager.instance.lakeSpoken = false;
        }
        else if(canContinue && TownSceneManager.instance.trashCount <= 3)
        {
            TownSceneManager.instance.trashDialog(this.gameObject.name);
        }
        else
        {
            WorldCharacter.readFromScript("TrashDialogDefault", "trash");
            Manager.instance.spacePressed.Add(() =>
            {
                WorldTextManager.instance.clearDialog();
            });
        }

    }
}
