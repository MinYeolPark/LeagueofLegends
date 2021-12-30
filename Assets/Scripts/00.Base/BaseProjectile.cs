using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    //Test
    public float shootForce = 0f;

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = (GameObject)Instantiate(
                bullet, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shootForce);
        }
    }
}
