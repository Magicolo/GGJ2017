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
		float ratio = LevelManager.Instance.LightSpeedRatio;
		Text.text = string.Format("{0}% Light Speed", ratio * 100f);

		float danger = Mathf.Pow(ratio, 8f);
		float random = UnityEngine.Random.Range(-danger, danger);
		Text.color = new Color(Mathf.Pow(ratio, 2f) + random, 1f - danger + random, 1f - danger + random, 1f);
	}
}
