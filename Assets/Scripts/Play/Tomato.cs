using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    [SerializeField]
    Sprite goaledTomato, boomedTomato;

    //토마토, 골 충돌체크, 발사여부. 
    [SerializeField]
    GameObject[] items;
    public bool isGoaled, isOnTheAir, isKatchuped;


    void Start()
    {
        if (GameObject.Find("Rino"))
        {
            isGoaled = false;
            isOnTheAir = false;
            isKatchuped = false;
        }
        
    }

    void Update()
    {
        if (isKatchuped == false)
        {
            items = GameObject.FindGameObjectsWithTag("coin");

            for (int n = 0; n < items.Length; n++)
            {
                #region 토마토와 코인의 출돌판정
                Vector2 p1 = transform.position;
                Vector2 p2 = items[n].transform.position;
                Vector2 dir = p1 - p2;
                float dis = dir.magnitude;
                float r1 = 0.1f;
                float r2 = 0.25f;

                if (dis < r1 + r2)
                {
                    //코인 먹기! 처리.
                    items[n].GetComponent<Coin>().GetCoin();
                }
                #endregion
            }
        }
    }

    #region 보스와 충돌판정
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionEnter2D has been called : " + collision.transform.tag);
        //콜라이더도 꺼야함. 
        //gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        if (collision.transform.tag == "Boss" && isKatchuped == false)
        {
            StartCoroutine(Delay03Sec());
        }
    }

    IEnumerator Delay03Sec()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<SpriteRenderer>().sprite = goaledTomato;
        //firework.Play();
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.25f);
        //Success! 성공 토마토 터지는 모션 필요. 일단은 콜라이더 없앰.
        isGoaled = true;
        //print(" GameObject.Find(\"StageDirector\").GetComponent<StageDirector>().AddSucceededTom()");
        GameObject.Find("StageDirector").GetComponent<StageDirector>().AddSucceededTom();   //디렉터에게 성공 보고
    }
    #endregion


    #region 발사된걸 과일이 인식하는 부분
    public void CallThisWhenImShooted()
    {
        Debug.Log("Shooted!");
        isOnTheAir = true;
        if (isOnTheAir)
        {
            StartCoroutine(TomatoBoom());
        }
    }
    #endregion

    #region 토마토 터지는 부분
    IEnumerator TomatoBoom()
    {
        yield return new WaitForSeconds(5);
        if (!isGoaled)
        {
            gameObject.GetComponent<AudioSource>().Play();
            isKatchuped = true;
            print("boom!! : " + gameObject.name);
            gameObject.GetComponent<SpriteRenderer>().sprite = boomedTomato;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

    }
    #endregion

}
