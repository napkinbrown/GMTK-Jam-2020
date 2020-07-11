using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeController : MonoBehaviour
{
    private bool isFreezing = false;
    private bool waitForHealTick = false;
    public float initHealRatePerSecond = .6f;
    public float healthRateIncrease = .2f;

    public int ticksBetweenIncrease = 3;

    private int tickCount = 0;
    private float healRate;


    void Start()
    {
        healRate = initHealRatePerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) {
            FreezeStart();
        }
        else if (Input.GetButtonUp("Fire2")) {
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
        tickCount = 0;

        //Handle particles
    }

    IEnumerator FreezeTick() 
    {
        // If I got here then I'm in freeze mode. I need to wait X number of seconds before I can do a heal tick. So this should be a coroutine
        waitForHealTick = true;
        yield return new WaitForSeconds(1/healRate);
        //If I let up the button before this timout is done then I'm not freezing anymore
        if(waitForHealTick) 
        {
            waitForHealTick = false;
            tickCount += 1;

            if(tickCount % ticksBetweenIncrease == 0) 
            {
                //modify healrate
                healRate += healthRateIncrease;
            }

            //Let everyone know we're good to go
            EventManager.TriggerEvent(EventNames.FREEZE_TICK);
        }
    }
}
