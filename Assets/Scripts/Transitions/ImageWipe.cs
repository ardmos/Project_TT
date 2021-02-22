using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageWipe : MonoBehaviour
{
    //ImageWipe !! 부분. 
    [SerializeField]
    Animator transition;
    public float transitionTime = 1f;
    [SerializeField]
    Image transitioningImage;
    [SerializeField]
    Sprite[] sourceImages;

    public void StartImageWipe(string sceneName, int ImgNum)
    {
        StartCoroutine(StartImageWipeCor(sceneName, ImgNum));
    }

    //Transition ImageWipe 부분
    IEnumerator StartImageWipeCor(string sceneName, int ImgNum)
    {
        transitioningImage.sprite = sourceImages[ImgNum];
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
