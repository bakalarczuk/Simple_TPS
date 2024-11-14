using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public delegate void Scored();
	public static event Scored OnScored;

	[SerializeField] private Image healthBar;
    [SerializeField] private float health = 100;
    [SerializeField] private float currentHealth = 100;
    [SerializeField] private float healthPercentage = 100;
    [SerializeField] private Material[] materials;

	private MeshRenderer renderer;
	public EnemyController controller;

	public void Start()
	{
        currentHealth = health;
        healthBar.color = Color.green;
        healthBar.fillAmount = healthPercentage;
	}

    public void SetDamage(float amount)
    {
        currentHealth -= amount;
        healthPercentage = currentHealth / health;
        healthBar.fillAmount = healthPercentage;

        healthBar.color = Color.Lerp(Color.red, Color.green, healthPercentage);

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }


    IEnumerator Die()
	{
		yield return new WaitForSeconds(1);
		controller.spawnedEnemies.Remove(gameObject);
		Destroy(gameObject);
		OnScored?.Invoke();
	}
}
