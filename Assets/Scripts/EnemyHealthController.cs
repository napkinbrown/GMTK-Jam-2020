using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : EntityHealthController
{
    protected override void Die(){
        Debug.Log("Dead");
        EventManager.TriggerEvent(EventNames.ENEMY_DIED);
        this.gameObject.SetActive(false);
    }
}
