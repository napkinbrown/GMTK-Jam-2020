using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerTester : MonoBehaviour
{
    public readonly string TEST_EVENT = "Test Event";

    void OnEnable() {
        EventManager.StartListening(TEST_EVENT, TestEventAction);
    }

    void OnDisable() {
        EventManager.StopListening(TEST_EVENT, TestEventAction);
    }

    void Update()
    {
        if(Input.GetKeyDown("space")) {
            Debug.Log("Event fired!");
            EventManager.TriggerEvent(TEST_EVENT);
        }
    }

    void TestEventAction() {
        Debug.Log("Event heard!");
    }
}
