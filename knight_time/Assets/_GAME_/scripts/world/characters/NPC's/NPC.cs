using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : WorldCharacter
{
    public override void Initialize()
    {
        charType = CharacterType.NPC;
        mainSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
    }


    public override void showInteract()
    {
        WorldTextManager.instance.changeInteractText("Press E to Talk", this);
    }

    public override void takeDamage(int damage)
    {
    }

    void Start()
    {
        Initialize();
    }

}
