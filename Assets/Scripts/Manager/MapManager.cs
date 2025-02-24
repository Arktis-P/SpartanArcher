using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] mapPrefabs;

    private int RandomMapCount()  // 
    {   
        int mapCounts = mapPrefabs.Count();
        int randomMapCount = Random.Range(0, mapCounts);
        return randomMapCount;
    }
    public void LoadRandomMap()  // load random map on start
    {
        if (mapPrefabs == null)  // process exception
        { Debug.LogError("Map Prefabs are not founded!"); return; }

        int randomMapCount = RandomMapCount();
        Instantiate(mapPrefabs[randomMapCount]);
    }

    // spawn entities (player, monsters & boss)
}
