using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;
    public TextMeshProUGUI UIScore;
    public TextMeshProUGUI UILives;
    public TextMeshProUGUI UITreasure;

    public float lives;
    public float score;
    public int treasureLeft;
    // Start is called before the first frame update
    public virtual void Start()
    {
        if (UIScore != null)
        {
            UIScore.text = "Score: " + score;
        }
        if (UILives != null)
        {
            UILives.text = "Lives: " + lives;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void UpdateTreasure();
    public abstract void RespawnPlayer();
    public abstract void AddToScore(float amount);
    public abstract void RemoveScore(float amount);
    public abstract void AddLives(float amount);
    public abstract void RemoveLives(float amount);
    
}
