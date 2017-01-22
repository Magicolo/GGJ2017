using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class MenuShader : MonoBehaviour
{
	public Material Material;
	public float Lenght;

	private void Update()
	{
		float x = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
		Material.SetFloat("_MouseX", x);

		Material.SetFloat("_Length", Lenght);
	}
}


