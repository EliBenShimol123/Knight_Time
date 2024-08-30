using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public abstract class WorldCharacter : WorldInteractables
{
    public CharacterType charType;

    public int maxHealth;
    public int health;

    public abstract void takeDamage(int damage);


    public static void readFromScript(string scriptName, string characterName)
    {
        //string filePath = Path.Combine(Application.dataPath, "Resources", "dialogs", characterName, scriptName + ".txt");
        string filePath = "dialogs/" + characterName + '/' + scriptName;
        Debug.Log(filePath);
        TextAsset textAsset = Resources.Load<TextAsset>(filePath);
        if (textAsset != null)
        {
            string[] lines = textAsset.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            string line = lines[0].Replace("!name!", MainHero.instance.selectedName);
            Debug.Log(line);
            var valForLine = GetSpriteAndString(line);
            WorldTextManager.instance.writeDialogText(valForLine.Item1, valForLine.Item2);

            for (int i = 1; i < lines.Count(); i++)
            {
                line = lines[i].Replace("!name!", MainHero.instance.selectedName);
                valForLine = GetSpriteAndString(line);
                Sprite talking = valForLine.Item1;
                string say = valForLine.Item2;
                Manager.instance.spacePressed.Add(() =>
                {
                    WorldTextManager.instance.writeDialogText(talking, say);
                });
            }
        }
        else
        {
            Debug.Log("can't find " + filePath);
        }
    }



    public void playSound(string clipName, float duration, float volume)
    {
        AudioSource source = Manager.instance.GetComponent<AudioSource>();

        AudioClip clip = Resources.Load<AudioClip>("sounds/" + clipName);

        Manager.instance.GetComponent<AudioSource>().clip = clip;
        Manager.instance.GetComponent<AudioSource>().loop = true;
        Manager.instance.GetComponent<AudioSource>().volume = volume;

        StartCoroutine(PlayAudioForSpecifiedDuration(duration));
    }

    private IEnumerator PlayAudioForSpecifiedDuration(float duration)
    {
        // Play the audio
        Manager.instance.GetComponent<AudioSource>().Play();

        // Wait for the specified duration
        float startTime = Time.time;

        while (Time.time - startTime < duration && !WorldTextManager.instance.skipDialog)
        {
            yield return null; 
        }

        // Stop the audio
        Manager.instance.GetComponent<AudioSource>().Stop();
    }

    public void stopSound()
    {
        Manager.instance.GetComponent<AudioSource>().Stop();
    }
}
