using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Who : MonoBehaviour
{
    string answer = null;
    int chance = 0;
    string[] text = new string[] {"이곳을 알려준 크리스마스 요정님은 누구인가요?한글자씩 또박또박 클릭해 말해보세요 ^-^ "
        , "잘했어요!"
        , "다시한번 잘 생각해봐요!"};


    private void Start()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if(chance==4 && answer == "아뚜뚱이")
        {
            StartCoroutine(SetText_Succes());
        }
        else if(chance ==4 && answer != "아뚜뚱이")
        {
            StartCoroutine(SetText_Fail());
        }
    }

    public void OnBtnClick_Ah()
    {
        answer += "아";
        chance++;
        Debug.Log("Chance " + chance);
    }
    public void OnBtnClick_DDoo()
    {
        answer += "뚜";
        chance++;
        Debug.Log("Chance " + chance);
    }
    public void OnBtnClick_DDoong()
    {
        answer += "뚱";
        chance++;
        Debug.Log("Chance " + chance);
    }
    public void OnBtnClick_Ee()
    {
        answer += "이";
        chance++;
        Debug.Log("Chance " + chance);
    }

    public void MissionSucceded()
    {
        Debug.Log("Succes!!! Who!");
        //칭호우! 

        GameObject.Find("그의칭호는").transform.localScale = new Vector3(1f, 1f, 1f);
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
