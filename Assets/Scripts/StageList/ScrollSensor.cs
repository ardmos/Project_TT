using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollSensor : MonoBehaviour
{
    Vector2 startPos, newPos, pos, deltapos;

    public string message { get; set; }

    Vector3 movePos, wstartPos, wnewPos; 
    float moveYdistance;
    [SerializeField]
    GameObject mcamera, stageListSceneManager;
    [SerializeField]
    ImageWipe imageWipeObj;

    //For SecretPlace
    int tab=0;
    //

    void Update()
    {
        ScrollTouchRec();
    }

    #region 터치 인식
    private void ScrollTouchRec()
    {
        //print("Touch: " + message + ", moved distance: " + (newPos-startPos));
        
        //스테이지 하우스 터치된게 아닐경우, 스크롤로 처리. 
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //여기서 스테이지하우스 터치인지 처리하고, 아닐경우 아래 처리하도록. 
            #region 스테이지 하우스들 터치 인식
            pos = touch.position;
            pos = Camera.main.ScreenToWorldPoint(pos);

            RaycastHit2D hit = Physics2D.Raycast(pos, transform.forward, 15f);
            Debug.DrawRay(pos, transform.forward * 10, Color.red, 0.3f);

            if(hit == true)
            {                
                switch (hit.transform.tag)
                {
                    case "Stage0":
                        //SceneManager.LoadScene("Stage0");
                        imageWipeObj.StartImageWipe("Stage0", 0);
                        break;
                    case "Stage1":
                        if (stageListSceneManager.GetComponent<StageListSceneManager>().houses[1].pline.enabled == false)
                            //SceneManager.LoadScene("Stage1");
                            imageWipeObj.StartImageWipe("Stage1", 1);
                        else
                            print("pline!!!");
                        break;
                    case "Stage2":
                        if (stageListSceneManager.GetComponent<StageListSceneManager>().houses[2].pline.enabled == false)
                            //SceneManager.LoadScene("Stage2");
                            imageWipeObj.StartImageWipe("Stage2", 2);
                        else
                            print("pline!!!");
                        break;
                    case "Stage3":
                        break; 
                    case "Stage4":
                        break;
                    case "Stage5":
                        break;
                    case "SecretSanctuary":
                        // 비밀 이벤트 지역! 우리의 비밀 공간
                        tab++;
                        print("Tabed!! : "+ tab);
                        if (tab >= 3)
                        {
                            SceneManager.LoadScene("Door");
                        }
                        break;

                    default:
                        break;
                }
            }
            #endregion




            #region 터치스크롤 부분
            deltapos = touch.deltaPosition;
            pos = touch.position;
            wstartPos = Camera.main.ScreenToWorldPoint(new Vector3(deltapos.x, deltapos.y));
            wnewPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y));

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
    }
    #endregion
}
