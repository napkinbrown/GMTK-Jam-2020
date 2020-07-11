using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    
    public float rateOfFirePerSecond = 2;
    public int shotSpeed = 200;
    public GameObject projectile;

    private bool canShoot = true;

     void Fire()
     {
         // Need to rate limit
         if (canShoot){
            // Have to create a bullet
            GameObject go = Instantiate(projectile, gameObject.transform);
            SprinkleScript bullet = go.GetComponent<SprinkleScript>();
            bullet.targetVector = new Vector3(1,1,0);
            bullet.speed = shotSpeed;

            //Go cooldown
            StartCoroutine(ShootCooldown());
         }
        
     }

     IEnumerator ShootCooldown() {
         canShoot = false;
         yield return new WaitForSeconds(1/rateOfFirePerSecond);
         canShoot = true;
     }

    // Update is called once per frame
    void Update()
    {
        //TODO: Change to GetButton when we start rate limiting
        if (Input.GetButton("Fire1")){
             Fire();
        }
        
    }
}
