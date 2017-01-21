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

	void Update()
	{
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
				return Mathf.Sin(time * Mathf.PI * 2f * Frequency + Offset) * Amplitude + Center;
			default:
				return time;
		}
	}
}
