using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

        Debug.Log("Player respawned.");
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
            UILives.text = "Lives: " + lives;
        }
    }

    public override void RemoveLives(float amount)
    {
        lives = lives - amount;
        if (lives >= 0)
        {
            Debug.Log("Player should be respawning.");
            RespawnPlayer();
        }
        else if (lives < 0)
        {
            if (GameManager.instance != null)
            {
                Debug.Log("GAME OVER!!!!");
                GameManager.instance.ActivateGameOver();
            }
        }
        if (UILives != null)
        {
            UILives.text = "Lives: " + lives;
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
        }

        if (Input.GetKeyUp(sneakKey))
        {
            pawn.StopSneak();
        }
    }
}
