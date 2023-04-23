using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerSpawnPoint : MonoBehaviour
{
    public Transform[] waypoints;
    public bool hasBeenUsed;
    public void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.officerSpawns.Add(this);
        }
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.officerSpawns.Remove(this);
        }
    }
}
