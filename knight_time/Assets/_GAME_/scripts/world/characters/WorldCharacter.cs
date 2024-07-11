using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldCharacter : MonoBehaviour
{
    public CharacterType charType;

    public abstract void Initialize();
}
