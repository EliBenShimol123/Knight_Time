using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldTextManager : MonoBehaviour
{

    public static WorldTextManager instance;
    [SerializeField] private GameObject interact_text;
    [SerializeField] private GameObject enterName;
    [SerializeField] public GameObject healthBar;
    public WorldInteractables interactChar;
    public bool inInteract = false;

    //for dialog
    [SerializeField] private GameObject dialog_box;
    private TMP_Text dialog_text;
    private string writer;

    private float delayBeforeStart = 0f;
    private float timeBtwChars = 0.07f;
    private string leadingChar = "";
    private bool leadingCharBeforeDelay = false;

    public bool skipDialog = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            dialog_text = dialog_box.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
            clearInteractText();
            clearDialog();

        }
        Debug.Log("text");
    }


    public void changeInteractText(string txt, WorldInteractables character)
    {
        interact_text.gameObject.SetActive(true);
        interact_text.transform.GetChild(0).GetComponent<TMP_Text>().text = txt;
        interactChar = character;
    }
    public void clearInteractText()
    {
        if (interact_text != null)
        {
            interact_text.gameObject.SetActive(false);
            interact_text.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            interactChar = null;
        }
    }

    public void startInteract()
    {
        inInteract = true;
        interactChar.interact();
        //clearInteractText();
    }

    public void writeDialogText(Sprite image, string txt)
    {
        dialog_box.gameObject.SetActive(true);
        dialog_box.transform.GetChild(0).GetComponent<Image>().sprite = image;
        writer = txt;
        StartCoroutine("TypeWriterTMP");
    }

    public void clearDialog()
    {
        dialog_box.gameObject.SetActive(false);
        dialog_text.text = "";
        if(MainHero.instance != null)
            MainHero.instance.canMove = true;
        inInteract = false;
        skipDialog = false;
    }

    public void showCanSkip()
    {
        Manager.instance.canSkip = true;
        dialog_box.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void hideCanSkip()
    {
        Manager.instance.canSkip = false;
        dialog_box.transform.GetChild(2).gameObject.SetActive(false);
    }


    IEnumerator TypeWriterTMP()
    {
        hideCanSkip();
        MainHero.instance.playSound("typing", writer.Length * timeBtwChars, 0.2f);
        dialog_text.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

        for (int i = 0; i<writer.Length && !skipDialog; i++)
        {
            if (dialog_text.text.Length > 0)
            {
                dialog_text.text = dialog_text.text.Substring(0, dialog_text.text.Length - leadingChar.Length);
            }
            dialog_text.text += writer[i];
            dialog_text.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            dialog_text.text = dialog_text.text.Substring(0, dialog_text.text.Length - leadingChar.Length);
        }
        if (skipDialog)
        {
            dialog_text.text = writer;
            skipDialog = false;
        }
        showCanSkip();
    }



    public void showEnterName()
    {
        inInteract = true;
        enterName.gameObject.SetActive(true);
        MainHero.instance.canMove = false;
    }

    public void nameEntered()
    {
        MainHero.instance.canMove = true;

        MainHero.instance.loadName(enterName.transform.GetChild(0).GetComponent<TMP_InputField>().text);
       
        enterName.gameObject.SetActive(false);
        inInteract = false;

        if (interactChar != null)
        {
            startInteract();
        }
    }
}
