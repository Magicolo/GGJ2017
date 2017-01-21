using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
	public static DifficultyManager Instance { get; private set; }

	public float Speed = 0.5f;

	public float IncrementPerSecond = 0.01f;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		Speed += IncrementPerSecond;
	}
}
