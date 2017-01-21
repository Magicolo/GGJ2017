using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ChunkLevel : MonoBehaviour
{
	public ChunkInfo[] Level;
}

[Serializable]
public class ChunkInfo
{
	public Chunk chunk;
	public float weight;
}
