using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToGameplay : MonoBehaviour
{
    public AudioSource buttonclick;
    public AudioClip buttonclip;
    public void ChangeToGameplay()
    {
        buttonclick.PlayOneShot(buttonclip);
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateGameplay();
        }
    }
}
