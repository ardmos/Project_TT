using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollSensor : MonoBehaviour
{
    Vector2 startPos, newPos, currentpos, deltapos;

    public string message { get; set; }

    Vector3 movePos, wstartPos, wnewPos; 
    float moveYdistance;
    [SerializeField]
    GameObject mcamera, stageListSceneManager;
    [SerializeField]
    ImageWipe imageWipeObj;


    void Update()
    {
        ScrollTouchRec();
    }

    #region 터치 인식
    private void ScrollTouchRec()
    {
        //print("Touch: " + message + ", moved distance: " + (newPos-startPos));

        #if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //스테이지 하우스 터치된게 아닐경우, 스크롤로 처리. 
            #region 스테이지 하우스들 터치 인식


            currentpos = touch.position;



            TouchStageButtonCheck(currentpos);
            #endregion



            //스크롤로 처리
            #region 터치스크롤 부분

            deltapos = touch.deltaPosition;
            //currentpos = touch.position;

            wstartPos = Camera.main.ScreenToWorldPoint(new Vector3(deltapos.x, deltapos.y));
            wnewPos = Camera.main.ScreenToWorldPoint(new Vector3(currentpos.x, currentpos.y));

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = wnewPos - wstartPos;
                    message = "Began ";
                    break;
                case TouchPhase.Moved:
                    newPos = wnewPos - wstartPos;
                    message = "Moved";
                    movePos = (Vector3)(newPos - startPos);
                    //민감도 조절 부분
                    if (movePos.y > 0)
                    {
                        moveYdistance = movePos.y + 0.1f;
                    }
                    else if (movePos.y < 0)
                    {
                        moveYdistance = movePos.y - 0.1f;
                    }

                    if ((mcamera.transform.position.y - moveYdistance) >= 0 && (mcamera.transform.position.y - moveYdistance) <= 30)
                        mcamera.transform.Translate(new Vector3(0, -moveYdistance));
                    startPos = newPos;
                    break;
                default:
                    break;
            }
            #endregion
        }
        #endif

        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentpos = Input.mousePosition;

            TouchStageButtonCheck(currentpos);
        }

        #endif

 
    }

    private void TouchStageButtonCheck(Vector2 pos)
    {
        pos = Camera.main.ScreenToWorldPoint(pos);

        RaycastHit2D hit = Physics2D.Raycast(pos, transform.forward, 15f);
        Debug.DrawRay(pos, transform.forward * 10, Color.red, 0.3f);

        if (hit == true)
        {
            switch (hit.transform.tag)
            {
                case "Stage0":
                    //SceneManager.LoadScene("Stage0");
                    imageWipeObj.StartImageWipe("Stage0", 0);
                    break;
                case "Stage1":
                    MoveToGameStageScene(1, "Stage1", 1);
                    break;
                case "Stage2":
                    MoveToGameStageScene(2, "Stage2", 2);
                    break;
                case "Stage3":
                    MoveToGameStageScene(3, "Stage3", 0);
                    break;
                case "Stage4":
                    MoveToGameStageScene(4, "Stage4", 1);
                    break;
                case "Stage5":
                    break;
                case "SecretSanctuary":
                    // 비밀 이벤트 지역!
                    break;

                default:
                    break;
            }
        }
    }

    private void MoveToGameStageScene(int selected_house_num, string stage_name, int wipe_anim_num)
    {
        if (stageListSceneManager.GetComponent<StageListSceneManager>().houses[selected_house_num].pline.enabled == false)
            //SceneManager.LoadScene("Stage2");
            imageWipeObj.StartImageWipe(stage_name, wipe_anim_num);
        else
            print("pline!!!");
    }
    #endregion
}
