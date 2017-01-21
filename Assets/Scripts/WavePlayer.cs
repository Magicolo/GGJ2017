using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class WavePlayer : MonoBehaviour
{
	public float AmplitudeSpeed = 1f;
	public float FrequencySpeed = 1f;

	void Start()
	{
		LevelManager.Instance.OnLost += OnLost;
	}

	void FixedUpdate()
	{
		UpdateInput(new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")), Time.fixedDeltaTime);
	}

	void UpdateInput(Vector2 input, float deltaTime)
	{
		var wave = LevelManager.Instance.Wave;
		float frequency = Mathf.Clamp(wave.Frequency - input.x * FrequencySpeed * deltaTime, 1f, 5f);
		var position = LevelManager.Instance.MainCamera.WorldToViewportPoint(transform.position);

		float offset = wave.Frequency * position.x * Mathf.PI * 2f + wave.Offset - frequency * position.x * Mathf.PI * 2f;
		wave.Offset = offset;
		wave.Frequency = frequency;
		wave.Amplitude = Mathf.Clamp(wave.Amplitude + input.y * AmplitudeSpeed * deltaTime, 0.1f, 0.45f);
	}

	void OnLost()
	{
		LevelManager.Instance.OnLost -= OnLost;
		Destroy(gameObject);
	}
}
