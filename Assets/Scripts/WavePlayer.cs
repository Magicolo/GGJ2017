using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class WavePlayer : MonoBehaviour
{
	public float AmplitudeSpeed = 1f;
	public float FrequencySpeed = 1f;
	public Wave Wave;

	void FixedUpdate()
	{
		UpdateInput(new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")), Time.fixedDeltaTime);
	}

	void UpdateInput(Vector2 input, float deltaTime)
	{
		Wave.Frequency = Mathf.Clamp(Wave.Frequency + input.x * FrequencySpeed * deltaTime, 1f, 5f);
		Wave.Amplitude = Mathf.Clamp(Wave.Amplitude + input.y * AmplitudeSpeed * deltaTime, 0.1f, 0.5f);
	}
}
