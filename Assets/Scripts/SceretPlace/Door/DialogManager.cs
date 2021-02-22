using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DialogManager : MonoBehaviour
{

    [SerializeField]
    Text txtname;
    [SerializeField]
    Text dialogText;
    [SerializeField]
    Image arrowDown;
    [SerializeField]
    GameObject dialogMainCharacterObj, dialogSubCharacterObj, dialogOpponentCharacterObj;
    [SerializeField]
    Sprite[] characterImages;

    public int clickCount=0;

    string unknown = "...";
    string god = "아뚜뚱이";
    string cute = "블자";
    //혜인이 string girl = "혜인이";
    public string[,] textsHi;


    // Start is called before the first frame update
    void Start()
    {
        textsHi = new string[,]{
        {unknown ,"으으... 뭐야 한참 단잠을 자던 중에!"}
        ,{unknown , "오잉? 넌 누구야? 여긴 누가 알려줬어?? 이곳은 요정밖에 모르는곳인데!" } //이름, 칭호 맞추기 팝업. 
            ,{ unknown , "그 요정님과 아는 사이라니 정말 대단한걸!"}
        , { unknown , "정말 위대하고, 또 멋지고, 또 잘생기고, 거기다 성격도 좋고, 운동도 잘하고, 섹시하고, 게다가 세심하고, 무려 똑똑하기까지하고, ...\n ... 말하기 입아프군...  아무튼 완벽한 요정이랑 말이야!"}
        , { unknown, "뭐야, 표정이 왜그래?                                  "}
            , { unknown, "아무튼 슬슬 내가 누구인지 알려줄 때가 된 듯 싶군!"}              
             //등장 애니메이션
            , { god, "짜-잔!! \n그래, 내가 바로 그 아뚜뚱이야!!! xD 이제부터 우리가 여행을 안내해줄거야! " }
             //등장 애니메이션
            , { cute, "왕! (잘 따라오라구!)" }
        };
        dialogMainCharacterObj.SetActive(false);
        dialogSubCharacterObj.SetActive(false);
        dialogOpponentCharacterObj.SetActive(false);
        arrowDown.enabled = false;
        DialogText_Hi();
        txtname.text = textsHi[clickCount, 0];
        dialogText.text = null;
        dialogText.DOText(textsHi[clickCount, 1], 1.5f, true, ScrambleMode.None);
        clickCount++;
        StartCoroutine(ArrowDown(1.5f));
    }

    #region Dialog Text 바꾸기
    public void DialogText_Hi()
    {

        if (arrowDown.enabled == true)
        {
            float durationTime = 1.5f;
            arrowDown.enabled = false;

            switch (clickCount)
            {
                case 1:
                    정상속도(durationTime);
                    clickCount--;
                    //이름 누가 알려줬는가 팝업. 정답을 맞출 시 팝업은 닫히면서 clickCOunt를 늘려준다. 
                    StartCoroutine(DelayPopup(durationTime));
                    break;
                case 4:
                    잠깐빠르게(0.5f);
                    break;
                case 6:
                    정상속도(durationTime);
                    //듕이컹 스프라이트 등장 쾅!
                    dialogMainCharacterObj.SetActive(true);
                    //dialogMainCharacterObj.GetComponent<Image>().DOPlay();                 
                    break;
                case 7:
                    잠깐빠르게(0.5f);
                    //강아지 스프라이트 등장 쾅! 
                    dialogSubCharacterObj.SetActive(true);
                    break;
                case 8:
                    SceneManager.LoadScene("Sanctuary");
                    break;
                default:
                    정상속도(durationTime);
                    break;
            }
        }
    }

    private void 잠깐빠르게(float delay)
    {
        txtname.text = textsHi[clickCount, 0];
        dialogText.text = null;
        dialogText.DOText(textsHi[clickCount, 1], delay, true, ScrambleMode.None);
        StartCoroutine(ArrowDown(delay));
        Debug.Log(textsHi.Length/2 + ", " + clickCount);
        if (clickCount >= textsHi.Length / 2)
            return;
        else
            clickCount++;
    }
    private void 정상속도(float delay)
    {
        txtname.text = textsHi[clickCount, 0];
        dialogText.text = null;
        dialogText.DOText(textsHi[clickCount, 1], delay, true, ScrambleMode.None);
        StartCoroutine(ArrowDown(delay));
        Debug.Log(textsHi.Length/2 + ", " + clickCount);
        if (clickCount >= textsHi.Length / 2)
            return;
        else
            clickCount++;
    }
    #endregion
    IEnumerator DelayPopup(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(GameObject.Find("누가알려줬지") != null)
            GameObject.Find("누가알려줬지").transform.localScale = new Vector3(1f, 1f, 1f);
    }

    IEnumerator ArrowDown(float delay)
    {
        yield return new WaitForSeconds(delay);
        arrowDown.enabled = true;
    }

    #region 문제들.
    //public void 
    #endregion
}
