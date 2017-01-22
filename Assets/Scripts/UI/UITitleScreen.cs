using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UITitleScreen : MonoBehaviour
{
	public void OnStart()
	{
		SceneManager.LoadScene("Main");
	}

	public void OnCredits()
	{
		SceneManager.LoadScene("Credits");
	}
}
