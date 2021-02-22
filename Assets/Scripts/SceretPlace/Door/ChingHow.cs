using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChingHow : MonoBehaviour
{
    string answer = null;
    int chance = 0;
    public GameObject dialogManager;
    string[] text = new string[] {"그 요정님의 칭호도 아시나요?"
        , "잘했어요!"
        , "다시한번 잘 생각해봐요!"};

    private void Start()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (chance == 3 && answer == "듕이컹")
        {
            StartCoroutine(SetText_Succes());
        }
        else if (chance == 3 && answer != "듕이컹")
        {
            StartCoroutine(SetText_Fail());
        }
    }

    public void BtnClicked_DDyoong()
    {
        answer += "듕";
        chance++;
        Debug.Log("Chance " + chance);
    }
    public void BtnClicked_Ee()
    {
        answer += "이";
        chance++;
        Debug.Log("Chance " + chance);
    }
    public void BtnClicked_Kung()
    {
        answer += "컹";
        chance++;
        Debug.Log("Chance " + chance);
    }

    public void MissionSucceded()
    {
        Debug.Log("Succes!!! ChingHo!");
        //ClickCounter ++;
        dialogManager.GetComponent<DialogManager>().clickCount++;
        dialogManager.GetComponent<DialogManager>().DialogText_Hi();
        Destroy(gameObject);
    }
    public void MissionFailed()
    {
        Debug.Log("Failed!! and chance" + chance);
        //다시!
        answer = null;
        chance = 0;
    }

    IEnumerator SetText_Succes()
    {
        gameObject.GetComponentInChildren<Text>().text = text[1];
        yield return new WaitForSeconds(1.3f);
        MissionSucceded();
    }
    IEnumerator SetText_Fail()
    {
        MissionFailed();
        gameObject.GetComponentInChildren<Text>().text = text[2];
        yield return new WaitForSeconds(1.3f);
        gameObject.GetComponentInChildren<Text>().text = text[0];
    }
}
