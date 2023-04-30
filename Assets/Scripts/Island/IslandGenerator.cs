using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    public GameObject[] levelPrefabs;
    public GameObject currentLevelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateIsland()
    {
        // Instantiate the prefab to create a new instance
        GameObject newLevelPrefab = levelPrefabs[GameManager.instance.currentLevel - 1];
        GameObject newLevel = Instantiate(newLevelPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Set the parent of the new instance
        //newLevel.transform.SetParent(transform);

        currentLevelPrefab = newLevel;

        GameManager.instance.Invoke("SpawnPlayer", 2.5f);
        GameManager.instance.Invoke("SpawnAI", 0.1f);
        GameManager.instance.Invoke("SpawnTreasure", 0.1f);


    }

    public void DestroyIsland(GameObject parentObject)
    {
        // Iterate through all child objects of the parent object
        for (int i = parentObject.transform.childCount - 1; i >= 0; i--)
        {
            // Get a reference to the current child object
            GameObject childObject = parentObject.transform.GetChild(i).gameObject;

            // Destroy the child object
            Destroy(childObject);
        }
        Destroy(parentObject);
    }
}
