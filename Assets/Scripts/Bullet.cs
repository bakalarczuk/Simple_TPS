using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float damageFactor = 125f;
	private float timer = 0;

	public void Update()
	{
		//Bullet is destroyed after 3 seconds from shoot
		timer += Time.deltaTime;
		if(timer >= 3)
		{
			Destroy(gameObject);
		}
	}
	public void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.CompareTag("Player")) { return; }
		Debug.Log("COLLISION: "+ collision.transform);
		Enemy e = collision.transform.GetComponent<Enemy>();
		Destroy(gameObject);
		if (e != null) { e.SetDamage(damageFactor); }
	}
}
