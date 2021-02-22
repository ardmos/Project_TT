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
        crossFade_start.StartCrossFadingImageToPlace("Title");
    }
}
