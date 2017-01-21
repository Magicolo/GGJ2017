using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	public Vector2 PlayerBounds = new Vector2(0f, 0.75f);
	public float Speed = 0.5f;

	void Awake()
	{
		Instance = this;
	}
}
