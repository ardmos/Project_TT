using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAppleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Shoot(new Vector2(200, 200)); 
    }
    
    public void Shoot(Vector2 dir)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(dir);
        
    }

    #region 과녁에 맞았을 때
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionEnter2D has been called : " + collision.transform.tag);
        //콜라이더도 꺼야함. 
        //gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        if(collision.transform.tag == "Boss")
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<ParticleSystem>().Play();
        }
        
    }
    #endregion
}
