using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{

	Chunk[] chunks;
	Chunk CurrentChunk;

	public int FirstChunkStartDistance;
	public int DistanceBetweenChunks = 20;

	// Use this for initialization
	void Start()
	{
		chunks = Resources.LoadAll<Chunk>("Chunks");
		NextChunk();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void NextChunk()
	{
		var nextChunk = chunks[Random.Range(0, chunks.Length - 1)];

		var newGo = Object.Instantiate(nextChunk).gameObject;
		newGo.transform.position = Vector3.zero;
		if (CurrentChunk == null)
			newGo.transform.Translate(FirstChunkStartDistance, -50, 0);
		else
		{
			var offset = new Vector3(DistanceBetweenChunks + CurrentChunk.Width, 0, 0);
			newGo.transform.Translate(CurrentChunk.transform.position + offset);
		}

		newGo.AddComponent<ChunkMover>();

		//var body = newGo.AddComponent<Rigidbody2D>();
		var box = newGo.AddComponent<BoxCollider2D>();
		box.offset = new Vector2(nextChunk.Width / 2, 50);
		box.size = new Vector2(nextChunk.Width, 100);
		newGo.layer = LayerMask.NameToLayer("Chunk");

		CurrentChunk = nextChunk;
	}
}
