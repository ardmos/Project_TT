using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 스테이지 종료시 결과 팝업을 띄워주는 스크립트.
/// 때가 오면 Init 메서드를 StageDirector에서 호출해줌으로써 실행된다.
/// </summary>

public class ResultPopupController : MonoBehaviour
{
    //변수들
    float endtime;
    int throwedtom, succeededtom, stageNumber;

    //별점 표시
    [SerializeField]
    GameObject star1, star2, star3;   
    //타임스탬프
    [SerializeField]
    Text min, sec;
    //소모한 토마토 갯수
    [SerializeField]
    Text throwedTomCount;
    //얻은 케챱
    [SerializeField]
    Text earnedKatchupCount;
    //얻은 토마토
    [SerializeField]
    Text earnedTomCount;
    //얻은 아이템 갯수
    [SerializeField]
    bool[] earnedItems;

    //팝업 진동애니메이션
    Animator resultPopupAnimator;


    void Start()
    {
        //resultPopupAnimator = gameObject.GetComponent<Animator>();        
    }

    //StageDirector에서 호출이 된다. 이 메서드가 실행됨으로써 결과팝업이 뜨게됨.
    public void Init(float endtime, int throwedtom, int succeededtom, int stageNumber, bool[] getitems)
    {
        resultPopupAnimator = gameObject.GetComponent<Animator>();
        this.endtime = endtime;
        this.throwedtom = throwedtom;
        this.succeededtom = succeededtom;
        this.stageNumber = stageNumber;
        this.earnedItems = getitems;
        min.text = "00";
        sec.text = "00";

        PrintStarImages();
        PrintTimeStamp();
        PrintThrowedTomCount();
        PrintEarnedKatchupCount();
        PrintEarnedTomCount();
    }

    #region Set

    #endregion


    #region 출력처리부분
    #region 별점 책정
    private int CalcStarScore(string err) 
    {
        //Debug.Log("CalcStarScore() : " + err);
        int s = 0;
        //Debug.Log("didCalc? : " + didCalculated);
        for (int n = 0; n < earnedItems.Length; n++)
        {
            //Debug.Log("for " + n);
            if (earnedItems[n] == true)
            {
                s++;
                //Debug.Log("s++ so now s is " + s);
            }
        }

        switch (s)
        {
            case 0:
                //Debug.Log("switch " + s);
                return 0;
            case 1:
                //Debug.Log("switch " + s);
                return 1;
            case 2:
                //Debug.Log("switch " + s);
                return 2;
            case 3:
                //Debug.Log("switch " + s);
                return 3;
            default:
                Debug.Log("CalcStarScore() Here!!! Please Check me!");
                return 0;
        }

    }
    private void PrintStarImages()
    {
        //Debug.Log("PrintStarImages()");
        switch (CalcStarScore("PrintStarImages()"))
        {
            case 0:
                //Debug.Log("switch P 0");
                resultPopupAnimator.SetTrigger("star0");
                break;
            case 1:
                //Debug.Log("switch P 1");
                resultPopupAnimator.SetTrigger("star1");
                break;
            case 2:
                //Debug.Log("switch P 2");
                resultPopupAnimator.SetTrigger("star2");
                break;
            case 3:
                //Debug.Log("switch P 3");
                resultPopupAnimator.SetTrigger("star3");
                break;
            default:
                Debug.LogError("PrintStarImages() or CalcStarScore() error");
                break;
        }
    }
    #endregion

    #region 타임스탬프
    private void PrintTimeStamp()
    {
        int imin, isec;
        imin = (int)endtime / 60;
        isec = (int)endtime % 60;
        if (imin < 10)
        {
            min.text = "0" + imin;
        }
        else
        {
            min.text = imin.ToString();
        }

        if (isec < 10)
        {
            sec.text = "0" + isec;
        }
        else
        {
            sec.text = isec.ToString();
        }
    }
    #endregion

    #region 던진 토마토 갯수 출력
    private void PrintThrowedTomCount()
    {
        throwedTomCount.text = throwedtom.ToString();
    }
    #endregion

    #region 얻은 케챱 출력
    private void PrintEarnedKatchupCount()
    {
        earnedKatchupCount.text = (throwedtom - succeededtom).ToString();
    }
    #endregion

    #region 얻은 토마토 출력
    private void PrintEarnedTomCount()
    {
        //Debug.Log((CalcStarScore("PrintEarnedTomCount()")).ToString());
        earnedTomCount.text = (CalcStarScore("PrintEarnedTomCount()")).ToString();
    }
    #endregion

    #endregion


    #region 얻은 보상들 저장부분. UserData에 저장.
    private void SavingRewards()
    {
        //GameObject userObj = GameObject.Find("User");
        User user_data = User.Instance;
        if (userObj != null)
        {
            user_data.AddingKatchups((throwedtom - succeededtom));
            user_data.AddingTomatoes(CalcStarScore("SavingRewards()"));
            user_data.SaveClearedStageStarScore(new User.StageStarScore(stageNumber, CalcStarScore("SavingRewards()")));
        }
    }    
    #endregion


    #region 완료버튼 기능. 스테이지리스트로 가기
    public void ToTheStageListMan()
    {
        SavingRewards();
        //끌 때 스테이지디렉터에 isalreadyPopUped 이거 false로 만들어주고 꺼야함. 완료시 스테이지리스트로.
        GameObject.Find("StageDirector").GetComponent<StageDirector>().SetIsAlreadyPopUped(false);
        //저장. 
        if (GameObject.Find("User") != null)
            GameObject.Find("User").GetComponent<User>().SaveUser();
        if (GameObject.Find("CrossFadeImage_Start") != null)
        {
            GameObject.Find("CrossFadeImage_Start").GetComponent<CrossFadeImage_Start>().StartCrossFadingImage();
        }
        else
        {
            SceneManager.LoadScene("StageList");
        }
    }
    #endregion

}
