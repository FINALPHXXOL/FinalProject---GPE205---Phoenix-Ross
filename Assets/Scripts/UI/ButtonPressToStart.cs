using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToStart : MonoBehaviour
{
    public AudioSource buttonclick;
    public AudioClip buttonclip;
    public void ChangeToMainMenu()
    {
        buttonclick.PlayOneShot(buttonclip);
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateMainMenuScreen();
        }
    }
}