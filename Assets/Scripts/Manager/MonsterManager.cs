using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> monsterPrefabs;

    [SerializeField] Rect spawnAreas;
    private List<MonsterController> activeMonsters = new List<MonsterController>();

    GameManager gameManager;

    private void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    // select random monster to spawn
    private void SpawnRandomMonster()
    {
        if (monsterPrefabs.Count == 0) { Debug.LogError("Cannot find Monster Prefabs!"); return; }

        // get random monster from prefab list
        GameObject randomPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];
        // get random position from entire spawn area
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreas.xMin, spawnAreas.xMax),
            Random.Range(spawnAreas.yMin, spawnAreas.yMax)
            );

        GameObject spawnedMonster = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        MonsterController monsterController = spawnedMonster.GetComponent<MonsterController>();

        // init monster controller  // make monster object work
    }

    public void RemoveMonster()  
    {
        // remove monsters which are already dead
        // remove monsters only if player cleared the stage
    }
}
