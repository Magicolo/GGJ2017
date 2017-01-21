using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	public float Difficulty = 1f;

	void Awake()
	{
		Instance = this;
	}
}
