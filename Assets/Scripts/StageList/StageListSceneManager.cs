using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Stage List Scene Manager. 
/// 
/// 하는 일
/// 1. 유저가 스테이지 클리어시 받은 점수를 스테이지하우스 위에 별표로 표시해준다. 
/// 2. 별표가 1개 이상이면 다음 스테이지를 개방해준다. 
/// </summary>


public class StageListSceneManager : MonoBehaviour
{
    [Serializable]
    public class House
    {
        public SpriteRenderer[] stars;
        //Sprite stageName;
        public SpriteRenderer pline;
        private House()
        {
            stars = new SpriteRenderer[3];
        }
    }
    [SerializeField]
    public House[] houses;  // 스테이지 증가시 현 스크립트에 House 배열 크기 조절하기!! 별표!!!
    [SerializeField]
    User user_data;


    void Start()
    {
        Debug.Log("Scene Start Called");

        InitDatas();
        DefaultHouseSetter();
        SettingHouseStarsAndPline();

        //현재과일 토마토로 초기화.
        user_data.SetCurrentFruit_Tomato();
        //카메라 위치 로드
        LoadMainCameraPos();
    }

    private void InitDatas()
    {
        GameObject.Find("AutoLoader").GetComponent<AutoLoader>().AutoLoad_UserData();
        user_data = User.Instance;
    }

    //초기 집 세팅. 모든집 별 전부 끄고 폴리스라인은 스테이지0 제외하곤 모두 켜는 메서드.
    private void DefaultHouseSetter()
    {        
        for (int housesNum = 0; housesNum < houses.Length; housesNum++)
        {
            for (int starNum = 0; starNum < houses[housesNum].stars.Length; starNum++)
            {
                houses[housesNum].stars[starNum].enabled = false;
                if (housesNum == 0)
                {
                    houses[housesNum].pline.enabled = false;
                }
                else
                {
                    houses[housesNum].pline.enabled = true;
                }
            }
        }
    }

    //각 스테이지 하우스에 맞는 별점과 개방 상태를 설정해주는 부분.
    private void SettingHouseStarsAndPline()
    {
        print("SettingHouseStarsAndPline()");

        for (int i = 0; i < user_data.arrClearedStageStarScore.Count; i++)
        {
            int stageNum = user_data.arrClearedStageStarScore[i].Stage();

            //스테이지 클리어시 
            //최종 클리어한 스테이지가 1.마지막스테이지인지 2. 별이 0개는 아닌지 확인하고 3.최종클리어스테이지 다음 스테이지 폴리스라인 제거 들어감.
            if (stageNum + 1 < houses.Length && user_data.arrClearedStageStarScore[i].StarScore() != 0)
            {
                Debug.Log("스테이지" + stageNum +
                    " 에서 별을" + user_data.arrClearedStageStarScore[i].StarScore() + "개 획득한것이 확인되어 해당 스테이지의 폴리스라인을 비활성화 합니다.  (전체 스테이지 개수" + houses.Length + ")");
                houses[stageNum + 1].pline.enabled = false; //폴리스라인 제거
            }
            else // 최종스테이지이면 해제할 다음 스테이지의 폴리스라인이 없으므로  스킵. or 0점이어도 다음스테이지 폴리스라인 안풀어줌.
            { }


            //Debug.Log("stageNum:" + stageNum + "\nstar:" + userDataObj.GetComponent<User>().arrClearedStageStarScore[i].StarScore());

            //받은 별점 갯수에 맞게 별들 달아주는 메서드.
            switch (user_data.arrClearedStageStarScore[i].StarScore())
            {
                case 0:
                    break;
                case 1:
                    houses[stageNum].stars[0].enabled = true;
                    break;
                case 2:

                    for (int starNum = 0; starNum < 2; starNum++)
                    {
                        houses[stageNum].stars[starNum].enabled = true;
                    }
                    break;
                case 3:
                    for (int starNum = 0; starNum < 3; starNum++)
                    {
                        houses[stageNum].stars[starNum].enabled = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //카메라 위치를 로드해주는 부분. 이전에 씬을 떠나기 전에 마지막으로 위치했던 곳으로. 
    //만약에 처음 방문한거면 0,0,-10에 위치
    private void LoadMainCameraPos()
    {
        Vector3 camera_pos_loaded = user_data.LoadCameraPosition();
        if (camera_pos_loaded == Vector3.zero)
        {
            camera_pos_loaded = new Vector3(0f, 0f, -10f);
        }
        Camera.main.transform.position = camera_pos_loaded;
    }

}
