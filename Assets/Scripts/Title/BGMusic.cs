using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : DontDestroy<BGMusic>
{
    public void BGMusic_ON()
    {
        GetComponent<AudioSource>().enabled = true;
    }
    public void BGMusic_OFF()
    {
        GetComponent<AudioSource>().enabled = false;
    }
}
