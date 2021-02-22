using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    Vector2 startPos, newPos, pos, deltapos;

    public string message { get; set; }

    Vector3 movePos, wstartPos, wnewPos, bemm, newmm;
    [SerializeField]
    GameObject vfx;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            #region 터치방향 추출.
            deltapos = touch.deltaPosition;
            pos = touch.position;
            wstartPos = Camera.main.ScreenToWorldPoint(new Vector3(deltapos.x, deltapos.y));
            wnewPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y));
            bemm = Camera.main.ScreenToViewportPoint(new Vector3(deltapos.x, deltapos.y));
            newmm = Camera.main.ScreenToViewportPoint(new Vector3(pos.x, pos.y));

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //startPos = wnewPos - wstartPos;
                    startPos = newmm - bemm;
                    message = "Began ";
                    //print(message);
                    break;
                case TouchPhase.Moved:
                    //newPos = wnewPos - wstartPos;
                    newPos = newmm - bemm;
                    message = "Moved";
                    //print(message);
                    movePos = newPos - startPos;

                    GameObject effect = Instantiate(vfx);
                    effect.transform.position = movePos;
                    effect.GetComponent<ParticleSystem>().Play();
                    break;
                default:
                    break;
            }
            #endregion
        }
    }
}

