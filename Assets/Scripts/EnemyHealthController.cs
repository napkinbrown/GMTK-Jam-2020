using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : EntityHealthController
{
    protected override void Die(){
        this.gameObject.SetActive(false);
    }
}
