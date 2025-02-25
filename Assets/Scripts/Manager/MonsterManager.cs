using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Coroutine stageRoutine;
    private bool monsterSpawnComplete;
    
    [SerializeField] private List<GameObject> monsterPrefabs;

    [SerializeField] Rect spawnArea;
    public List<MonsterController> activeMonsters = new List<MonsterController>();

    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f);

    private static MonsterManager Instance;

    private int stage;
    GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
            Instance = this;
        }

            this.gameManager = gameManager;

        stage = gameManager.Stage;  // get stage from game manager
        StartStage(stage);  // start monster manager
    }

    // when start the stage, decide nums of monsters spawn
    public void StartStage(int stage)
    {
        if (stage <= 0) { return; }

        // check boss stage here?
        StartSpawn(setMonsterNum(stage));  // spawn monsters
    }
    private int setMonsterNum(int stage)  // set random spawn number according to the stage
    {
        int monsterNum = 0;

        switch (stage = stage % 10)
        {
            case 1: case 2: case 3:
                monsterNum = Random.Range(3, 6); break;
            case 4: case 5: case 6:
                monsterNum = Random.Range(5, 10); break;
            case 7: case 8: case 9:
                monsterNum = Random.Range(9, 15); break;
            case 10:  // boss stage
                monsterNum = Random.Range(9, 12); break;
            default: monsterNum = 0; break;
        }

        return monsterNum;
    }

    private void StartSpawn(int num)
    {
        monsterSpawnComplete = false;
        
        for (int i = 0; i < num; i++) SpawnRandomMonster();

        monsterSpawnComplete = true;
    }

    // select random monster to spawn
    private void SpawnRandomMonster()
    {
        if (monsterPrefabs.Count == 0) { Debug.LogError("Cannot find Monster Prefabs!"); return; }

        // get random monster from prefab list
        GameObject randomPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];
        // get random position from entire spawn area
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnArea.xMin, spawnArea.xMax),
            Random.Range(spawnArea.yMin, spawnArea.yMax)
            );

        GameObject spawnedMonster = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        MonsterController monsterController = spawnedMonster.GetComponent<MonsterController>();
        monsterController.Init(this, gameManager.player.transform);  // init monster controller

        activeMonsters.Add(monsterController);  // make monster object work
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnArea == null) return;  // process exception

        Gizmos.color = gizmoColor;
        Vector3 center = new Vector3(spawnArea.x + spawnArea.width / 2, spawnArea.y + spawnArea.height / 2);
        Vector3 size = new Vector3(spawnArea.width, spawnArea.height);

        Gizmos.DrawCube(center, size);
    }

    public void RemoveMonsterOnDeath(MonsterController monster)  
    {
        activeMonsters.Remove(monster);  // remove from the list
        // remove monsters which are already dead
        // remove monsters only if player cleared the stage
        if (monsterSpawnComplete && activeMonsters.Count == 0)  // == stage clear
            gameManager.StageClear();
    }
}
