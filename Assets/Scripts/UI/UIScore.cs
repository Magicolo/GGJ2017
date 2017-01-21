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
		var wave = LevelManager.Instance.Wave;
		float ratio = LevelManager.Instance.LightSpeedRatio;
		string format = "";
		if (ratio > 0.95)
			format = "0000";
		else if (ratio > 0.9)
			format = "000";
		else if (ratio > 0.7)
			format = "00";
		else if (ratio > 0.5)
			format = "0";

		Text.text = string.Format("Light Speed: {0:0." + format + "}%\nWave: {1}\nFrequency: {2}\nAmplitude: {3}",
			ratio * 100f,
			wave.Shape,
			wave.Frequency,
			wave.Amplitude);

		float danger = Mathf.Pow(ratio, 8f);
		float random = UnityEngine.Random.Range(-danger, danger);
		Text.color = new Color(Mathf.Pow(ratio, 2f) + random, 1f - danger + random, 1f - danger + random, 1f);
	}
}
