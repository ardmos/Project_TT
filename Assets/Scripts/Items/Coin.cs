using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int coin = 0;
    [SerializeField]
    GameObject starpop;

    public void GetCoin()
    {
        //스테이지디렉터에 보고하고 파괴. 결과창에 반영되게끔.
        GameObject.Find("StageDirector").GetComponent<StageDirector>().SetHaveCoin(coin);
        //FX 발동. 
        GameObject pref = Instantiate(starpop) as GameObject;
        pref.transform.position = this.gameObject.transform.position;
        //파괴
        Destroy(this.gameObject);
    }

}
