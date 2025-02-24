using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] mapPrefabs;
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;

    Vector3 playerSpawnPoint = new Vector3(-7, 0, 0);
    Vector3 bossSpawnPoint = new Vector3(25, 0, 0);

    public void Init()
    {
        LoadRandomMap();
    }

    private int RandomMapCount()
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
    public void SpawnPlayer()
    {
        player.transform.position = playerSpawnPoint;
        Instantiate(player);
    }
    public void SpawnBoss()
    {
        boss.transform.position = bossSpawnPoint;
        Instantiate(boss);
    }
}
