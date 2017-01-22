using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
	public Chunk chunk;
	Rigidbody2D body;
	public int yOffset;

	void Start()
	{
		chunk = GetComponent<Chunk>();
		body = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		var x = -LevelManager.Instance.Speed * ChunkManager.instance.SpeedModifier * Time.deltaTime;
		var y = Mathf.Sin(Time.time) * chunk.ySpeed;

		var newP = new Vector3(transform.position.x + x, y - 50 + yOffset);
		body.MovePosition(newP);


		if (transform.position.x < -chunk.Width - 700)
		{
			Debug.LogWarning("A chunk went too fast and didnt touch the camera's limits! I have destroied it for ya but it shoudl'nt append...");
			Destroy(gameObject);
		}

	}
}
