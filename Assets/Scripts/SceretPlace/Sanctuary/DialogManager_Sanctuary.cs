using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DialogManager_Sanctuary : MonoBehaviour
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
    [SerializeField]
    GameObject 요술봉, dialogObj, 좌측제너레이터0, 좌측제너레이터1, 좌측제너레이터2, 우측제너레이터0, 우측제너레이터1, 우측제너레이터2, 장난꾸러기, aura, cloud, finalPopUp;

    public int clickCount = 0;

 
    string god = "아뚜뚱이";
    string cute = "요정블자";
    //string girl = "혜인이";



    //다이얼로그 스크립트
    public string[,] dialogues;



    //성공, 실패 스택. 
    public int suc = 0;
    public int fai = 0;
    bool is13on = false;


    //마법시전
    bool magicStarted = false;
    int before = 0;
    int after = 0;
    float charging = 0;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = new string[,]{
        {god ,"좋아! 그럼 시작해볼까? \n강아지요정! 여기 예쁜 공주님한테 잘 설명해주라구!"}
        ,{cute , "... ..." }
            ,{ god, "강아지요정?"}
            ,{cute , "... ..." }
            ,{ god, "어이쿠, 이런!! \n큰일이군!  "}
            ,{ god, "우리 하얀 강아지 요정은 귀엽고 사랑스럽지만, 그만큼 장난꾸러기라서 말이야... \n마음에 드는 사람만 보면 장난치고싶어하거든. \n우리 혜인이가 마음에 든 모양이야."}
            ,{ god, "아무래도 지금도 혜인이한테 장난치고싶어서 장난감 생산라인에 올라간 것 같아"}
            ,{ god, "그나저나 이번 장난감들은 \n잘 분류되어야하는것들인데..."}
            ,{ god, "혹시나 섞이면 \n왼쪽나라로 가야할 장난감이 오른쪽 나라로 가고, 오른쪽 나라로 가야할 선물이 왼쪽 나라로 가고..."}//글씨 작게  8
            ,{ god, "그러면 아이들이 선물을 받지 못하게돼!!!"}//글씨 크게!   9
            ,{ god, "부탁해, 혜인이가 떨어지는 선물들을 잘 분류해줘!"}
            ,{ god, "자, 여기 요술봉이야. 이걸 화면을 터치해서 타이밍 맞게 휘두르면 장난감들을 '왼쪽 나라', '오른쪽 나라'로 분류할 수 있을거야!"}//요술봉 생김    11
            ,{ god, "그럼, 부탁할게!! 부디 아이들의 선물을 지켜줘!!!! xD "}// 시작.다이얼로그는 크기 0,0,0 or SetActive false,  제너레이터 시작(제네레이터들의 isOn True로. ) 12
            ,{ "", "제너레이터 가동!"} //13
            // ~~~  suc 300이상. 다이얼로그14 실행.  다이얼로그크기 1,1,1 제너레이터 중지. 
            ,{ god, "후... 이제 더 던질 장난감은 없을거야! 수고했어 혜인아!! 덕분에 살았어."}
            ,{ cute, "왕?!"}//강아지 추락 이벤트  15
            ,{ god, "오잉? \n헛! 강아지요정이 미끄러졌어!!!"}
            ,{ god, "큰일이야! 이렇게된 이상 혜인이가 마법을 쓰는수밖에 없어!"}
            ,{god, "시간이 없어! 빨리 요술봉을 좌우로 마구 휘둘러서 마법 에너지를 충전시킨 다음! 요술봉이 빛나면"}
            ,{god, "화면 가운데에 '우리만의 하트뿅 표시'를  그려! \n그럼 뭉게구름이 나와서 강아지요정을 구해낼거야!!"}
            ,{ god, "명심해! 요술봉을 마구 흔든 다음, '우리만의 하트뿅 표시'야!!"}//요술봉, 하트뿅 리스너.  20
            ,{ "","21은 마법시전이벤트다이얼로그종료"} //21은 마법시전 이벤트 다이얼로그 종료.,
            ,{ god, "좋아, 차징이 끝났어! 이제 화면에 우리만의 하트뿅 표시를 마음을 담아 그려봐!"}// 22.
            ,{ "","23는 하트그리기 이벤트 다이얼로그 종료"} //23는 하트그리기 이벤트 다이얼로그 종료
            ,{ cute, "어머...!!(낑!)"}    //강아지 구름에 추락씬   24
            ,{ god, "성공이야!!! 요 사고뭉치녀석! 혜인요정공주님이 아니었다면 정말 큰일날 뻔 했다구!!"}
            ,{ cute, "왕왕!!! (정말 고마워!! 내가 잠깐 인간의 언어를 한 건 잊어줘!! )"}
            ,{ "모두", "와!!! 정말 잘됐어!!!~~  (왕!!)"}//페이드아웃.끝.타이틀 화면으로 27

        };
        finalPopUp.SetActive(false);
        cloud.SetActive(false);
        aura.GetComponent<SpriteRenderer>().color = new Color(1f, 0.65f, 0.37f, 0f);
        장난꾸러기.SetActive(false);
        요술봉.SetActive(false);
        dialogMainCharacterObj.SetActive(false);
        dialogSubCharacterObj.SetActive(false);
        dialogOpponentCharacterObj.SetActive(false);
        arrowDown.enabled = false;
        DialogText_Hi();
        txtname.text = dialogues[clickCount, 0];
        dialogText.text = null;
        dialogText.DOText(dialogues[clickCount, 1], 1.5f, true, ScrambleMode.None);
        dialogMainCharacterObj.SetActive(true);
        clickCount++;
        StartCoroutine(ArrowDown(1.5f));
    }

    private void Update()
    {
        if (suc >= 1000 && is13on == false)
        {
            is13on = true;
            // ~~~  suc 300이상. 다이얼로그13 실행.  다이얼로그크기 1,1,1 <--- SetActive true로 바꿈. 제너레이터 중지.  <<   DialogText_Hi() 호출하면 됨. 
            제너레이터가동(false);
            clickCount++;
            dialogObj.SetActive(true);
            DialogText_Hi();
            
        }

        if (magicStarted)
        {
            //좌우로 마구 흔들어서 마법에너지를 충전. (구름이될 위치에 희미하게 에너지 생성 transparancy)   < Update에서.
            after = 요술봉.GetComponent<MagicWand>().swinged;
            charging = (after - before);
            aura.GetComponent<SpriteRenderer>().color = new Color(1f, 0.65f, 0.37f, charging / 10f);
            if (charging >= 10 && magicStarted == true)
            {                
                //차징 완료! 하트 차례! 다이얼로그로 알려주기!
                clickCount++;
                dialogObj.SetActive(true);
                print("차징완료!");
                DialogText_Hi();
                magicStarted = false;
            }
        }
        else
        {
            before = 요술봉.GetComponent<MagicWand>().swinged;
        }
    }

    #region Dialog Text 바꾸기
    public void DialogText_Hi()
    {

        if (arrowDown.enabled == true)
        {
            float durationTime = 1.5f;
            arrowDown.enabled = false;
            
            dialogText.text = null;


            switch (clickCount)
            {
                case 1:
                    dialogMainCharacterObj.SetActive(false);
                    txtname.text = dialogues[clickCount, 0];
                    dialogText.DOText(dialogues[clickCount, 1], 0.2f, true, ScrambleMode.None);
                    StartCoroutine(ArrowDown(0.2f));
                    clickCount++;
                    break;
                case 2:
                    잠깐빠르게(0.5f);
                    break;
                case 3:
                    잠깐빠르게(0.2f);
                    dialogMainCharacterObj.SetActive(false);
                    break;
                case 4:
                    잠깐빠르게(1f);
                    break;
                case 6:
                    잠깐빠르게(1f);
                    break;
                case 8:
                    //글씨 작게
                    dialogText.fontSize = 30;
                    잠깐빠르게(2f);
                    break;
                case 9:
                    //글씨 크게
                    dialogText.fontSize = 50;
                    잠깐빠르게(1f);
                    break;
                case 10:
                    dialogText.fontSize = 40;
                    잠깐빠르게(1f);
                    break;
                case 11:
                    //요술봉 생김
                    요술봉.SetActive(true);
                    정상속도(durationTime);
                    break;
                case 13:
                    //게임 시작
                    정상속도withoutCLickCountPlus(durationTime);
                    // 시작.다이얼로그는 크기 0,0,0 or SetActive false,  제너레이터 시작(제네레이터들의 isOn True로. )
                    dialogObj.SetActive(false);
                    제너레이터가동(true);
                    //게임 종료 후에 DialogText_Hi() 호출하면 됨. 
                    break;
                case 15:
                    //강아지 추락 이벤트
                    //dialogSubCharacterObj.SetActive(true);
                    잠깐빠르게(0.3f);                    
                    장난꾸러기.SetActive(true);
                    장난꾸러기.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    break;
                case 21:
                    정상속도withoutCLickCountPlus(durationTime);
                    //마법시전부분
                    마법시전();
                    break;
                case 22:
                    print("22");
                    정상속도(durationTime);
                    break;
                case 23:
                    정상속도withoutCLickCountPlus(durationTime);
                    하트그리러가기();
                    break;
                case 24:
                    //강아지 추락 이벤트
                    //dialogSubCharacterObj.SetActive(true);
                    장난꾸러기.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    정상속도(durationTime);
                    break;
                case 26:
                    dialogSubCharacterObj.SetActive(true);
                    정상속도(durationTime);
                    break;
                case 28:
                    //스테이지리스트로.  노노.  마지막 사진 땋!  
                    //SceneManager.LoadScene("StageList");
                    finalPopUp.SetActive(true);
                    finalPopUp.GetComponent<FinalPopUp>().StartFinalPopUp();
                    break;
                default:
                    정상속도(durationTime);
                    break;
            }
        }
    }

    private void 잠깐빠르게(float delay)
    {
        dialogMainCharacterObj.SetActive(true);
        txtname.text = dialogues[clickCount, 0];
        dialogText.DOText(dialogues[clickCount, 1], delay, true, ScrambleMode.None);
        StartCoroutine(ArrowDown(delay));
        Debug.Log(dialogues.Length / 2 + ", " + clickCount);
        if (clickCount >= dialogues.Length / 2)
            return;
        else
            clickCount++;
    }
    private void 정상속도(float delay)
    {
        dialogMainCharacterObj.SetActive(true);
        txtname.text = dialogues[clickCount, 0];
        dialogText.DOText(dialogues[clickCount, 1], delay, true, ScrambleMode.None);
        StartCoroutine(ArrowDown(delay));
        Debug.Log(dialogues.Length / 2 + ", " + clickCount);
        if (clickCount >= dialogues.Length / 2)
            return;
        else
            clickCount++;
    }
    private void 정상속도withoutCLickCountPlus(float delay)
    {
        dialogMainCharacterObj.SetActive(true);
        txtname.text = dialogues[clickCount, 0];
        dialogText.DOText(dialogues[clickCount, 1], delay, true, ScrambleMode.None);
        StartCoroutine(ArrowDown(delay));
        Debug.Log(dialogues.Length / 2 + ", " + clickCount);
    }

    private void 제너레이터가동(bool on)        //정지는 false;
    {
        좌측제너레이터0.GetComponent<Generator_Left>().isOn = on;
        좌측제너레이터1.GetComponent<Generator_Left>().isOn = on;
        좌측제너레이터2.GetComponent<Generator_Left>().isOn = on;
        우측제너레이터0.GetComponent<Generator_Right>().isOn = on;
        우측제너레이터1.GetComponent<Generator_Right>().isOn = on;
        우측제너레이터2.GetComponent<Generator_Right>().isOn = on;
    }

    private void 마법시전()
    {
        print("spell");
        //다이얼로그 비활성화
        dialogObj.SetActive(false);
        //좌우로 마구 흔들어서 마법에너지를 충전. (구름이될 위치에 희미하게 에너지 생성 transparancy)   < Update에서.
        magicStarted = true;
        //하트&터치 체크  charging 이 10 이상일 때.  << Update에서.
        //GameObject.Find("TouchListener").GetComponent<TouchListener>().isFullCharged = true;
        //구름생성 << 구름생성() 에서. 


        //clickCount++, 차징 해제. 마법 끝. 
        //magicStarted = false;
        //GameObject.Find("TouchListener").GetComponent<TouchListener>().isFullCharged = false;
        //다이얼로그 활성화.
        //DialogText_Hi() 호출
    }

    private void 하트그리러가기()
    {
        print("heart");
        dialogObj.SetActive(false);
        //하트&터치 체크  charging 이 10 이상일 때. 터치 인식 시작.
        GameObject.Find("TouchListener").GetComponent<TouchListener>().isFullCharged = true;
    }

    public void 구름생성(bool active)
    {
        print("구름생성");
        cloud.SetActive(active);
        //clickCount++, 차징 해제. 마법 끝. 
        clickCount++;
        
        GameObject.Find("TouchListener").GetComponent<TouchListener>().isFullCharged = false;
        //다이얼로그 활성화.
        dialogObj.SetActive(true);
        //DialogText_Hi() 호출
        DialogText_Hi();
    }
    #endregion
    IEnumerator ArrowDown(float delay)
    {
        yield return new WaitForSeconds(delay);
        arrowDown.enabled = true;
    }

    #region 문제들.
    //public void 
    #endregion
}

