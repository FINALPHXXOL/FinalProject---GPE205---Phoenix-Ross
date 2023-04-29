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
    public GameObject islandGenerator;
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
    public int currentLevel = 1;
    private int index;
    private int listlength;

    // Game States
    public GameObject AllMenus;
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;
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
        ActivateTitleScreen();
        
    }

    private void DeactivateAllStates()
    {
        // Deactivate all Game States
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }

    public void QuitTheGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ActivateTitleScreen()
    {
        AllMenus.SetActive(true);
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        TitleScreenStateObject.SetActive(true);
    }

    public void ActivateMainMenuScreen()
    {
        AllMenus.SetActive(true);
        // Deactivate all states
        DeactivateAllStates();
        // Activate the main menu screen
        MainMenuStateObject.SetActive(true);
    }

    public void ActivateOptionsScreen()
    {
        AllMenus.SetActive(true);
        // Deactivate all states
        DeactivateAllStates();
        // Activate the options screen
        OptionsScreenStateObject.SetActive(true);
    }

    public void ActivateCredits()
    {
        AllMenus.SetActive(true);
        // Deactivate all states
        DeactivateAllStates();
        // Activate the credits screen
        CreditsScreenStateObject.SetActive(true);
    }

    public void ActivateGameplay()
    {
        IslandGenerator map = islandGenerator.GetComponent<IslandGenerator>();
        map.GenerateIsland();

        // Deactivate all states
        DeactivateAllStates();
        AllMenus.SetActive(false);
        // Activate the gameplay screen
        GameplayStateObject.SetActive(true);
    }

    public void ActivateGameOver()
    {
        Debug.Log("Why");
        DestroyAllPlayerControllers();
        DestroyAllAIControllers();
        DestroyAllPawns();
        DestroyAllTreasure();

        IslandGenerator map = islandGenerator.GetComponent<IslandGenerator>();
        map.DestroyIsland(map.currentLevelPrefab);
        if (AllMenus != null)
        {
            AllMenus.SetActive(true);
        }
        
        // Deactivate all states
        DeactivateAllStates();
        // Activate the game over screen
        GameOverScreenStateObject.SetActive(true);
    }

    public void DestroyAllPlayerControllers()
    {
        // Loop through the list of objects to destroy
        foreach (PlayerController obj in players)
        {
            // Destroy the current object
            Destroy(obj.gameObject);
        }

        // Clear the list to remove all references to the destroyed objects
        //objectsToDestroy.Clear();
    }

    public void DestroyAllAIControllers()
    {
        // Loop through the list of objects to destroy
        foreach (AIController obj in enemyAIs)
        {
            // Destroy the current object
            Destroy(obj.gameObject);
        }

        // Clear the list to remove all references to the destroyed objects
        //objectsToDestroy.Clear();
    }

    public void DestroyAllTreasure()
    {
        TreasurePickup[] objectsToDelete = FindObjectsOfType<TreasurePickup>();

        foreach (TreasurePickup obj in objectsToDelete)
        {
            Destroy(obj.gameObject);
        }
    }

    public void DestroyAllPawns()
    {
        Pawn[] objectsToDelete = FindObjectsOfType<Pawn>();

        foreach (Pawn obj in objectsToDelete)
        {
            Destroy(obj.gameObject);
        }
    }

    public Transform FindRandomSpawn<T>(List<T> list) where T : Component
    {
        T randomT = list[UnityEngine.Random.Range(0, list.Count)];

        GameObject randomObj = randomT.gameObject;
        
        Transform itemTransform = randomObj.transform;

        if (!(randomT is PlayerSpawnPoint))
        {
            list.Remove(randomT);
            Destroy(randomObj);
        }

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

            CaptainSpawnPoint ways = enemyAISpawnTransform.GetComponent<CaptainSpawnPoint>();

            foreach (Transform obj in ways.waypoints)
            {
                int arrayLength = newController.waypoints.Length;
                Array.Resize(ref newController.waypoints, arrayLength + 1);
                newController.waypoints[arrayLength] = obj;
            }

            newController.isPatrolLoop = true;
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
