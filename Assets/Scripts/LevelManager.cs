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

	public Rect Bounds = Rect.MinMaxRect(0.1f, 0.1f, 0.23f, 0.83f);
	public float TimeSpeed = 1f;
	public float FlickerIntensity = 0.2f;

	public Camera MainCamera;
	public Camera UICamera;
	public Wave Wave;

	public float Speed
	{
		get { return Mathf.Clamp(Mathf.Pow(Difficulty, 0.75f), 1f, 4f); }
	}
	public float Difficulty { get { return ElapsedTime / 20f + 1f; } }
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

		ElapsedTime += Time.deltaTime * TimeSpeed;
		UpdateBackground();
	}

	void UpdateBackground()
	{
		float random = UnityEngine.Random.Range(-FlickerIntensity, FlickerIntensity) * Difficulty;
		MainCamera.backgroundColor = new Color(
			Mathf.Clamp(Mathf.Lerp(MainCamera.backgroundColor.r, MainCamera.backgroundColor.r + random, Time.deltaTime), 0, 0.1f * Difficulty),
			Mathf.Clamp(Mathf.Lerp(MainCamera.backgroundColor.g, MainCamera.backgroundColor.g + random, Time.deltaTime), 0, 0.1f * Difficulty),
			Mathf.Clamp(Mathf.Lerp(MainCamera.backgroundColor.b, MainCamera.backgroundColor.b + random, Time.deltaTime), 0, 0.1f * Difficulty),
			MainCamera.backgroundColor.a);
	}
}
