using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLimit : MonoBehaviour
{
	void OnTriggerExit2D(Collider2D collision)
	{
		var chunk = collision.gameObject.GetComponent<Chunk>();
		if (chunk == null) return;

		Destroy(collision.gameObject);
	}
}
