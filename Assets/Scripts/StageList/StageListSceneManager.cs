using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageListSceneManager : MonoBehaviour
{
    [Serializable]
    public class House
    {
        public SpriteRenderer[] stars;
        //Spreite stageName;
        public SpriteRenderer pline;
        private House()
        {
            stars = new SpriteRenderer[3];
        }
    }
    [SerializeField]
    public House[] houses;

    /// <summary>
    /// 스테이지 증가시 현 스크립트에 House 배열 크기 조절해야함.
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Scene Start Called");
        GameObject.Find("AutoLoader").GetComponent<AutoLoader>().AutoLoad_UserData();
        DefaultHouseSetter();
        SettingHouseStarsAndPline();

        //현재과일 토마토로 초기화.
        User user_data = GameObject.FindObjectOfType<User>();
        user_data.SetCurrentFruit_Tomato();
        //카메라 위치 로드.
        Vector3 camera_pos_loaded = user_data.LoadCameraPosition();
        if (camera_pos_loaded == Vector3.zero)
        {
            camera_pos_loaded = new Vector3(0f, 0f, -10f);
        }
        Camera.main.transform.position = camera_pos_loaded;


    }

    private void SettingHouseStarsAndPline()
    {
        print("SettingHouseStarsAndPline()");
        if (GameObject.Find("User"))
        {
            GameObject userDataObj = GameObject.Find("User");

            for(int i = 0; i < userDataObj.GetComponent<User>().arrClearedStageStarScore.Count; i++)
            {
                int stageNum = userDataObj.GetComponent<User>().arrClearedStageStarScore[i].Stage();

                //스테이지 클리어시 
                //최종 클리어한 스테이지가 1.마지막스테이지인지 2. 별이 0개는 아닌지 확인하고 3.최종클리어스테이지 다음 스테이지 폴리스라인 제거 들어감.
                if (stageNum + 1 < houses.Length && userDataObj.GetComponent<User>().arrClearedStageStarScore[i].StarScore() != 0)
                {
                    Debug.Log("스테이지" + stageNum + 
                        " 에서 별을" + userDataObj.GetComponent<User>().arrClearedStageStarScore[i].StarScore() + "개 획득한것이 확인되어 해당 스테이지의 폴리스라인을 비활성화 합니다.  (전체 스테이지 개수" + houses.Length + ")");
                    houses[stageNum + 1].pline.enabled = false; //폴리스라인 제거
                }
                else // 최종스테이지이면 해제할 다음 스테이지의 폴리스라인이 없으므로  스킵. or 0점이어도 다음스테이지 폴리스라인 안풀어줌.
                { }


                //Debug.Log("stageNum:" + stageNum + "\nstar:" + userDataObj.GetComponent<User>().arrClearedStageStarScore[i].StarScore());

                //받은 별점 갯수에 맞게 별들 달아준다.
                switch (userDataObj.GetComponent<User>().arrClearedStageStarScore[i].StarScore())
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
    }

    private void DefaultHouseSetter()
    {
        //초기 집 세팅. 모든집 별 전부 끄고 폴리스라인은 스테이지0 제외하곤 모두 켠다. 
        for (int housesNum=0; housesNum<houses.Length; housesNum++)
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
}
