using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] mapPrefabs;
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject[] monsterPrefabs;

    Vector3 playerSpawnPoint = new Vector3(-7, 0, 0);
    Vector3 bossSpawnPoint = new Vector3(25, 0, 0);
    // values for random spawn area for monsters
    float minMonsterSpawnX = -3f;
    float maxMonsterSpawnX = 21f;
    float minMonsterSpawnY = -3.5f;
    float maxMonsterSpawnY = 3.5f;

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
    public void SpawnMonster()
    {
        // get a random monster from the list
        int rand = Random.Range(0, monsterPrefabs.Count());
        GameObject monster = monsterPrefabs[rand];

        // get a random point in spawn area
        float spawnPointX = Random.Range(minMonsterSpawnX, maxMonsterSpawnX);
        float spawnPointY = Random.Range(minMonsterSpawnY, maxMonsterSpawnY);
        Vector3 monsterSpawnPoint = new Vector3(spawnPointX, spawnPointY, 0);

        monster.transform.position = monsterSpawnPoint;  // set the point

        Instantiate(monster);  // spawn monster
    }
}
