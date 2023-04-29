using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseMaker : MonoBehaviour
{
    public float volumeDistance;
    public float volumeDistanceDefault;
    private float timeUntilNextEvent;

    public Image noiseCircle;


    public void Start()
    {
        timeUntilNextEvent = 0f;
        RectTransform rectTransform = noiseCircle.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2((volumeDistance), (volumeDistance));
    }
    public void Update()
    {
        timeUntilNextEvent -= Time.deltaTime;
        if (timeUntilNextEvent < 0)
        {
            volumeDistance = 0;
            RectTransform rectTransform = noiseCircle.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2((volumeDistance), (volumeDistance));
        }
    }
    public void StopNoise()
    {
        volumeDistance = 0;
        RectTransform rectTransform = noiseCircle.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2((volumeDistance), (volumeDistance));
    }
    public void MakeNoise(float amount)
    {
        if (amount >= volumeDistance)
        {
            Debug.Log("Is it working");
            volumeDistance = amount;
            if (noiseCircle != null)
            {
                RectTransform rectTransform = noiseCircle.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2((volumeDistance * 4), (volumeDistance * 4));
            }
            timeUntilNextEvent = 1f;
        }
    }
}
