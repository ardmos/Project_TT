using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitAndLoadUser_Title : MonoBehaviour
{
    [SerializeField]
    User user;
    [SerializeField]
    BGMusic bgmusic;
    // 타이틀 기초. 저장정보 읽어와서 배경음악, 효과음 제어 세팅 초기화.
    void Start()
    {
        gameObject.GetComponent<AutoLoader>().AutoLoad_UserData();

        user = GameObject.Find("User").GetComponent<User>();
        bgmusic = GameObject.Find("BGMusic").GetComponent<BGMusic>();

        //로드 이후이기 때문에 해당 정보 바탕으로 초기화 시작해도 문제없음.
        
        if (user.GetsfxOn())
        {
            //sfxOn    
        }
        else
        {
            //sfxOff
        }
        if (user.GetmusicOn())
        {
            //musicOn
            bgmusic.BGMusic_ON();
        }
        else
        {
            //musicOff
            bgmusic.BGMusic_OFF();
        }
    }

}
