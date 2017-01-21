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

	public Camera MainCamera;
	public Camera UICamera;
	public Vector2 PlayerBounds = new Vector2(0f, 0.75f);

	public float Speed = 0.5f;

	public float LightSpeedRatio
	{
		get { return 1f - 1f / Mathf.Pow(Speed * 4f, Speed / 4f); }
	}

	public void Lose()
	{
		if (OnLost != null)
			OnLost();
	}

	public void Restart()
	{
		SceneManager.LoadScene("Main");
	}

	void Awake()
	{
		Instance = this;
	}

	void Update()
	{
		if (LightSpeedRatio >= 1f)
			SceneManager.LoadScene("Crash");
	}
}
