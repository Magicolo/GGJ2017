using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICredits : MonoBehaviour
{
	public void OnBack()
	{
		SceneManager.LoadScene("Intro");
	}
}
