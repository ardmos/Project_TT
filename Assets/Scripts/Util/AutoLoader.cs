using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//매 씬마다 존재해야함!!!  

public class AutoLoader : MonoBehaviour
{
    // 각 씬이 시작될 때 아래 메소드를 통해 User 오브젝트의 데이터를 갱신해줘야함.
    public void AutoLoad_UserData()
    {
        //로드 이미 파일 존재하지 않는지 체크 후 진행. 존재파일 없으면 아무것도 안함. 
        if (GameObject.Find("User") != null)
            GameObject.Find("User").GetComponent<User>().LoadUser();
    }

}
