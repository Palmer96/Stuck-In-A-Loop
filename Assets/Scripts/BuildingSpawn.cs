using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buildingPrefabs;
    
    void Start()
    {
        // clear display building
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        // spawn random building from list
        SpawnBuilding();    
    }


    void SpawnBuilding()
    {
        int buildingIndex = Random.Range(0, buildingPrefabs.Length);
        Instantiate(buildingPrefabs[buildingIndex], transform);
    }
}
