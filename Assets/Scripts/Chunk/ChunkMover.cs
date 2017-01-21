using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{

	public Chunk chunk;

	void Start()
	{
		chunk = GetComponent<Chunk>();
	}

	// Update is called once per frame
	void Update()
	{
		var xSpeed = -DifficultyManager.instance.speed;
		transform.Translate(xSpeed, 0, 0);
	}
}
