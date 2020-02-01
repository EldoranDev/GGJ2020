using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : Singleton<AudioControl>
{

    public void SetGeneralVolume(float volume)
    {
        AudioListener.volume = volume;
    }

}
