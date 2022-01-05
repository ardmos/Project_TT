using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirToggleController : MonoBehaviour
{
    TouchController touch_controller;

    // Start is called before the first frame update
    void Start()
    {
        if (touch_controller == null)
        {
            try
            {
                touch_controller = GameObject.FindObjectOfType<TouchController>();
            }
            catch (System.Exception)
            {

                throw new System.Exception("오브젝트명을 확인해주세요");
            }
        }

        gameObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate{
            ToggleTurnRorL();
        });
        
    }

    #region 몽딩이 회전 방향 토글버튼 기능.
    public void ToggleTurnRorL()
    {
        touch_controller.right = !touch_controller.right;        
    }
    #endregion
}
