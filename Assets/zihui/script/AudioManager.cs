using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource BGM;
    [SerializeField]
    AudioSource deadSoundEffect;
    [SerializeField]
    AudioSource eatAlphabetEffect;
    [SerializeField]
    AudioSource eatEquipmentEffect;


    public void PlayBGM()
    {
        if (BGM != null)
        {
            BGM.loop = true;
            BGM.Play();
        }
    }


    public void StopBGM()
    {
        BGM.Stop();
    }
    public void PlayDead()
    {
        if (deadSoundEffect != null)
        {
            deadSoundEffect.Play();
        }
    }

    public void PlayAlphabet()
    {
        if (eatAlphabetEffect != null)
        {
            eatAlphabetEffect.Play();
        }
    }

    public void PlayEquipment()
    {
        if (eatEquipmentEffect != null)
        {
            eatEquipmentEffect.Play();
        }
    }


}

