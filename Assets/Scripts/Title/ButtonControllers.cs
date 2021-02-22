using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControllers : MonoBehaviour
{
    [SerializeField]
    GameObject optionPopupPrefab, canvas, crossFadeImgObj;

    public void ActiveStartButton()
    {
        //저장. 
        if(GameObject.Find("User")!=null)
            GameObject.Find("User").GetComponent<User>().SaveUser();
        //스타트 버튼
        //SceneManager.LoadScene("StageList");
        //StartCoroutine(CrossFade());
        crossFadeImgObj.GetComponent<CrossFadeImage_Start>().StartCrossFadingImage();
    }

    public void ActiveOptionButton()
    {
        //옵션 팝업(프리팹)
        GameObject optionPopupObj = Instantiate(optionPopupPrefab) as GameObject;
        optionPopupObj.transform.SetParent(canvas.transform);
        optionPopupObj.GetComponent<RectTransform>().localPosition = Vector3.zero;        
    }
    public void ActiveExitButton()
    {
        //저장. 
        if (GameObject.Find("User") != null)
            GameObject.Find("User").GetComponent<User>().SaveUser();
        //앱 종료
        Application.Quit();
    }



}
