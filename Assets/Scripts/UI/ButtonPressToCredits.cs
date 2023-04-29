using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToCredits : MonoBehaviour
{
    public AudioSource buttonclick;
    public AudioClip buttonclip;
    public void ChangeToCredits()
    {
        buttonclick.PlayOneShot(buttonclip);
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateCredits();
        }
    }
}
