using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletspawn;
    public float bulletVelocity = 30;
    public float bulletPrefablifetime = 3f;


    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            fireweapon();
        }
        
    }
    private void fireweapon()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletspawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bulletspawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet,bulletPrefablifetime));

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);

    }
}
