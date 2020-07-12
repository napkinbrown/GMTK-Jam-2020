using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHurtingShotScript : MonoBehaviour
{
    public GameObject bullet;
    public float fireDelay;
    public float fireSpeed;
    
    private bool canFire;


    void FixedUpdate()
    {
        if(canFire) {
            GameObject newBullet = Instantiate(bullet);
            newBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * fireDelay, ForceMode2D.Impulse);
            StartCoroutine(DelayFire());
        }
    }
    
    IEnumerator DelayFire() {
        canFire = false;
        yield return new WaitForSeconds(.5f);
        canFire = true;
    }
}
