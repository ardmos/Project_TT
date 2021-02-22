using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_Right : MonoBehaviour
{
    public GameObject toyPrefab;
    float span = 0.15f;
    float delta = 0;
    public bool isOn = false;
    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            this.delta += Time.deltaTime;
            if (this.delta > this.span)
            {
                this.delta = 0;
                GameObject toy = Instantiate(toyPrefab) as GameObject;
                toy.transform.position = gameObject.transform.position;
                int rm = Random.Range(0, 24);//ToyBox크기 반영
                toy.GetComponent<SpriteRenderer>().sprite = GameObject.Find("=====TOY BOX=====").GetComponent<ToyBox>().toySprites[rm];
                toy.GetComponent<Toy>().loc = "Right";
            }
        }
    }
}
