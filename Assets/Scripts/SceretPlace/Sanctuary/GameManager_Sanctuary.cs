using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Sanctuary : MonoBehaviour
{
    bool swingDir;
    [SerializeField]
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnSwingClicked()
    {       
        swingDir = !swingDir;
        animator.SetBool("swingDir", swingDir);
        //print("swing dir: " + swingDir);
        if(GameObject.Find("요술봉"))
            GameObject.Find("요술봉").GetComponent<MagicWand>().swinged++;
    }

    
}
