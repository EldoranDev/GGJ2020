using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOption : MonoBehaviour
{
    [SerializeField]
    Slider masterVolumeSlider;

    public void SetMasterVolume() 
    {
        //AudioControl.Instance.SetGeneralVolume(masterVolumeSlider.value);
        Debug.Log(AudioListener.volume);
    }

}
