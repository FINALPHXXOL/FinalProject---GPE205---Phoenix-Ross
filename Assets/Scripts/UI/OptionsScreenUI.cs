using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static AIController;
using static UnityEngine.GraphicsBuffer;

public class OptionsScreenUI : MonoBehaviour
{
    public enum SelectState { Treasure, Officer, Captain };
    public SelectState currentState = SelectState.Treasure;

    public AudioMixer mainAudioMixer;
    public Slider mainVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;

    public GameObject TreasureSelectStateObject;
    public GameObject OfficerSelectStateObject;
    public GameObject CaptainSelectStateObject;

    public TextMeshProUGUI UIChangeSelect;


    public void Start()
    {
        OnMainVolumeChange();
        ChangeSelectState();
    }

    private void DeactivateAllStates()
    {
        // Deactivate all Game States
        TreasureSelectStateObject.SetActive(false);
        OfficerSelectStateObject.SetActive(false);
        CaptainSelectStateObject.SetActive(false);
    }

    public void ChangeSelectState()
    {
        switch (currentState)
        {
            case SelectState.Treasure:
                DeactivateAllStates();
                TreasureSelectStateObject.SetActive(true);
                UIChangeSelect.text = "Treasures: ";
                break;
            case SelectState.Officer:
                DeactivateAllStates();
                OfficerSelectStateObject.SetActive(true);
                UIChangeSelect.text = "Officers: ";
                break;
            case SelectState.Captain:
                DeactivateAllStates();
                CaptainSelectStateObject.SetActive(true);
                UIChangeSelect.text = "Captains: ";
                break;
        }
    }

    public void OnClickDropDownState()
    {
        if (currentState == SelectState.Treasure)
        {
            currentState = SelectState.Officer;
            ChangeSelectState();
        } else if (currentState == SelectState.Officer)
        {
            currentState = SelectState.Captain;
            ChangeSelectState();
        } else if (currentState == SelectState.Captain)
        {
            currentState = SelectState.Treasure;
            ChangeSelectState();
        }
    }

    public void OnMainVolumeChange()
    {
        // Start with the slider value (assuming our slider runs from 0 to 1)
        float newVolume = mainVolumeSlider.value;
        if (newVolume <= 0)
        {
            // If we are at zero, set our volume to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are >0, so start by finding the log10 value 
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20db range (instead of 0-1 db)
            newVolume = newVolume * 20;
        }

        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("MainVolume", newVolume);
    }
    public void OnSFXVolumeChange()
    {
        // Start with the slider value (assuming our slider runs from 0 to 1)
        float newVolume = sfxVolumeSlider.value;
        if (newVolume <= 0)
        {
            // If we are at zero, set our volume to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are >0, so start by finding the log10 value 
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20db range (instead of 0-1 db)
            newVolume = newVolume * 20;
        }

        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("SFXVolume", newVolume);
    }

    public void OnMusicVolumeChange()
    {
        // Start with the slider value (assuming our slider runs from 0 to 1)
        float newVolume = musicVolumeSlider.value;
        if (newVolume <= 0)
        {
            // If we are at zero, set our volume to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are >0, so start by finding the log10 value 
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20db range (instead of 0-1 db)
            newVolume = newVolume * 20;
        }

        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("MusicVolume", newVolume);
    }

}