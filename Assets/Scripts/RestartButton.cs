using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{ 
    public void Restartbutton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene2");
    }
    public void NextStage()
    {
        WaveManager waveManager = GetComponent<WaveManager>();
        waveManager.StartNextWave();
    }
}
