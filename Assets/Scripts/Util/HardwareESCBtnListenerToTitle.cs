using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HardwareESCBtnListenerToTitle : MonoBehaviour
{
    [SerializeField]
    CrossFadeImage_Start crossFade_start;
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Menu))
            {
                OnApplicationPause(true);
            }
            else if (Input.GetKey(KeyCode.Home))
            {
                OnApplicationPause(true);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                //저장. 
                if (GameObject.Find("User") != null)
                    GameObject.Find("User").GetComponent<User>().SaveUser();

                crossFade_start.StartCrossFadingImageToPlace("Title");               
            }
        }
    }

    private void OnApplicationPause(bool pause)
    {
        
    }
}
