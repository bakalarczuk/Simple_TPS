using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public Text scoreValue;
	public Text stateValue;

	private int score = 0;

	public void OnEnable()
	{
		Enemy.OnScored += Enemy_OnScored;
		InputController.OnStateChanged += InputController_OnStateChanged;
	}

	public void OnDisable()
	{
		Enemy.OnScored -= Enemy_OnScored;
		InputController.OnStateChanged -= InputController_OnStateChanged;
	}

	public void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.lockState = CursorLockMode.None;
		scoreValue.text = score.ToString("D4");
	}

	private void InputController_OnStateChanged(string state)
	{
		stateValue.text = state;
	}

	private void Enemy_OnScored()
	{
		score += 1;
		scoreValue.text = score.ToString("D4");
	}

}
