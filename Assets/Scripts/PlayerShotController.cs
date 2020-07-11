using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    
    public float fireRatePerSecond = 2;
    public int shotSpeed = 200;
    public GameObject reticle;
    public float reticleDepth;
    public float responsivenessPercentage;

    public GameObject projectile;

    private ReticleController reticleController;
    private bool canShoot = true;

    private bool isFreezing = false;

    void OnEnable() {
        EventManager.StartListening(EventNames.FREEZE_START, FreezeStart);
        EventManager.StartListening(EventNames.FREEZE_STOP, FreezeStop);
    }

    void OnDisable() {
        EventManager.StopListening(EventNames.FREEZE_START, FreezeStart);
        EventManager.StopListening(EventNames.FREEZE_STOP, FreezeStop);
    }

    void Start() {
        reticle = Instantiate(reticle);
        SetReticleParameters();
    }

    void SetReticleParameters() {
        reticleController = reticle.GetComponent<ReticleController>();
        reticleController.reticleDepth = this.reticleDepth;
        reticleController.responsivenessPercentage = this.responsivenessPercentage;
    }

     void Fire()
     {
         if (canShoot && !this.isFreezing){
            GameObject bullet = Instantiate(projectile, gameObject.transform);
            bullet.GetComponent<Rigidbody2D>().AddForce(GetReticleDirection() * shotSpeed, ForceMode2D.Impulse);

            //Go cooldown
            StartCoroutine(ShootCooldown());
         }
        
     }

    private Vector2 GetReticleDirection() {
        Vector2 heading = reticle.transform.position - this.transform.position;
        return heading.normalized;
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

        void FreezeStart() 
    {
        isFreezing = true;
    }

    void FreezeStop() 
    {
        isFreezing = false;
    }

}
