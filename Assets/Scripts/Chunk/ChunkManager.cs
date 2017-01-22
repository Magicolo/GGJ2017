using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChunkManager : MonoBehaviour
{
	public static ChunkManager instance;

	ChunkLevel[] levels;
	Chunk CurrentChunk;

	public int FirstChunkStartDistance;
	public int DistanceBetweenChunks = 20;

	public float SpeedModifier = 1;

	bool justDidAHard;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start()
	{
		levels = UnityEngine.Object.FindObjectsOfType<ChunkLevel>();
		Array.Sort(levels, (a, b) => a.Difficulty.CompareTo(b.Difficulty));
		if (levels.Length == 0)
			Debug.LogError("Tu as oublier de mettre un chunkLevel, please!");
		NextChunk();
	}

	// Update is called once per frame
	void Update()
	{

	}

	Chunk GetNextChunk()
	{
		var difficulty = (int)Mathf.Min(LevelManager.Instance.Difficulty, levels.Length) - 1;

		var currentLevel = levels[difficulty];
		float totalWeight = 0;
		foreach (var c in currentLevel.Level)
		{
			totalWeight += c.weight;
		}

		int infinitblockerthing = 22;
		while (infinitblockerthing-- > 0)
		{
			float neededWeight = UnityEngine.Random.Range(0, totalWeight);
			float weightcumul = 0;
			foreach (var c in currentLevel.Level)
			{
				weightcumul += c.weight;

				if (weightcumul >= neededWeight && !(justDidAHard && c.chunk.IsHard))
				{
					justDidAHard = c.chunk.IsHard;

					return c.chunk;
				}

			}
		}

		Debug.LogError("Didnt found a chunk");
		return null;
	}

	public void NextChunk()
	{
		var nextChunk = GetNextChunk();

		var newGo = UnityEngine.Object.Instantiate(nextChunk).gameObject;
		newGo.transform.position = new Vector3(0, -50, 0);
		if (CurrentChunk == null)
			newGo.transform.Translate(FirstChunkStartDistance, 0, 0);
		else
		{
			var xOffset = CurrentChunk.transform.position.x + DistanceBetweenChunks + CurrentChunk.Width;
			var offset = new Vector3(xOffset, 0, 0);
			newGo.transform.Translate(offset);
		}

		var mover = newGo.AddComponent<ChunkMover>();


		newGo.AddComponent<Rigidbody2D>();
		var box = newGo.AddComponent<BoxCollider2D>();
		box.offset = new Vector2(nextChunk.Width / 2, 50);
		box.size = new Vector2(nextChunk.Width, 100);
		newGo.layer = LayerMask.NameToLayer("Chunk");

		CurrentChunk = newGo.GetComponent<Chunk>();

		var xFlip = 1;
		var yFlip = 1;
		/*if (CurrentChunk.CanFlipInX && UnityEngine.Random.Range(0, 2) < 1)
		{
			xFlip = -1;
			newGo.transform.Translate(CurrentChunk.Width, 0, 0);
		}*/

		if (CurrentChunk.CanFlipInY && UnityEngine.Random.Range(0, 2) < 1)
		{
			yFlip = -1;
			mover.yOffset = 100;
		}

		newGo.transform.localScale = new Vector3(xFlip * transform.localScale.x, yFlip * transform.localScale.y, transform.localScale.z);
		if (yFlip == -1)

			newGo.transform.Translate(0, 100, 0);
	}
}
