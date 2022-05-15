using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public Transform firepoint;
    public GameObject bulletPrefab;
    public int damage = 40;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot() {
        // shooting logic
        
        // Prefab logic
        // Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);

        // Raycast logic
        RaycastHit2D hitInfo = Physics2D.Raycast(firepoint.position, firepoint.right);

        if(hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

            lineRenderer.SetPosition(0, firepoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);

        } else
        {
            lineRenderer.SetPosition(0, firepoint.position);
            lineRenderer.SetPosition(1, firepoint.position + firepoint.right * 100);
        }

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;
    }
}
