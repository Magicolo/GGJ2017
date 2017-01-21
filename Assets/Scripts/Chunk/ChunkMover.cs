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
		var x = -LevelManager.Instance.Difficulty * ChunkManager.instance.SpeedModifier * Time.deltaTime;
		var y = Mathf.Sin(Time.time) * chunk.ySpeed;
		transform.Translate(x, 0, 0);
		transform.position = new Vector3(transform.position.x, y - 50);


		if (transform.position.y < -chunk.Width - 200)
		{
			Debug.LogWarning("A chunk went too fast and didnt touch the camera's limits! I have destroied it for ya but it shoudl'nt append...");
			Destroy(gameObject);
		}

	}
}
