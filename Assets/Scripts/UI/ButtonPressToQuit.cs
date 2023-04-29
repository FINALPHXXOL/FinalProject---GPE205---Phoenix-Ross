using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToQuit : MonoBehaviour
{
    public AudioSource buttonclick;
    public AudioClip buttonclip;
    public void ChangeToQuit()
    {
        buttonclick.PlayOneShot(buttonclip);
        if (GameManager.instance != null)
        {
            GameManager.instance.QuitTheGame();
        }
    }
}
