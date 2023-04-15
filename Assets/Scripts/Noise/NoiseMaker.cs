using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    public float volumeDistance;
    public float volumeDistanceDefault;
    private float timeUntilNextEvent;

    public void Start()
    {
        timeUntilNextEvent = 0f;
    }
    public void Update()
    {
        timeUntilNextEvent -= Time.deltaTime;
        if (timeUntilNextEvent < 0)
        {
            volumeDistance = 0;
        }
    }
    public void MakeNoise(float amount)
    {
        if (amount >= volumeDistance)
        {
            volumeDistance = amount;
            timeUntilNextEvent = 5;
        }
    }
}
