using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopupController : MonoBehaviour
{
    [SerializeField]
    Toggle sfxOn, sfxOff, musicOn, musicOff;

    GameObject userObj;
    private void Awake()
    {
        userObj = GameObject.Find("User");
        Inits();
    }

    private void Inits()
    {
        sfxOn.isOn = userObj.GetComponent<User>().GetsfxOn();
        sfxOff.isOn = userObj.GetComponent<User>().GetsfxOff();
        musicOn.isOn = userObj.GetComponent<User>().GetmusicOn();
        musicOff.isOn = userObj.GetComponent<User>().GetmusicOff();
    }

    public void CloseBtnClicked()
    {
        Destroy(gameObject);
    }

    #region SFX Toggle Controll
    public void SFXtoggleControll()
    {
        if (sfxOn.isOn)
        {
            print("sfxOn");
            userObj.GetComponent<User>().SetsfxOn(true);
            userObj.GetComponent<User>().SetsfxOff(false);
        }
        else if (sfxOff.isOn)
        {
            print("sfxOff");
            userObj.GetComponent<User>().SetsfxOn(false);
            userObj.GetComponent<User>().SetsfxOff(true);
        }
    }
    #endregion

    #region Music Toggle Controll
    public void MusictoggleControll()
    {        
        if (GameObject.Find("BGMusic"))
        {
            GameObject bgmusicObj = GameObject.Find("BGMusic");

            if (musicOn.isOn)
            {
                print("musicOn");
                bgmusicObj.GetComponent<AudioSource>().enabled = true;
                userObj.GetComponent<User>().SetmusicOn(true);
                userObj.GetComponent<User>().SetmusicOff(false);
            }
            else if (musicOff.isOn)
            {
                print("musicOff");
                bgmusicObj.GetComponent<AudioSource>().enabled = false;
                userObj.GetComponent<User>().SetmusicOn(false);
                userObj.GetComponent<User>().SetmusicOff(true);
            }
        }   
    }
    #endregion

}
