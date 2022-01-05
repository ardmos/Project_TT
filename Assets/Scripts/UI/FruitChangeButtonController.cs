using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitChangeButtonController : MonoBehaviour
{
    User user_data;

    // Start is called before the first frame update
    void Start()
    {
        if (user_data == null)
        {
            try
            {
                user_data = GameObject.FindObjectOfType<User>();
            }
            catch (System.Exception)
            {

                throw new System.Exception("오브젝트명을 확인해줍쇼...! xD");
            }
        }

        gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnFruitChangeButtonClicked(); } );
        gameObject.GetComponent<Image>().sprite = user_data.GetCurrentFruitImages();
    }

    private void OnFruitChangeButtonClicked()
    {
        // 1. Enum + 1한 과일 이미지로 해당 현재 선택 과일 이미지로 버튼 이미지를 변경
        gameObject.GetComponent<Image>().sprite = user_data.GetCurrentFruitImages();
        // 2. Enum + 1한 과일 Prefab으로 해당 현재 선택 과일 Prefab 변경
        user_data.SetCurrentFruitToNextFruit();

    }
}
