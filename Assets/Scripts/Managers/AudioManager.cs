using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource caughtAudio;
    public AudioClip caughtClip;

    public AudioSource pickupAudio;
    public AudioClip pickupClip;

    public AudioSource spottedAudio;
    public AudioClip spottedClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
    public void PlayCaughtSound()
    {
        if (caughtAudio != null)
        {
            caughtAudio.PlayOneShot(caughtClip);
        }
    }

    public void PlayPickupSound()
    {
        if (pickupAudio != null)
        {
            pickupAudio.PlayOneShot(pickupClip);
        }
    }

    public void PlaySpottedSound()
    {
        if (spottedAudio != null)
        {
            spottedAudio.PlayOneShot(spottedClip);
        }
    }
}
