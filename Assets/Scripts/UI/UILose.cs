using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UILose : MonoBehaviour
{
	public GameObject Panel;

	void Start()
	{
		LevelManager.Instance.OnLost += OnLost;
	}

	void OnLost()
	{
		LevelManager.Instance.OnLost -= OnLost;
		Panel.SetActive(true);
	}

	public void OnRestart()
	{
		LevelManager.Instance.Restart();
	}
}
