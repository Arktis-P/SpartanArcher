using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Coroutine stageRoutine;
    private bool monsterSpawnComplete;
    
    [SerializeField] private List<GameObject> monsterPrefabs;
    [SerializeField] private List<GameObject> bossPrefabs;

    [SerializeField] Rect spawnArea;
    public List<MonsterController> activeMonsters = new List<MonsterController>();
    public List<BossController> activeBoss = new List<BossController>();

    private Vector3 bossPos = new Vector3(25, 0, 0);

    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f);

    private static MonsterManager Instance;

    private int stage;
    GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;

        CheckErrors();

        stage = gameManager.Stage;  // get stage from game manager
        StartStage(stage);  // start monster manager
    }

    // chcek for exceptions
    private void CheckErrors()
    {
        if (monsterPrefabs.Count == 0) { Debug.LogError("Cannot find Monster Prefabs!"); }
        if (bossPrefabs.Count == 0) { Debug.LogError("Cannot find Monster Prefabs!"); }
    }

    // when start the stage, decide nums of monsters spawn
    public void StartStage(int stage)
    {
        if (stage <= 0) { return; }

        StartSpawn(setMonsterNum(stage));  // spawn monsters
        if (stage % 10 == 0) SpawnRandomBoss();  // boss spawn
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
            case 0:  // boss stage 
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
    // select random boss to spawn
    private void SpawnRandomBoss()
    {
        // gen random boss from prefab list
        GameObject randomPrefab = bossPrefabs[Random.Range(0, bossPrefabs.Count)];

        // instantiate boss instance
        GameObject spawnedBoss = Instantiate(randomPrefab, bossPos, Quaternion.identity);
        BossController bossController = spawnedBoss.GetComponent<BossController>();
        bossController.Init(this, gameManager.player.transform);

        activeBoss.Add(bossController);
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
        if (monsterSpawnComplete && activeMonsters.Count == 0 && activeBoss.Count == 0) gameManager.StageClear();  // == stage clear
    }
    public void RemoveBossOnDeath(BossController boss)
    {
        activeBoss.Remove(boss);
        if (monsterSpawnComplete && activeMonsters.Count == 0 && activeBoss.Count == 0) gameManager.StageClear();
    }
    // test for stage clearing
    public void TestDeath()
    {
        if (activeMonsters.Count == 0)
        {
            if (activeBoss.Count == 0) return;
            else
            {
                activeBoss[0].Death();  // if boss controller is on make it death
                return;
            }
        }
        int rand = Random.Range(0, activeMonsters.Count);
        MonsterController monster = activeMonsters[rand];
        monster.Death();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) TestDeath();
    }
}
