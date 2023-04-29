using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainSpawnPoint : MonoBehaviour
{
    public Transform[] waypoints;
    public bool hasBeenUsed;
    public void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.captainSpawns.Add(this);
        }
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.captainSpawns.Remove(this);
        }
    }
}
