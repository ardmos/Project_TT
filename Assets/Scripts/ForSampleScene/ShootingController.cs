using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    [SerializeField]
    GameObject tomatoPos, tomatoPrefab;
    GameObject tomato_RO;


    private bool isReloaded;


    private void Start()
    {
        ReloadingTomato();
        //isReloaded = true;
    }


    #region   화면 한 번 터치해서 발사하는 방식.  (again버튼 대신. 토글식으로 구현. )
    public void OnShootingButotnClicked()
    {
        if (isReloaded)
        {
            //발사!
            tomato_RO.GetComponent<RelativeJoint2D>().enabled = false;
            isReloaded = false;
        }
        else
        {
            //재장전!
            ReloadingTomato();

            //tomato.GetComponent<RelativeJoint2D>().enabled = true;
            //tomato.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            //tomato.transform.position = tomatoPos.transform.position;
            //isReloaded = true;
        }


    }
    #endregion


    //재장전. 
    private void ReloadingTomato()
    {
        GameObject tomato = Instantiate(tomatoPrefab) as GameObject;
        tomato.transform.position = tomatoPos.transform.position;
        //tomato.transform.SetParent(GameObject.Find("Catapult").transform);
        tomato.GetComponent<RelativeJoint2D>().connectedBody = GameObject.Find("rope").GetComponent<Rigidbody2D>();
        tomato_RO = tomato;
        isReloaded = true;
    }





    #region 화면 꾹 눌렀다 떼는 방식. 
    /*
    private void OnMouseDown()
    {
        print("Down");
    }

    private void OnMouseUp()
    {
        print("Up");
    }
    */
    
    #endregion
}

