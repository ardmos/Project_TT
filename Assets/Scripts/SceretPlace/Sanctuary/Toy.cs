using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    public string loc = null;
    bool isreported = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isreported == false)
        {
            switch (loc)
            {
                case "Left":
                    if (gameObject.transform.position.x < 0)
                        성공();
                    else
                        실패();
                    break;
                case "Right":
                    if (gameObject.transform.position.x > 0)
                        성공();
                    else
                        실패();
                    break;
                default:
                    break;
            }
        }

    }

    private void 성공()
    {
        GameObject.Find("=====DIALOGMANAGER=====").GetComponent<DialogManager_Sanctuary>().suc++;
        isreported = true;
    }
    private void 실패()
    {
        GameObject.Find("=====DIALOGMANAGER=====").GetComponent<DialogManager_Sanctuary>().fai++;
        isreported = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bottom"))
            Destroy(gameObject);
    }
}
