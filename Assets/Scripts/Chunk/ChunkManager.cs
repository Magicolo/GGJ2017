using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
	public static ChunkManager instance;

	Chunk[] chunks;
	Chunk CurrentChunk;

	public int FirstChunkStartDistance;
	public int DistanceBetweenChunks = 20;


	private void Awake()
	{
		instance = this;
	}

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

	public void NextChunk()
	{
		var nextChunk = chunks[Random.Range(0, chunks.Length)];

		var newGo = Object.Instantiate(nextChunk).gameObject;
		newGo.transform.position = new Vector3(0, -50, 0);
		if (CurrentChunk == null)
			newGo.transform.Translate(FirstChunkStartDistance, 0, 0);
		else
		{
			var xOffset = CurrentChunk.transform.position.x + DistanceBetweenChunks + CurrentChunk.Width;
			var offset = new Vector3(xOffset, 0, 0);
			newGo.transform.Translate(offset);
		}

		newGo.AddComponent<ChunkMover>();

		newGo.AddComponent<Rigidbody2D>();
		var box = newGo.AddComponent<BoxCollider2D>();
		box.offset = new Vector2(nextChunk.Width / 2, 50);
		box.size = new Vector2(nextChunk.Width, 100);
		newGo.layer = LayerMask.NameToLayer("Chunk");

		CurrentChunk = newGo.GetComponent<Chunk>();
	}
}
