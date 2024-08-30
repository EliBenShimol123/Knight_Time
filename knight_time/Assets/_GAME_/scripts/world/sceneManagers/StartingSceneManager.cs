using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSceneManager : MonoBehaviour
{
    public static StartingSceneManager instance;
    public bool merlinDone;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            merlinDone = false;

        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("world");
    }


}
