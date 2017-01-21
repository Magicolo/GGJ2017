using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum Shape
{
	Sine,
	Square,
}

public class Wave : MonoBehaviour
{
	[Header("Wave Settings")]
	public Shape Shape;
	public float Frequency = 1f;
	public float Amplitude = 1f;
	public float Center;
	public float Offset;

	[Header("Render Settings")]
	public int Definition = 1000;
	public LineRenderer Line;

	float offset;

	void FixedUpdate()
	{
		offset += LevelManager.Instance.Speed * Time.deltaTime;
		offset %= Mathf.PI * 2f;

		var positions = new Vector3[Definition];

		for (int i = 0; i < positions.Length; i++)
		{
			float ratio = (float)i / Definition;
			var position = new Vector3(ratio, Solve(ratio), -LevelManager.Instance.MainCamera.transform.position.z);
			positions[i] = LevelManager.Instance.MainCamera.ViewportToWorldPoint(position);
		}

		Line.numPositions = Definition;
		Line.SetPositions(positions);
	}

	public float Solve(float time)
	{
		switch (Shape)
		{
			case Shape.Sine:
				return Mathf.Sin(time * Mathf.PI * 2f * Frequency + Offset + offset) * Amplitude + Center;
			case Shape.Square:
				return Mathf.Clamp(Mathf.Sin(time * Mathf.PI * 2f * Frequency + Offset + offset) * 10f, -1f, 1f) * Amplitude + Center;
			default:
				return time;
		}
	}
}
