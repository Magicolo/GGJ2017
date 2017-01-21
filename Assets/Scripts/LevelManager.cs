using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	public event Action OnLost;

	public Vector2 PlayerBounds = new Vector2(0f, 0.75f);
	public float Speed = 0.5f;
	public float Increment = 0.01f;

	public Camera MainCamera;
	public Camera UICamera;
	public Wave Wave;

	public bool HasLost { get; private set; }

	public float LightSpeedRatio
	{
		get { return 1f - 1f / Mathf.Pow(Speed * 4f, Speed / 4f); }
	}

	public void Lose()
	{
		HasLost = true;

		if (OnLost != null)
			OnLost();
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void Awake()
	{
		Instance = this;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			Restart();
			return;
		}
		else if (HasLost)
			return;

		Speed += Increment * Time.deltaTime;

		if (LightSpeedRatio >= 1f)
			SceneManager.LoadScene("Crash");
	}
}
