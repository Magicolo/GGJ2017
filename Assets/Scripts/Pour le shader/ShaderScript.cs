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

	private void Update()
	{
		if (Material == null)
			return;
		Material.SetFloat("_Raster", Mathf.Sin(RasterSpeed * Time.time));
		Debug.Log(Mathf.Sin(Time.time));
	}
}

