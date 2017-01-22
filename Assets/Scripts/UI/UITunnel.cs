using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UITunnel : MonoBehaviour
{
	public Slider Slider;
	public Image Background;
	public float ColorSpeed = 3f;

	ParticlePlayer player;

	void Update()
	{
		player = player == null ? FindObjectOfType<ParticlePlayer>() : player;

		if (player == null)
			return;

		Slider.value = player.CooldownRatio;

		if (Slider.value >= 1f)
			Background.color = Background.color.HueShift(Time.deltaTime * ColorSpeed * 5f);
		else
			Background.color = Background.color.HueShift(Time.deltaTime * ColorSpeed);
	}
}
