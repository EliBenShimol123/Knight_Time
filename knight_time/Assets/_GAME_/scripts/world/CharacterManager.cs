using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    public string createdScene;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            createdScene = SceneManager.GetActiveScene().name;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            if (!instance.createdScene.Equals(SceneManager.GetActiveScene().name) &&
                !SceneManager.GetActiveScene().name.Equals("Fighting_Scene")) {//not in same scene and not in fighting
                Destroy(instance.gameObject);
                instance = this;
                createdScene = SceneManager.GetActiveScene().name;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        Debug.Log("characters");
    }

    private void MergeChildren()
    {
        // Get the existing instance's children
        Transform existingTransform = instance.transform;

        // Get the new instance's children
        Transform newTransform = transform;

        // Move new instance's children to the existing instance
        foreach (Transform child in newTransform)
        {
            Debug.Log(child.name);
            child.SetParent(existingTransform);
        }
    }

    }
