using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public bool hasBeenUsed;
    public void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.playerSpawns.Add(this);
        }
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.playerSpawns.Remove(this);
        }
    }
}
