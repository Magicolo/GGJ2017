using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLimit : MonoBehaviour
{
	void OnTriggerExit2D(Collider2D collision)
	{
		ChunkManager.instance.NextChunk();
	}
}
