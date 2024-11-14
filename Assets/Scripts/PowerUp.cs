using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PUType
{
	Shoot,
	Move
}

public class PowerUp : MonoBehaviour
{
	public PUType puType = PUType.Shoot;

	public void OnCollisionEnter(Collision collision)
	{
		gameObject.SetActive(false);
		if (puType == PUType.Shoot) {
			collision.collider.GetComponent<ShootingController>().heavyDamage *= 2;
			collision.collider.GetComponent<ShootingController>().lightDamage *= 2;
			collision.collider.GetComponent<ShootingController>().PowerUp();
		}
		if(puType == PUType.Move)
		{
			collision.collider.GetComponent<CharacterController>().moveSpeed *= 1.5f;
			collision.collider.GetComponent<CharacterController>().jumpSpeed *= 1.5f;
			collision.collider.GetComponent<CharacterController>().PowerUp();
		}
	}
}
