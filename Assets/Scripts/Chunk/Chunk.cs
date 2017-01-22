using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{

	public int Width;
	public bool IsHard = false;

	public float ySpeed = 0;


	private void OnDrawGizmos()
	{
		Vector3 size = new Vector3(Width, 100, 0);
		if (IsHard)
			Gizmos.color = Color.red;
		else
			Gizmos.color = Color.green;

		Gizmos.DrawWireCube(transform.position + size / 2, size);
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
