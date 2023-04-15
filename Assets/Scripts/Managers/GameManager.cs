using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;

    //Prefabs
    public GameObject playerControllerPrefab;
    public GameObject piratePawnPrefab;
    public GameObject AIControllerPrefab;
    public GameObject AIPiratePawnPrefab;
    public Transform playerSpawnTransform;
    public Transform enemyAISpawnTranform;
    public List<PlayerController> players;
    public List<AIController> enemyAIs;
    public List<PawnSpawnPoint> spawns;
    public AudioSource deathAudio;
    public AudioClip deathClip;
    public int enemyCount;
    private int index;
    private int listlength;
    #endregion Variables

    // Game States
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        players = new List<PlayerController>();
        enemyAIs = new List<AIController>();
        spawns = new List<PawnSpawnPoint>();

    }

    // Update is called once per frame
    void Start()
    {
        SpawnPlayer();
    }

    public Transform FindRandomSpawn()
    {
        Transform itemTransform = spawns[UnityEngine.Random.Range(0, spawns.Count)].transform;
        return itemTransform;
    }

    public void SpawnPlayer()
    {
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(piratePawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
        newPawn.controller = newController;
    }

    public void SpawnAI()
    {
        enemyAISpawnTranform = FindRandomSpawn();
        GameObject newAIObj = Instantiate(AIControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(AIPiratePawnPrefab, enemyAISpawnTranform.position, enemyAISpawnTranform.rotation);

        Controller newController = newAIObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
        newPawn.controller = newController;
    }
}
