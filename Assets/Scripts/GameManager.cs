using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int waveWaitTime;

    void Start()
    {
        NextWave();
        EventManager.StartListening(EventNames.WAVE_END, NextWave);
    }

    private void NextWave() {
        StartCoroutine(BeginWave());
    }

    IEnumerator BeginWave() {
        yield return new WaitForSeconds(waveWaitTime);
        EventManager.TriggerEvent(EventNames.WAVE_START);
    }
}
