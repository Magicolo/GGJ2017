using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class ShaderScript : MonoBehaviour
{
	public Material ObstacleMat;
	public Material WaveMat;
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

		if (ObstacleMat == null)
			return;
		v += RasterSpeed * Time.deltaTime;
		v %= 1;
		ObstacleMat.SetFloat("_Raster", v);

		//Line
		if (player == null)
			return;

		var pp = LevelManager.Instance.MainCamera.WorldToViewportPoint(player.transform.position);
		ObstacleMat.SetVector("_PlayerP", pp);

		WaveMat.SetFloat("_PlayerX", pp.x);
		var cm = ChunkManager.instance;

		int d = (cm.MaxDifficulty - cm.CurrentDifficulty) / cm.MaxDifficulty;
		WaveMat.SetFloat("_Length", d);
	}
}

