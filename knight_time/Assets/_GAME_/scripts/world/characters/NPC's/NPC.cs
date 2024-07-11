using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : WorldCharacter
{
    public override void Initialize()
    {
        charType = CharacterType.NPC;
    }


    public abstract void interact();
}
