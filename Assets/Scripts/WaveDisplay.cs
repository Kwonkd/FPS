using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{
    public WaveManager waveManager; // 웨이브 매니저 참조 변수
    public TextMeshProUGUI waveDisplay;
    public TextMeshProUGUI enemiesLeftDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waveDisplay.text = "Wave " + waveManager.wave;
        enemiesLeftDisplay.text = "EnemiesLeft " + waveManager.enemiesLeft;
    }
}
