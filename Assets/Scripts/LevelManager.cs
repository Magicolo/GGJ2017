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
	public float Speed = 1f;

	public Camera MainCamera;
	public Camera UICamera;
	public Wave Wave;

	public float Difficulty { get { return ElapsedTime / 60f + 1f; } }
	public bool HasLost { get; private set; }
	public float ElapsedTime { get; private set; }
	public float LightSpeedRatio
	{
		get { return 1f - 1f / Mathf.Pow(Difficulty * 4f, Difficulty / 4f); }
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
		else if (LightSpeedRatio >= 1f)
		{
			SceneManager.LoadScene("Crash");
			return;
		}

		ElapsedTime += Time.deltaTime * Speed;
		UpdateBackground();
	}

	void UpdateBackground()
	{
		MainCamera.backgroundColor = MainCamera.backgroundColor.HueShift(Time.deltaTime * Difficulty * 0.025f);
	}
}
