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
		var x = -LevelManager.Instance.Speed * ChunkManager.instance.SpeedModifier * Time.deltaTime;
		var y = Mathf.Sin(Time.time) * chunk.ySpeed;
		transform.Translate(x, 0, 0);
		transform.position = new Vector3(transform.position.x, y);
	}
}
