using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class PlayerController : Controller
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode sneakKey;

    // Start is called before the first frame update
    public override void Start()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.playerSpawnTransform != null)
            {
                GameManager.instance.players.Add(this);
            }
        }

        base.Start();

        if (UILives != null)
        {
            UILives.text = "" + lives;
        }
        treasureLeft = GameManager.instance.treasure.Count;
        if (UITreasure != null)
        {
            UITreasure.text = "" + treasureLeft;
        }

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (pawn != null)
        {
            ProcessInputs();
        }
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.playerSpawnTransform != null)
            {
                GameManager.instance.players.Remove(this);
            }
        }
    }

    public override void RespawnPlayer()
    {
        GameManager.instance.playerSpawnTransform = GameManager.instance.FindRandomSpawn<PlayerSpawnPoint>(GameManager.instance.playerSpawns);

        GameObject newPawnObj = Instantiate(GameManager.instance.piratePawnPrefab, GameManager.instance.playerSpawnTransform.position, GameManager.instance.playerSpawnTransform.rotation);

        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        this.pawn = newPawn;
        newPawn.controller = this;

        
    }

    public override void AddToScore(float amount)
    {
        score = amount + score;
        if (UIScore != null)
        {
            UIScore.text = "Score: " + score;
        }
    }

    public override void RemoveScore(float amount)
    {
        score = score - amount;
        if (UIScore != null)
        {
            UIScore.text = "Score: " + score;
        }
    }

    public override void AddLives(float amount)
    {
        lives = amount + lives;
        if (lives >= 0)
        {
            RespawnPlayer();
        }
        if (UILives != null)
        {
            UILives.text = "" + lives;
        }
    }

    public override void RemoveLives(float amount)
    {
        lives = lives - amount;
        if (lives >= 0)
        {
            RespawnPlayer();
        }
        else if (lives < 0)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.UIGameOver.text = "Game Over...";
                GameManager.instance.ActivateGameOver();
            }
        }
        if (UILives != null)
        {
            UILives.text = "" + lives;
        }
    }
    public override void UpdateTreasure()
    {
        treasureLeft = GameManager.instance.treasure.Count;
        if (UITreasure != null)
        {
            UITreasure.text = "" + treasureLeft;
        }
        if (treasureLeft <= 0)
        {
            GameManager.instance.UIGameOver.text = "YOU WIN!!!";
            GameManager.instance.ActivateGameOver();
        }
    }

    public void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }

        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }

        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }

        if (Input.GetKeyDown(sneakKey))
        {
            pawn.Sneak();
            if (!pawn.isPlaying)
            {
                pawn.StartCoroutine(pawn.PlayParticleSystem());
                pawn.isPlaying = true;
            }
        }

        if (Input.GetKeyUp(sneakKey))
        {
            pawn.StopSneak();
            if (pawn.isPlaying)
            {
                StopCoroutine(pawn.PlayParticleSystem());
                pawn.smokeScreen.Stop();
                pawn.isPlaying = false;
            }
        }
    }
}
