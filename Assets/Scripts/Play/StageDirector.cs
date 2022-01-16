using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 하는 일
/// 1. 스테이지에서 유저에게 보여지는 UI들의 정보를 업데이트 해준다
/// 2. 게임 종료시 게임 결과 팝업 띄우기를 호출해준다. 
/// 3. (미구현) 과일을 더 받을 수 있는 보상형 광고 버튼의 기능 처리를 해준다. 
/// </summary>


public class StageDirector : MonoBehaviour
{
    #region 변수들
    // 타이머, 던진 토마토, 성공한 토마토, 생성된 케챱 실시간 기록하는 곳.

    //현재 스테이지 정보
    [SerializeField]
    int stageNumber;

    //타이머 관련 변수들
    [SerializeField]
    Text min, sec;
    int imin, isec;
    float time;

    //던진 토마토 갯수(발사시에 보고받음.), 성공한 토마토 갯수(성공토마토는 토마토에게 보고받음.)
    [SerializeField]
    private int throwedtom, succeededtom;
    //보유한 토마토 갯수  
    [SerializeField]
    Text tomCount;
    //생성된 케챱 (던진 토마토 갯수 - 성공한 토마토 갯수.(보통 1이겠지)). 결과창에서 계산.

    //획득한 코인들.
    [SerializeField]
    private bool[] getitems = new bool[3];

    //결과창 팝업1
    [SerializeField]
    GameObject canvas, resultPopupPref;
    private bool isalreadyPopUped;
    #endregion
 
    
    //현재 던져진 토마토 갯수도 표시해야함.


    void Start()
    {
        initGameObjects();
        min.text = "00";
        sec.text = "00";
        time = 0;
        throwedtom = 0;
        succeededtom = 0;
        //tomCount.text = "0";  유저데이터에서 읽어와야함
        isalreadyPopUped = false;
    }

    private void initGameObjects()
    {
        try
        {
            if (min == null)
            {
                min = GameObject.Find("min").GetComponent<Text>();
            }
            if (sec == null)
            {
                sec = GameObject.Find("sec").GetComponent<Text>();
            }
            if (tomCount == null)
            {
                tomCount = GameObject.Find("TomatoCount").GetComponent<Text>();
            }
        }
        catch (System.Exception)
        {
            //Debug.Log("뭔가문제임!!!");
            throw new System.Exception("뭔가!!! 뭔가 문제임!!! 도움!!");
            //throw;
        }        
    }

    void Update()
    {
        time += Time.deltaTime;
        imin = (int)time / 60;
        isec = (int)time % 60;


        #region 게임 최대시간 60분 초과시.
        if(imin == 60)
        {
            if (!isalreadyPopUped)
            {
                //결과창 띄우기
                PopUpResult();
            }  
        }
        #endregion

        #region 분 초 표기
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
        #endregion

        #region 보유한 토마토 갯수 실시간 업데이트
        if(GameObject.Find("User"))
            tomCount.text = GameObject.Find("User").GetComponent<User>().GetTomatoesCount().ToString();
        #endregion

    }


    #region Get 모음
    public int GetSucceededTom()
    {
        return succeededtom;
    }
    public int GetThrowedTom()
    {
        return throwedtom;
    }
    #endregion

    #region Set 모음
    #region 성공토마토 보고받는곳
    public void AddSucceededTom()
    {
        succeededtom++;
        //결과창 출력(출력시에 케챱 계산.)
        if (!isalreadyPopUped){
            PopUpResult();
        }        
    }
    #endregion

    #region 발사시에 보고받는곳
    public void AddThrowedTom()
    {
        throwedtom++;
    }
    #endregion

    #region isalreadyPopupUped
    public void SetIsAlreadyPopUped(bool b)
    {
        isalreadyPopUped = b;
    }
    #endregion

    public void SetHaveCoin(int i)
    {
        getitems[i] = true; 
    }
    #endregion

    #region Popup Result Scene
    private void PopUpResult()
    {
        isalreadyPopUped = true;
        GameObject resultPopup = Instantiate(resultPopupPref) as GameObject;
        resultPopup.transform.SetParent(canvas.transform);
        resultPopup.GetComponent<RectTransform>().localPosition = Vector3.zero;
        resultPopup.GetComponent<ResultPopupController>().Init(time, throwedtom, succeededtom, stageNumber, getitems);
    }
    #endregion

    #region 추가토마토 광고버튼 기능

    #endregion

}
