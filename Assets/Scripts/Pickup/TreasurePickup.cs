using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TreasurePickup : Pickup
{
    public TreasurePowerup powerup;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.treasure.Add(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        // variable to store other object's PowerupController - if it has one
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        // If the other object has a PowerupController
        if (powerupManager != null)
        {
            powerupManager.Add(powerup);

            // Destroy this pickup
            Destroy(gameObject);

        }
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.treasure.Remove(this);
            if (GameManager.instance.treasure.Count <= 0)
            {
                Debug.Log("YOU WON!!!!");
                GameManager.instance.ActivateGameOver();
            }
        }
    }
}
