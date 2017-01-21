using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
	public Text Text;

	void Update()
	{
		Text.text = string.Format("{0}% Light Speed", LevelManager.Instance.LightSpeedRatio * 100f);
	}
}
