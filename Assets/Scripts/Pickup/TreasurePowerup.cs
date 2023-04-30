using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class TreasurePowerup : Powerup
{
    public float scoreToAdd;
    public override void Apply(PowerupManager target)
    {
        Pawn addScore = target.GetComponent<Pawn>();
        if (addScore != null)
        {
            if (addScore.controller != null)
            {
                addScore.controller.Invoke("UpdateTreasure", 0.1f);
                addScore.controller.AddToScore(scoreToAdd);
            }
        }
    }

    public override void Remove(PowerupManager target)
    {
        // TODO: Remove Health changes
    }
}
