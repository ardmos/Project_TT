using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField]
    Sprite goaledTomato, boomedTomato;

    //토마토, 골 충돌체크. 
    GameObject goal;
    public bool isGoaled;


    void Start()
    {
        if (GameObject.Find("Goal"))
        {
            this.goal = GameObject.Find("Goal");
            isGoaled = false;
            StartCoroutine(TomatoBoom());
        }
    }

    void Update()
    {
        if (GameObject.Find("Goal"))
        {
            #region 토마토와 골의 출돌판정
            Vector2 p1 = transform.position;
            Vector2 p2 = this.goal.transform.position;
            Vector2 dir = p1 - p2;
            float dis = dir.magnitude;
            float r1 = 0.1f;
            float r2 = 0.25f;

            if (dis < r1 + r2)
            {
                //Success! 성공 토마토 터지는 모션 필요. 일단은 콜라이더 없앰.
                isGoaled = true;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                gameObject.GetComponent<SpriteRenderer>().sprite = goaledTomato;

                GameObject.Find("StageDirector").GetComponent<StageDirector>().AddSucceededTom();   //디렉터에게 성공 보고
            }
            #endregion
        }
    }


    #region 토마토 터지는 부분
    IEnumerator TomatoBoom()
    {
        yield return new WaitForSeconds(5);
        if (!isGoaled)
        {
            print("boom!! : " + gameObject.name);
            gameObject.GetComponent<SpriteRenderer>().sprite = boomedTomato;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

    }
    #endregion

}
