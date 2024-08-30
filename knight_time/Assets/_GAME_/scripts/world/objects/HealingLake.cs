using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingLake : WorldInteractables
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        //mainSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        worldName = "HealingLake";
    }

    public override void showInteract()
    {
        WorldTextManager.instance.changeInteractText("Press E to Heal", this);
    }

    public override void interact()
    {

        MainHero.instance.heal();
        WorldTextManager.instance.clearInteractText();
        WorldTextManager.instance.clearDialog();
    }
}
