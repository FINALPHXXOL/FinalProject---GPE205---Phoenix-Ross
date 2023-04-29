using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToOptions : MonoBehaviour
{
    public AudioSource buttonclick;
    public AudioClip buttonclip;
    public void ChangeToOptions()
    {
        buttonclick.PlayOneShot(buttonclip);
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateOptionsScreen();
        }
    }
}
