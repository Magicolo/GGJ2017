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
	void FixedUpdate()
	{
		var x = -LevelManager.Instance.Speed * ChunkManager.instance.SpeedModifier * Time.fixedDeltaTime;
		var y = Mathf.Sin(Time.time) * chunk.ySpeed;

		var newP = new Vector3(body.position.x + x, y - 50 + yOffset);
		body.MovePosition(newP);

		if (body.position.x < -chunk.Width - 700)
		{
			Debug.LogWarning("A chunk went too fast and didn't touch the kémera's limitezs! I have déstroyaide it for ya butte it shouldn't apande...");
			Destroy(gameObject);
		}

	}
}
