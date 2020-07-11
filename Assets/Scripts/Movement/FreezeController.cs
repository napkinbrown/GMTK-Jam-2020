using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeController : MonoBehaviour
{
    private bool isFreezing = false;
    private bool waitForHealTick = false;
    public float initHealRatePerSecond = .5f;
    public float healthRateIncrease = 1;
    private float healRate;


    void Start()
    {
        healRate = initHealRatePerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) {
            Debug.Log("Starting Freeze");
            FreezeStart();
        }
        else if (Input.GetButtonUp("Fire2")) {
            Debug.Log("Ending Freeze");
            FreezeEnd();
        }

        if (isFreezing && !waitForHealTick) {
            //Go cooldown
            StartCoroutine(FreezeTick());
        }
    }

    void FreezeStart()
    {
        //Fire event
        EventManager.TriggerEvent(EventNames.FREEZE_START);

        isFreezing = true;
        //Handle particles?
    }

    void FreezeEnd()
    {
        //Fire event
        EventManager.TriggerEvent(EventNames.FREEZE_STOP);
        //Reset back to initial values
        isFreezing = false;
        //We can't wait for a heal ticket anymore here
        waitForHealTick = false;
        //Reset heal rate
        healRate = initHealRatePerSecond;

        //Handle particles
    }

    IEnumerator FreezeTick() 
    {
        // If I got here then I'm in freeze mode. I need to wait X number of seconds before I can do a heal tick. So this should be a coroutine
        waitForHealTick = true;
        Debug.Log("Wait for heal tick start");
        yield return new WaitForSeconds(1/healRate);
        //If I let up the button before this timout is done then I'm not freezing anymore
        if(waitForHealTick) 
        {
            Debug.Log("heal tick happenings");
            waitForHealTick = false;
            //TODO modify healrate
            EventManager.TriggerEvent(EventNames.FREEZE_TICK);
        }
    }
}
