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
    public GameObject AIOfficerPrefab;
    public GameObject AICaptainPrefab;
    public GameObject OfficerPawnPrefab;
    public GameObject CaptainPawnPrefab;
    public GameObject treasurePrefab;
    public Transform playerSpawnTransform;
    public Transform enemyAISpawnTransform;
    public Transform treasureSpawnTransform;
    public List<PlayerController> players;
    public List<AIController> enemyAIs;
    public List<PickupSpawner> pickupSpawns;
    public List<PlayerSpawnPoint> playerSpawns;
    public List<OfficerSpawnPoint> officerSpawns;
    public List<CaptainSpawnPoint> captainSpawns;
    public List<TreasurePickup> treasure;
    public int officerCount;
    public int captainCount;
    public int treasureCount;
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
        pickupSpawns = new List<PickupSpawner>();
        playerSpawns = new List<PlayerSpawnPoint>();
        officerSpawns = new List<OfficerSpawnPoint>();
        captainSpawns = new List<CaptainSpawnPoint>();
    }

    // Update is called once per frame
    void Start()
    {
        Invoke("SpawnPlayer", 1f);
        Invoke("SpawnAI", 1f);
        Invoke("SpawnTreasure", 1f);
        //SpawnPlayer();
        //SpawnAI();
    }

    public Transform FindRandomSpawn<T>(List<T> list) where T : Component
    {
        Transform itemTransform = list[UnityEngine.Random.Range(0, list.Count)].transform;
        return itemTransform;
    }

    public void SpawnPlayer()
    {
        playerSpawnTransform = FindRandomSpawn<PlayerSpawnPoint>(playerSpawns);

        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(piratePawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
        newPawn.controller = newController;
    }

    public void SpawnAI()
    {
        for (int i = 0; i < officerCount; i++)
        {
            enemyAISpawnTransform = FindRandomSpawn<OfficerSpawnPoint>(officerSpawns);

            GameObject newAIObj = Instantiate(AIOfficerPrefab, Vector3.zero, Quaternion.identity);
            GameObject newPawnObj = Instantiate(OfficerPawnPrefab, enemyAISpawnTransform.position, enemyAISpawnTransform.rotation);

            AIController newController = newAIObj.GetComponent<AIController>();
            Pawn newPawn = newPawnObj.GetComponent<Pawn>();

            newController.pawn = newPawn;
            newPawn.controller = newController;

            OfficerSpawnPoint ways = enemyAISpawnTransform.GetComponent<OfficerSpawnPoint>();

            foreach (Transform obj in ways.waypoints)
            {
                int arrayLength = newController.waypoints.Length;
                Array.Resize(ref newController.waypoints, arrayLength + 1);
                newController.waypoints[arrayLength] = obj;
            }

            newController.isPatrolLoop = true;
        }

        for (int i = 0; i < captainCount; i++)
        {
            enemyAISpawnTransform = FindRandomSpawn<CaptainSpawnPoint>(captainSpawns);

            GameObject newAIObj = Instantiate(AICaptainPrefab, Vector3.zero, Quaternion.identity);
            GameObject newPawnObj = Instantiate(CaptainPawnPrefab, enemyAISpawnTransform.position, enemyAISpawnTransform.rotation);

            AIController newController = newAIObj.GetComponent<AIController>();
            Pawn newPawn = newPawnObj.GetComponent<Pawn>();

            newController.pawn = newPawn;
            newPawn.controller = newController;

            int arrayLength = newController.waypoints.Length;
            Array.Resize(ref newController.waypoints, arrayLength + 1);
            newController.waypoints[arrayLength] = enemyAISpawnTransform;
        }
    }

    public void SpawnTreasure()
    {
        for (int i = 0; i < treasureCount; i++)
        {
            treasureSpawnTransform = FindRandomSpawn<PickupSpawner>(pickupSpawns);

            GameObject newChest = Instantiate(treasurePrefab, treasureSpawnTransform.position, treasureSpawnTransform.rotation);
        }
    }
}
