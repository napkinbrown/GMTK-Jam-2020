using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public YouDied deathMessage;
    public int waveWaitTime;
    
     void OnEnable() {
        NextWave();
        EventManager.StartListening(EventNames.WAVE_END, NextWave);
        EventManager.StartListening(EventNames.PLAYER_DIED, HeDead);
    }

    void OnDisable() {
        EventManager.StopListening(EventNames.PLAYER_DIED, HeDead);
    }
    
    void HeDead() {
        deathMessage.GetComponent<Text>().enabled = true;
        Invoke("RestartGame", 5f);
    }

    void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    void NextWave() {
        StartCoroutine(BeginWave());
    }

    IEnumerator BeginWave() {
        yield return new WaitForSeconds(waveWaitTime);
        EventManager.TriggerEvent(EventNames.WAVE_START);
    }
}
