using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{

    public AudioSource buttonAS;
    public AudioClip buttonSound;

    public void ButtonS()
    {
        buttonAS.clip = buttonSound;
        buttonAS.Play();
    }

}
