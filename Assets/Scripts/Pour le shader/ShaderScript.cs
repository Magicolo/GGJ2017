using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class ShaderScript : MonoBehaviour
{
	public Material Material;

	public float RasterSpeed = 10;
	float v = 0;

	private void Update()
	{
		if (Material == null)
			return;
		v += RasterSpeed * Time.deltaTime;
		v %= 1;
		Material.SetFloat("_Raster", v);
	}
}

