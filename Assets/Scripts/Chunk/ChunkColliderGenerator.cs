using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkColliderGenerator : MonoBehaviour
{

	private LineRenderer myRenderer;
	private PolygonCollider2D polyCol;

	// Use this for initialization
	void Start()
	{
		myRenderer = GetComponent<LineRenderer>();
		polyCol = gameObject.AddComponent<PolygonCollider2D>();

		myRenderer.material = ShaderScript.Instance.ObstacleMat;

		Vector2[] paths = new Vector2[myRenderer.numPositions - 1];
		// the line renderer needs a final vertex to close itself, not the poly collider - hence, the -1

		for (int i = 0; i < myRenderer.numPositions - 1; i++)
		{
			paths[i] = myRenderer.GetPosition(i);
		}
		polyCol.SetPath(0, paths);

		//Debug.Log("first point is: " + polyCol.GetPath(0));
	}

	// Update is called once per frame
	void Update()
	{

	}
}
