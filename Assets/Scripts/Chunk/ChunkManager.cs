using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
	public static ChunkManager instance;

	ChunkLevel level;
	Chunk CurrentChunk;

	public int FirstChunkStartDistance;
	public int DistanceBetweenChunks = 20;

	public float SpeedModifier = 1;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start()
	{
		level = Object.FindObjectOfType<ChunkLevel>();
		if (level == null)
			Debug.LogError("Tu as oublier de mettre un chunkLevel, please!");
		NextChunk();
	}

	// Update is called once per frame
	void Update()
	{

	}

	Chunk GetNextChunk()
	{
		float totalWeight = 0;
		foreach (var c in level.Level)
		{
			totalWeight += c.weight;
		}

		float neededWeight = Random.Range(0, totalWeight);
		float weightcumul = 0;
		foreach (var c in level.Level)
		{
			weightcumul += c.weight;
			if (weightcumul >= neededWeight)
				return c.chunk;
		}

		return null;
	}

	public void NextChunk()
	{
		var nextChunk = GetNextChunk();

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
