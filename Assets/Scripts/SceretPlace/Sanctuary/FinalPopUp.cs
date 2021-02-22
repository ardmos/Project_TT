using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;
using System.IO;

public class FinalPopUp : MonoBehaviour
{
    [SerializeField]
    //List<Sprite> sprites;
    Sprite[] sprites;

    private void Start()
    {
        //GetFile();
    }

    public void StartFinalPopUp()
    {

        StartCoroutine(WoSS());
    }

    IEnumerator WoSS()
    {
        //for (int n = 0; n < sprites.Count; n++)
        for (int n = sprites.Length-1; n >= 0 ; n--)
        {          
            gameObject.GetComponent<Image>().sprite = sprites[n];
            yield return new WaitForSeconds(2f);
        }
    }

    
    
    /*
    private void GetFile()
    {
        string[] fName;

        fName = Directory.GetFiles("Assets/Resources/imgs/", "*.jpg");

        for (int i = 0; i < fName.Length; i++)
        {
            print(fName[i]);
            
            string filename = Path.GetFileNameWithoutExtension(fName[i]);
            //print("Assets/Resources/imgs/"+filename);
            //print(Resources.Load<Sprite>("/imgs/" + filename));
            //sprites.Add(UnityEditor.AssetDatabase.LoadAssetAtPath(fName[i], typeof(Sprite)) as Sprite);
            sprites.Add(Resources.Load<Sprite>("imgs/"+filename));
        }
    }
    */
}
