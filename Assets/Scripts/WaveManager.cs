using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner spawner;
    public int wave = 1;
    public int enemiesLeft;
    public GameObject uiManager;
    // Start is called before the first frame update
    void Start()
    {
        StartNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartNextWave()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UIManager manager = uiManager.GetComponent<UIManager>();
        manager.stageClearUI.SetActive(false);
        spawner.enemiesPerWave = 1 * wave; // 난이도 증가를 위해 각 웨이브에 해당되는 적의 수를 조절.
        spawner.StartWave();
        enemiesLeft = spawner.enemiesPerWave;
    }
    public void OnEnemyDefeated()
    {
        UIManager manager = uiManager.GetComponent<UIManager>();
        --enemiesLeft;
        if(enemiesLeft <= 0 && manager != null)
        {
            ++wave;
            //StartNextWave();
            manager.StageClear();
        }
    }
}
