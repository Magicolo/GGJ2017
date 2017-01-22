using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{

	public int Width;
	public bool IsHard = false;

	private bool CanFlipInX = true;
	public bool CanFlipInY = true;

	public float ySpeed = 0;


	private void OnDrawGizmos()
	{
		Vector3 size = new Vector3(Width, 100, 0);
		if (IsHard)
			Gizmos.color = Color.red;
		else
			Gizmos.color = Color.green;

		var offset = Vector3.zero;
		if (transform.localScale.y <= 0)
			offset = new Vector3(0, -100, 0);

		Gizmos.DrawWireCube(transform.position + size / 2 + offset, size);
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
