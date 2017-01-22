using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class ShaderScript : MonoBehaviour
{
	public Material Material;
	public Material LineMaterial;
	public static ShaderScript Instance;

	public float RasterSpeed = 10;
	float v = 0;



	ParticlePlayer player;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		player = player == null ? FindObjectOfType<ParticlePlayer>() : player;

		if (Material == null)
			return;
		v += RasterSpeed * Time.deltaTime;
		v %= 1;
		Material.SetFloat("_Raster", v);

		if (player == null)
			return;

		float x = LevelManager.Instance.MainCamera.WorldToViewportPoint(player.transform.position).x;
		LineMaterial.SetFloat("_PlayerX", x);
		var cm = ChunkManager.instance;

		int d = (cm.MaxDifficulty - cm.CurrentDifficulty) / cm.MaxDifficulty * 5;
		LineMaterial.SetFloat("_Length", d);
	}
}

