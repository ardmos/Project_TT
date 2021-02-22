using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnBackToStageList : MonoBehaviour
{
    public void BackToStageList()
    {
        //저장. 
        if (GameObject.Find("User") != null)
            GameObject.Find("User").GetComponent<User>().SaveUser();
        //SceneManager.LoadScene("StageList");
        if (GameObject.Find("CrossFadeImage_Start") != null)
        {
            GameObject.Find("CrossFadeImage_Start").GetComponent<CrossFadeImage_Start>().StartCrossFadingImage();
        }
        else
        {
            SceneManager.LoadScene("StageList");
        }
    }
}
