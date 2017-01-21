using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zTest : MonoBehaviour
{
	//int frame

	private void Update()
	{
		string thing = "";
		for (int i = 1; i <= 8; i++)
		{
			thing += Input.GetAxis("Horizontal" + i) + " - ";
		}
		Debug.Log(thing);

	}
}
