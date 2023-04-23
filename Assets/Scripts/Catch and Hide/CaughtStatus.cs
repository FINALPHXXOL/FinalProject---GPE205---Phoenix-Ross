using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtStatus : MonoBehaviour
{

    public Pawn owner;
    public GameObject audioManager;

    //public AudioSource hitExplosion;
    //public AudioClip hitExplode;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Caught(Pawn source)
    {
        Pawn pawn = gameObject.GetComponent<Pawn>();

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayCaughtSound();
        }
        Controller loseLife = pawn.controller;
        Debug.Log(source.name + " destroyed " + gameObject.name);
        loseLife.RemoveLives(1);
        Destroy(gameObject);
    }
}
