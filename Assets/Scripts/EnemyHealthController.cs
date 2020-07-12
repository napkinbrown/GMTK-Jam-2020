using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : EntityHealthController
{
    protected override void Die(){
        Debug.Log("Dead");
        this.gameObject.SetActive(false);
    }
}
