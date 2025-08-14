using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject stageClearUI;
    public GameObject gameClearUI;
    public GameObject waveManager;

    private void Start()
    {

    }
    public void StageClear()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        WaveManager wManager = waveManager.GetComponent<WaveManager>();
        if(wManager != null && wManager.wave >3)
        {
            gameClearUI.SetActive(true);
        }
        else
        {
            stageClearUI.SetActive(true);
        }
    }
    public void NextStageButton()
    {
        WaveManager manager = waveManager.GetComponent<WaveManager>();
        if (manager != null)
        {
            Time.timeScale = 1;
            manager.StartNextWave();
        }
    }
}
