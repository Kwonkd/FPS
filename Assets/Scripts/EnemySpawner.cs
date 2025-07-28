using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public GameObject enemyPrefab; // 적 프리팹 참조 변수
    public Transform[] spawnPoints; // 생성 위치를 참조할 배열
    public float spawnInterval = 2.0f; // 적 생성 간격
    public int enemiesPerWave = 5; // 웨이브당 생성 적의 수
    int spawned = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWave()
    {
        spawned = 0;

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (spawned < enemiesPerWave)
        {
            int idx = Random.Range(0, spawnPoints.Length);
            GameObject obj = Instantiate(enemyPrefab, spawnPoints[idx].position, Quaternion.identity);
            EnemyAI enemyAI = obj.GetComponent<EnemyAI>();
            if(enemyAI != null)
            {
                enemyAI.SetTarget(player);
            }
            ++spawned;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
