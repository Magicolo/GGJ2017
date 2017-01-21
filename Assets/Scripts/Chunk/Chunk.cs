using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{


	public int Difficulty;
	public int Width;
	public Color GizmosColor = Color.red;

	public float minSpeed = 1;
	public float maxSpeed = 1;

	public float ySpeed = 0;


	private void OnDrawGizmos()
	{
		Vector3 size = new Vector3(Width, 100, 0);
		Gizmos.color = GizmosColor;
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
