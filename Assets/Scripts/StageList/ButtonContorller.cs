using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonContorller : MonoBehaviour
{
    [SerializeField]
    CrossFadeImage_Start crossFade_start;
    public void ActiveMoveBackButton()
    {
        //씬 넘어가기 전에 카메라 pos 저장
        GameObject.FindObjectOfType<User>().SaveCameraPosition(Camera.main.transform.position);

        crossFade_start.StartCrossFadingImageToPlace("Title");
    }
}
