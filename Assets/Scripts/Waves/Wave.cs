using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum Shape
{
	Sine,
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
		offset = offset + LevelManager.Instance.Speed * Time.deltaTime;
		offset %= Mathf.PI * 2f;
		var positions = new Vector3[Definition];

		for (int i = 0; i < positions.Length; i++)
		{
			float ratio = (float)i / Definition;
			var position = new Vector3(ratio, Solve(ratio), -Camera.main.transform.position.z);
			positions[i] = Camera.main.ViewportToWorldPoint(position);
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
			default:
				return time;
		}
	}
}
