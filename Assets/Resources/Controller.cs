using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(AudioSource))]
public class Controller : MonoBehaviourPunCallbacks
{
    [SerializeField] private AudioClip seikai;
    [SerializeField] private AudioClip huseikai;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Seikai()
    {
        if (hayaoshiPinPonedObject)
        {
            hayaoshiPinPonedObject.Seikai();
            hayaoshiPinPonedObject = null;
        }
        audioSource.PlayOneShot(seikai);
    }

    public void Huseikai()
    {
        if (hayaoshiPinPonedObject)
        {
            hayaoshiPinPonedObject.Huseikai();
            hayaoshiPinPonedObject = null;
        }
        audioSource.PlayOneShot(huseikai);
    }

    private HayaoshiAnswerButton hayaoshiPinPonedObject = null;
    public bool HayaoshiChallenge(HayaoshiAnswerButton button)
    {
        if (hayaoshiPinPonedObject != null)
        {
            return false;
        }
        else
        {
            hayaoshiPinPonedObject = button;
            return true;
        }
    }
}
