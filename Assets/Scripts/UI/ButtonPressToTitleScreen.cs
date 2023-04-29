using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToTitleScreen : MonoBehaviour
{
    public AudioSource buttonclick;
    public AudioClip buttonclip;
    public void ChangeToTitleScreen()
    {
        buttonclick.PlayOneShot(buttonclip);
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateTitleScreen();
        }
    }
}
