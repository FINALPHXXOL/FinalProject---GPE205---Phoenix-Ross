using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public List<Powerup> powerups;
    public List<Powerup> removedPowerupQueue;

    public GameObject audioManager;
    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Powerup>();
        removedPowerupQueue = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        DecrementPowerupTimers();
    }

    private void LateUpdate()
    {
        ApplyRemovePowerupsQueue();
    }
    // The Add function will eventually add a powerup
    public void Add(Powerup powerupToAdd)
    {
        if (powerupToAdd != null)
        {
            powerupToAdd.Apply(this);
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayPickupSound();
            }
            powerups.Add(powerupToAdd);
        }
    }
    // The Add function will eventually add a powerup
    public void Remove(Powerup powerupToRemove)
    {
        if (powerupToRemove != null)
        {
            removedPowerupQueue.Add(powerupToRemove);
            powerupToRemove.Remove(this);
        }
    }

    private void ApplyRemovePowerupsQueue()
    {
        foreach (Powerup powerup in removedPowerupQueue)
        {
            powerups.Remove(powerup);
        }
    }
    public void DecrementPowerupTimers()
    {
        foreach (Powerup powerupToRemove in powerups)
        {
            powerupToRemove.duration -= Time.deltaTime;

            if (powerupToRemove.duration <= 0)
            {
                Remove(powerupToRemove);
            }

        }
    }
}