using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
	public static DifficultyManager Instance { get; private set; }

	public float Speed = 0.5f;

	private void Awake()
	{
		Instance = this;
	}
}
