using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject normalWeapon;
    public GameObject heavyWeapon;
    public Transform projectileHandle;

    public int heavyDamage = 125;
    public int lightDamage = 25;


    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(normalWeapon, projectileHandle.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Bullet>().damageFactor = lightDamage;
            bullet.GetComponent<Rigidbody>().AddForce(projectileHandle.forward * 200);
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject bullet = Instantiate(heavyWeapon, projectileHandle.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Bullet>().damageFactor = heavyDamage;
            bullet.GetComponentInChildren<Rigidbody>().AddForce(projectileHandle.forward * 400);
        }
    }

    public void PowerUp()
	{
        StartCoroutine(PUTimer());
	}

    IEnumerator PUTimer()
	{
        float duration = 5f;
                             
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        heavyDamage /= 2;
        lightDamage /= 2;
    }
}
