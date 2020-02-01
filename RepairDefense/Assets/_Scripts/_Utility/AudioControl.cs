using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{

    public void SetGeneralVolume(float volume)
    {
        AudioListener.volume = volume;
    }

}
