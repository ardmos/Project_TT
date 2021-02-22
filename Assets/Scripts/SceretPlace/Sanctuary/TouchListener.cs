using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchListener : MonoBehaviour
{
    Vector2 startPos, newPos, pos, deltapos;

    public string message { get; set; }

    Vector3 movePos, wstartPos, wnewPos;
    [SerializeField]
    GameObject dialogManager_Sanctuary, vfx;


    //마법진 완성되었는가
    bool iscomplete = false;
    //마법진
    //string magicCircle = "좌상좌하우하우상우하좌하";
    public bool[] magicspell = new bool[6] { false, false, false, false, false, false};
    public string recorded = null;
    public int spellInt = 0;
    public bool isFullCharged = false;
    string before = null;


    void Update()
    {
        if (Input.touchCount > 0 && isFullCharged == true)
        {
            Touch touch = Input.GetTouch(0);

            #region 터치방향 추출.
            deltapos = touch.deltaPosition;
            pos = touch.position;
            wstartPos = Camera.main.ScreenToWorldPoint(new Vector3(deltapos.x, deltapos.y));
            wnewPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y));

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = wnewPos - wstartPos;
                    message = "Began ";

                    //식 완성됐을 때 터치 들어오면 스킬 발동!
                    if(iscomplete==true)
                    {
                        print("Final Clicked!");
                        //발동 
                        dialogManager_Sanctuary.GetComponent<DialogManager_Sanctuary>().구름생성(true);
                        Destroy(gameObject);//리스너 파괴. 
                    }
                    break;
                case TouchPhase.Moved:
                    if(iscomplete == false)
                    {
                        newPos = wnewPos - wstartPos;
                        message = "Moved";
                        movePos = newPos - startPos;

                        GameObject effect = Instantiate(vfx);
                        effect.transform.position = movePos;
                        effect.GetComponent<ParticleSystem>().Play();


                        //좌상 -> 좌하 -> 우하 -> 우상 -> 우하 -> 좌하 .     마지막 터치 1번. 
                        if (spellInt >= 6)
                        {
                            //다시 처음부터!  
                            spellInt = 0;
                            recorded = null;
                            Debug.Log("처음부터!!" + spellInt);
                        }
                        else
                        {
                            //방향 추출. 
                            if (movePos.y > 0)
                            {
                                if (movePos.x > 0)
                                {
                                    //우상
                                    CheckMagicCircle("우상");
                                }
                                else
                                {
                                    //좌상
                                    CheckMagicCircle("좌상");
                                }
                            }
                            else
                            {
                                if (movePos.x > 0)
                                {
                                    //우하
                                    CheckMagicCircle("우하");
                                }
                                else
                                {
                                    //좌하
                                    CheckMagicCircle("좌하");
                                }
                            }
                            //
                            startPos = newPos;

                        }
                    }
                    break;
                default:
                    break;
            }
            #endregion
        }

        for(int n=0; n<magicspell.Length; n++)
        {
            if (magicspell[n] == true)
                spellInt++;
            else
                spellInt = 0;
        }

        if(spellInt==6)
        {
            //클리어!
            iscomplete = true;
            print("spellInt == 6");
        }
            
    }

    private void CheckMagicCircle(string spell)
    {
        if (before != null)
        {
            if (before != spell)
            {
                //방향이 바뀐거야! 그럼 이거 저장해!  
                before = spell;

                //magicspell[0~5]
                //0. 그냥 좌상 ok
                //1. 0 true, 좌하
                //2. 0,1 true, 우하
                //3. 반복. 0~본인번호-1까지. true이고, 각가 우상,   4.우하,   5.좌하. 
                switch (spell)
                {
                    case "좌상":
                        if (magicspell[0] == false)
                            magicspell[0] = true;
                        break;
                    case "좌하":
                        if (magicspell[0] == true && magicspell[1]==false)
                            magicspell[1] = true;
                        else if (magicspell[0] == true && magicspell[1] == true && magicspell[2] == true
                            && magicspell[3] == true && magicspell[4] == true && magicspell[5] == false)
                            magicspell[5] = true;
                        break;
                    case "우하":
                        if (magicspell[0] == true && magicspell[1] == true && magicspell[2] == false)
                            magicspell[2] = true;
                        else if (magicspell[0] == true && magicspell[1] == true && magicspell[2] == true 
                            && magicspell[3] == true && magicspell[4] == false)
                            magicspell[4] = true;
                        break;
                    case "우상":
                        if (magicspell[0] == true && magicspell[1] == true && magicspell[2] == true && magicspell[3] == false)
                            magicspell[3] = true;
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            //방향이 바뀐거야! 그럼 이거 저장해!  
            before = spell;
        }


    }

}
