using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    public float rateOfFire;
    public int shotSpeed = 200;
    public GameObject projectile;

     void Fire()
     {
         // Need to rate limit
         // Have to create a bullet
         // Have to make sure that bullet has physics

        GameObject go = Instantiate(projectile, gameObject.transform);
        SprinkleScript bullet = go.GetComponent<SprinkleScript>();
        bullet.targetVector = new Vector3(1,1,0);
        bullet.speed = shotSpeed;
     }

    // Update is called once per frame
    void Update()
    {
        //TODO: Change to GetButton when we start rate limiting
        if (Input.GetButtonDown("Fire1")){
             Fire();
        }
        
    }
}
