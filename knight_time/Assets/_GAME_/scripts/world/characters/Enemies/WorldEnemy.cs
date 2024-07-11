using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldEnemy : WorldCharacter
{

    public override void Initialize()
    {
        charType = CharacterType.ENEMY;
    }
}
