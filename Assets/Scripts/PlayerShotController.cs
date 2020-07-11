using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    
    public float fireRatePerSecond = 2;
    public int shotSpeed = 200;
    public GameObject projectile;

    private bool canShoot = true;

     void Fire()
     {
         // Need to rate limit
         if (canShoot){
            // Have to create a bullet
            GameObject bullet = Instantiate(projectile, gameObject.transform);
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1) * shotSpeed, ForceMode2D.Impulse);

            //Go cooldown
            StartCoroutine(ShootCooldown());
         }
        
     }

     IEnumerator ShootCooldown() {
         canShoot = false;
         yield return new WaitForSeconds(1/fireRatePerSecond);
         canShoot = true;
     }

    void Update()
    {
        //TODO: Change to GetButton when we start rate limiting
        if (Input.GetButton("Fire1")){
             Fire();
        }
        
    }
}
