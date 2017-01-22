using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
	public float MoveSpeed = 10f;
	public SpriteRenderer Renderer;
	public Rigidbody2D Body;
	public ParticleSystem DeathEffect;

	[Header("Tunnel Settings")]
	public float TunnelCooldown = 5f;
	public Transform TunnelLeft;
	public Transform TunnelRight;
	public ParticleSystem TunnelStartEffect;
	public ParticleSystem TunnelEndEffect;

	public float CooldownRatio
	{
		get { return Mathf.Clamp01(1f - tunnelCounter / TunnelCooldown); }
	}
	public bool CanTunnel
	{
		get { return tunnelCounter <= 0f; }
	}

	float tunnelCounter;

	void FixedUpdate()
	{
		UpdateInput(new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1")).normalized, Time.fixedDeltaTime);
	}

	void UpdateInput(Vector2 input, float deltaTime)
	{
		var wave = LevelManager.Instance.Wave;
		var motion = new Vector3((input.x * MoveSpeed) / Mathf.Pow(wave.Frequency * wave.Amplitude, 0.5f), 0f) * deltaTime;
		var position = LevelManager.Instance.MainCamera.WorldToViewportPoint(transform.position + motion);

		position.x = Mathf.Clamp(position.x, LevelManager.Instance.Bounds.xMin, LevelManager.Instance.Bounds.xMax);
		position.y = wave.Solve(position.x);
		position.z = -LevelManager.Instance.MainCamera.transform.position.z;
		Body.MovePosition(LevelManager.Instance.MainCamera.ViewportToWorldPoint(position));

		UpdateTunnel(input, deltaTime);
	}

	void UpdateTunnel(Vector2 input, float deltaTime)
	{
		tunnelCounter -= deltaTime;

		if (CanTunnel)
		{
			var wave = LevelManager.Instance.Wave;
			var position = LevelManager.Instance.MainCamera.WorldToViewportPoint(Body.position);
			var left = new Vector3(position.x - 1f / wave.Frequency, position.y, -LevelManager.Instance.MainCamera.transform.position.z);
			var right = new Vector3(position.x + 1f / wave.Frequency, position.y, -LevelManager.Instance.MainCamera.transform.position.z);

			TunnelLeft.position = LevelManager.Instance.MainCamera.ViewportToWorldPoint(left);
			TunnelRight.position = LevelManager.Instance.MainCamera.ViewportToWorldPoint(right);

			if ((input.y <= -0.5f && left.x >= LevelManager.Instance.Bounds.xMin) || (input.y >= 0.5f && right.x <= LevelManager.Instance.Bounds.xMax))
			{
				var target = input.y < 0f ? TunnelLeft.position : TunnelRight.position;
				TunnelStartEffect.transform.parent = null;
				TunnelStartEffect.transform.position = transform.position;
				TunnelStartEffect.Play();
				TunnelEndEffect.transform.parent = null;
				TunnelEndEffect.transform.position = target;
				TunnelEndEffect.Play();
				transform.position = target;
				tunnelCounter = TunnelCooldown;
			}
		}

		TunnelLeft.gameObject.SetActive(CanTunnel);
		TunnelRight.gameObject.SetActive(CanTunnel);
	}

	void OnTriggerEnter2D()
	{
		DeathEffect.transform.parent = null;
		DeathEffect.Play();
		LevelManager.Instance.Lose();
		Destroy(gameObject);
	}
}
