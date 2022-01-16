using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// StageList Scene에서 화면 스크롤 처리를 담당하는 스크립트. 
/// 
/// 하는 일
/// 1. 터치를 감지해 화면을 스크롤 한다. 
/// 2. 유저가 스테이지 버튼을 클릭했을 시 해당 스테이지로 씬 전환을 한다. 
/// 3. StageList 씬을 떠나기 전에는 현재 메인카메라의 위치를 저장한다. (나중에 StageList씬에 돌아왔을 때 카메라 위치를 설정하는데 쓰임)
/// </summary>

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

            currentpos = touch.position;

            //TouchPhase.Began 타이밍에 스테이지 하우스 터치된게 아닐경우, 스크롤로 처리. 
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

                    #region 스테이지 하우스들 터치된경우 인식
                    TouchStageButtonCheck(currentpos);
                    #endregion

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
            string hit_obj_tag = hit.transform.tag;

            //스테이지 하우스를 터치했을 경우. True 반환하면서 스테이지 이동
            if (hit_obj_tag.Contains("Stage"))
            {
                Debug.Log("char hit_obj_tag[hit_obj_tag.Length - 1] : " + hit_obj_tag[hit_obj_tag.Length - 1]);
                MoveToGameStageScene(hit_obj_tag[hit_obj_tag.Length - 1]-'0', hit_obj_tag, Random.Range(0, 3));
                //return true;
            }

            /*
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
            */
        }
        //return false;
    }

    private void MoveToGameStageScene(int selected_house_num, string stage_name, int wipe_anim_num)
    {
        try
        {
            //씬 넘어가기 전에 카메라 pos 저장
            GameObject.FindObjectOfType<User>().SaveCameraPosition(Camera.main.transform.position);

            if (stageListSceneManager.GetComponent<StageListSceneManager>().houses[selected_house_num].pline.enabled == false)
                //SceneManager.LoadScene("Stage2");
                imageWipeObj.StartImageWipe(stage_name, wipe_anim_num);
            else
                print("pline!!!");
        }
        catch (System.Exception)
        {
            throw new System.Exception("selected_house_num: " + selected_house_num + ", stage_name: " + stage_name + ", wipe_anim_num: " + wipe_anim_num);
        }

    }
    #endregion
}
