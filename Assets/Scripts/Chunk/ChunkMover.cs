using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
	public Chunk chunk;
	public float SpeedModifier;

	public float ySpeed;

	void Start()
	{
		chunk = GetComponent<Chunk>();
	}

	// Update is called once per frame
	void Update()
	{
		var x = -LevelManager.Instance.Speed * SpeedModifier * Time.deltaTime;
		var y = Mathf.Sin(Time.time) * ySpeed;
		transform.Translate(x, 0, 0);
		transform.position.Set(transform.position.x, y, 0);
	}
}
