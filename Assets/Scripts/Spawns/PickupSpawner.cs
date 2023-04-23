using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public bool hasBeenUsed;
    public void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.pickupSpawns.Add(this);
        }
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.pickupSpawns.Remove(this);
        }
    }
}
