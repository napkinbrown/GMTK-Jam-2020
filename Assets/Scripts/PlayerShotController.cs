using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    
    public float fireRatePerSecond = 2;
    public int shotSpeed = 200;
    public GameObject reticle;
    public float phase1ResponsivenessPercentage;
    public float phase2ResponsivenessPercentage;
    public float phase3ResponsivenessPercentage;

    public GameObject projectile;

    public PlayerHealthController healthController;

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
        reticleController.reticleDepth = Camera.main.WorldToScreenPoint(this.transform.transform.position).z;
        reticleController.responsivenessPercentage = GetResponsivenessPercentage();
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

    private float GetResponsivenessPercentage()
    {
        switch(healthController.GetHealthPhase())
        {
            case PlayerHealthController.HealthPhase.Solid:
               return phase1ResponsivenessPercentage;
         
            case PlayerHealthController.HealthPhase.Melty:
                return phase2ResponsivenessPercentage;
            
            case PlayerHealthController.HealthPhase.Melted:
                return phase3ResponsivenessPercentage;
        }
        return phase1ResponsivenessPercentage;
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
        reticleController.responsivenessPercentage = GetResponsivenessPercentage();
    
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
