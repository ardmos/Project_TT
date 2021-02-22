using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossFadeImage_Start : MonoBehaviour
{
    [SerializeField]
    Animator transition;
    public float transitionTime = 1f;

    public void StartCrossFadingImage()
    {
        StartCoroutine(CrossFade());
    }

    IEnumerator CrossFade()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("StageList");
    }


    public void StartCrossFadingImageToPlace(string sceneName)
    {
        StartCoroutine(CrossFade(sceneName));
    }

    IEnumerator CrossFade(string SceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneName);
    }
}
