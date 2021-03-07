﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    [SerializeField]
    GameObject tomatoPos, tomatoPrefab, rope_Arm, stageDirector;
    GameObject cur_reloadedTomato;
    //차징 위한 모터 스피드. 맥스스피드
    public float mSpeed, maxSpeed, speedUnit;
    HingeJoint2D hingeJoint2D;
    JointMotor2D jointMotor2D;

    //회전방향, 장전여부, 차징중인지
    [SerializeField]
    bool right, isReloaded, afterFire;

    //스윙스위칭 버튼 식별 위한 부분. 
    [SerializeField]
    GraphicRaycaster gr;

    //유저 데이터
    GameObject userObj;
    User userData;

    // Start is called before the first frame update
    void Start()
    {
        Inits();        
    }

    #region 초기화

    private void Inits()
    {
        isReloaded = false;
        right = true;  //기본은 우회전.
        afterFire = false;
        //ReloadingTomato();
        mSpeed = 0;
        maxSpeed = 18;
        speedUnit = 30;
        hingeJoint2D = rope_Arm.GetComponent<HingeJoint2D>();
        jointMotor2D = hingeJoint2D.motor;

        if (GameObject.Find("User"))
        {
            userObj = GameObject.Find("User");
            userData = userObj.GetComponent<User>();
        }
    }

    #endregion


    // Update is called once per frame
    void Update()
    {
        if (afterFire)
        {
            //None Charging State
            CoolingDown();
            jointMotor2D.motorSpeed = speedUnit * mSpeed;
            hingeJoint2D.motor = jointMotor2D;
        }
        else
        {
            //Charging State
            jointMotor2D.motorSpeed = speedUnit * mSpeed;
            hingeJoint2D.motor = jointMotor2D;
            ChargeAndShoot();
        }
    }

    
    #region 발사 후 쿨다운
    private void CoolingDown()
    {
        if (mSpeed > 0)
        {
            //좌회전 진정
            mSpeed--;
        }
        else if (mSpeed < 0)
        {
            //우회전 진정
            mSpeed++;
        }
        else
        {
            afterFire = false;
        }
    }
    #endregion

    #region 차징, 발사 부분

    private void ChargeAndShoot()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            #region 장전되어있는데 터치 이벤트
            if (isReloaded)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        //돌리기 시작
                        //print("Start!");
                        break;
                    case TouchPhase.Stationary:
                        //차징
                        afterFire = false;
                        switch (right)
                        {
                            case true:
                                if (mSpeed < maxSpeed)
                                    //mSpeed++;
                                    mSpeed += 0.1f;
                                break;
                            case false:
                                if (-maxSpeed < mSpeed)
                                    //mSpeed--;
                                    mSpeed -= 0.1f;
                                break;
                        }
                        //print("Charge!");
                        break;
                    case TouchPhase.Ended:
                        afterFire = true;
                        //발사! 
                        cur_reloadedTomato.GetComponent<Tomato>().CallThisWhenImShooted();
                        cur_reloadedTomato.GetComponent<RelativeJoint2D>().enabled = false;
                        isReloaded = false;
                        stageDirector.GetComponent<StageDirector>().AddThrowedTom();    //디렉터에게 발사 보고.
                        if (GameObject.Find("User"))
                        {
                            //유저데이타에도 던짐 보고.
                            userData.TomatoHaveThrowed();
                        }
                        break;
                }

            }
            #endregion  장전되어있는데 터치 이벤트

            #region 장전되어있지 않은 경우 터치 이벤트
            else
            {
                var ped = new PointerEventData(null);
                ped.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();  //여기에 히트된 개체 저장.
                gr.Raycast(ped, results);

                if(results.Count>0)
                {
                    GameObject obj = results[0].gameObject; //가장 위에 있는 UI부터 [0]~ 순으로 저장된다.
                    if (obj.CompareTag("SwitchingSwingDirection"))
                    {
                        //Debug.Log("SwitchingSwingDirection!!!! Yea!!!!!!!");
                    }
                    else
                    {
                        //Debug.Log(obj.tag);
                    }
                }
                else
                {
                    if ((touch.phase == TouchPhase.Began))
                    {
                        ReloadingTomato();
                    }
                }
            }                           
            #endregion  장전되어있지 않은 경우 터치 이벤트
        }
    }
    #endregion

    #region 재장전
    private void ReloadingTomato()
    {
        //남은 토마토가 없으면 장전 못해야함. 남은 토마토 체크 부분
        if (userData.GetTomatoesCount() <= 0)
        {
            Debug.Log("과일이 모자랍니다!");
        }
        else
        {
            GameObject tomato = Instantiate(tomatoPrefab) as GameObject;
            tomato.transform.position = tomatoPos.transform.position;
            //tomato.transform.SetParent(GameObject.Find("Catapult").transform);
            tomato.GetComponent<RelativeJoint2D>().connectedBody = GameObject.Find("Rope").GetComponent<Rigidbody2D>();
            cur_reloadedTomato = tomato;
            isReloaded = true;
            print("Reload Success");
        }
    }
    #endregion

    #region 몽딩이 회전 방향 토글버튼 기능.
    public void ToggleTurnRorL()
    {
        right = !right;        
    }
    #endregion
}
