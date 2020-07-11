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
        GameObject bullet = Instantiate(projectile, gameObject.transform);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1) * shotSpeed, ForceMode2D.Impulse);
    }

    void Update()
    {
        //TODO: Change to GetButton when we start rate limiting
        if (Input.GetButtonDown("Fire1")){
             Fire();
        }
        
    }
}
