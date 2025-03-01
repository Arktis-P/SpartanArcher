using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] mapPrefabs;
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;

    Vector3 playerSpawnPoint = new Vector3(-7, 0, 0);
    Vector3 bossSpawnPoint = new Vector3(25, 0, 0);

    private GameObject lastMap;

    public void Init()
    {
        if (mapPrefabs == null)  // process exception
        { Debug.LogError("Map Prefabs are not founded!"); return; }

        LoadRandomMap();
    }

    public void LoadTutorialMap()
    {
        if (lastMap != null) Destroy(lastMap);

        ResetPlayerPosition();

        lastMap = Instantiate(mapPrefabs[0]);
    }

    private int RandomMapCount()
    {   
        int mapCounts = mapPrefabs.Count();
        int randomMapCount = Random.Range(0, mapCounts);
        return randomMapCount;
    }
    public void LoadRandomMap()  // load random map on start
    {
        if (lastMap != null)  // destroy last map prefab
        {
            Destroy(lastMap);
        }

        ResetPlayerPosition();

        int randomMapCount = RandomMapCount();
        lastMap = Instantiate(mapPrefabs[randomMapCount]);
    }

    // spawn entities (player, monsters & boss)
    public void ResetPlayerPosition()
    {
        player.transform.position = playerSpawnPoint;
        //Instantiate(player);
    }
}
