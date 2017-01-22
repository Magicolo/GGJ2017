using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ChunkLevel : MonoBehaviour
{
	public ChunkInfo[] Level;
	public int Difficulty;
}

[Serializable]
public class ChunkInfo
{
	public Chunk chunk;
	public float weight;
}
