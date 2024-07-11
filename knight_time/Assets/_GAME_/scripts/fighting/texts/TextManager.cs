using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TextManager : MonoBehaviour
{

    public static TextManager instance;

    [SerializeField] private Button moveButton;
    [SerializeField] private GameObject moveBtnParent;
    [SerializeField] private TMP_Text description_text;
    [SerializeField] private Button attackBtn;
    [SerializeField] private Button healBtn;
    [SerializeField] private Button defendBtn;
    [SerializeField] private Button positionBtn;
    [SerializeField] private Button escBtn;
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;

    private int startingX = -215;
    private int startingY = 25;
    private Dictionary<string, Button> moves = new Dictionary<string, Button>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        Debug.Log("text");
    }

    public void createMoves(List<string> movesNames)
    {
        if (moves.Count == 0)
        {
            for (int i = 0; i < movesNames.Count; i++)
            {
                Button newMove = Instantiate(moveButton, moveBtnParent.transform);
                newMove.gameObject.SetActive(true);

                RectTransform rectTransform = newMove.GetComponent<RectTransform>();
                if (i % 2 == 0)
                {
                    rectTransform.anchoredPosition = new Vector3(startingX + 215 * (i / 2), startingY);
                }
                else
                    rectTransform.anchoredPosition = new Vector3(startingX + 215 * (i / 2), startingY - 50);

                newMove.transform.GetChild(0).GetComponent<TMP_Text>().text = movesNames[i];

                //Button buttonComponent = newMove.GetComponent<Button>();
                moves[movesNames[i]] = newMove;
                newMove.onClick.AddListener(() =>
                {
                    bool attack = FightingManager.instance.hero.SelectedMove(newMove.transform.GetChild(0).GetComponent<TMP_Text>().text);
                    if (attack)
                        FightingManager.instance.switchMethod(Method.Attack);
                });


            }
        }
        else
            foreach (var move in moves.Values)
                move.gameObject.SetActive(true);


        returnButton(() => FightingManager.instance.hero.turn());
        //escBtn.gameObject.SetActive(true);
        //escBtn.onClick.RemoveAllListeners();
        //escBtn.onClick.AddListener(() => FightingManager.instance.hero.turn());
    }

    public void returnButton(Action action)
    {
        escBtn.gameObject.SetActive(true);
        escBtn.onClick.RemoveAllListeners();
        escBtn.onClick.AddListener(() => { escBtn.gameObject.SetActive(false); action(); } );
    }


    public void hideMoves()
    {
        foreach (var move in moves.Values)
            move.gameObject.SetActive(false);
        escBtn.gameObject.SetActive(false);
    }


    //for user turn choices
    public void showChoices()
    {
        Debug.Log("show");
        attackBtn.gameObject.SetActive(true);
        healBtn.gameObject.SetActive(true);
        defendBtn.gameObject.SetActive(true);
        positionBtn.gameObject.SetActive(true);
    }
    public void clearChoices()
    {
        Debug.Log("clear");
        attackBtn.gameObject.SetActive(false);
        healBtn.gameObject.SetActive(false);
        defendBtn.gameObject.SetActive(false);
        positionBtn.gameObject.SetActive(false);
    }

    public void showMakeSure(string yesTxt, string noTxt, Action yesAction, Action noAction)
    {
        yesBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = yesTxt;
        noBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = noTxt;

        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => yesAction());
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => { noAction(); hideMakeSure(); });

        yesBtn.gameObject.SetActive(true);
        noBtn.gameObject.SetActive(true);
    }

    public void hideMakeSure()
    {
        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);
    }

    public void changeText(string txt, int size)
    {
        description_text.text = txt;
        description_text.fontSize = size;
    }
    public void clearText()
    {
        description_text.text = "";
    }

    public string getText()
    {
        return description_text.text;
    }

    public int getFontSize()
    {
        return (int)description_text.fontSize;
    }

}
