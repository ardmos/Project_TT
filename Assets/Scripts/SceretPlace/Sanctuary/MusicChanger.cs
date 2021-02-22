using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField]
    AudioClip audio;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("BGMusic"))
        {
            GameObject.Find("BGMusic").GetComponent<AudioSource>().clip = audio;
            GameObject.Find("BGMusic").GetComponent<AudioSource>().Play();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
