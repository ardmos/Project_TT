using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    GameObject sampleApple;
    public Transform guideLine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootCannon()
    {
        //sampleApple.GetComponent<Rigidbody2D>().AddForce(new Vector2(200,0), ForceMode2D.Impulse);
        sampleApple.GetComponent<Rigidbody2D>().velocity = guideLine.right*10f;
    }

}
