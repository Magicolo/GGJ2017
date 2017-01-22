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
	public AudioSource MotorSound;
	public AudioSource DeathSound;
	public AudioSource TunnelSound;

	public float CooldownRatio
	{
		get { return Mathf.Clamp01(1f - tunnelCounter / TunnelCooldown); }
	}
	public bool CanTunnel
	{
		get { return tunnelCounter <= 0f; }
	}

	Vector3 velocity;
	float tunnelCounter;

	void FixedUpdate()
	{
		transform.Rotate(0f, 0f, Time.fixedDeltaTime * -500f * LevelManager.Instance.Speed);
		UpdateInput(new Vector2(Input.GetAxisRaw("Horizontal1"), Input.GetAxisRaw("Vertical1")), Time.fixedDeltaTime);
		UpdateAudio();
	}

	void UpdateInput(Vector2 input, float deltaTime)
	{
		var wave = LevelManager.Instance.Wave;
		float speed = (input.x * MoveSpeed) / Mathf.Pow(wave.Frequency * Mathf.Abs(wave.Amplitude), 0.5f);

		if (speed < 0f)
			speed = Mathf.Max(speed, -MoveSpeed * 3f);
		else
			speed = Mathf.Min(speed, MoveSpeed * 3f);

		var motion = new Vector3(speed, 0f) * deltaTime;
		var position = LevelManager.Instance.MainCamera.WorldToViewportPoint(transform.position + motion);

		position.x = Mathf.Clamp(position.x, LevelManager.Instance.Bounds.xMin, LevelManager.Instance.Bounds.xMax);
		position.y = wave.Solve(position.x);
		position.z = -LevelManager.Instance.MainCamera.transform.position.z;
		var target = LevelManager.Instance.MainCamera.ViewportToWorldPoint(position);
		velocity = target - (Vector3)Body.position;
		Body.MovePosition(target);

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

			bool tunnelLeft = Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.Q);
			bool tunnelRight = Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.E);
			if ((tunnelLeft && left.x >= LevelManager.Instance.Bounds.xMin) || (tunnelRight && right.x <= LevelManager.Instance.Bounds.xMax))
			{
				var target = tunnelLeft ? TunnelLeft.position : TunnelRight.position;
				TunnelStartEffect.transform.parent = null;
				TunnelStartEffect.transform.position = transform.position;
				TunnelStartEffect.Play();
				TunnelEndEffect.transform.parent = null;
				TunnelEndEffect.transform.position = target;
				TunnelEndEffect.Play();
				transform.position = target;
				tunnelCounter = TunnelCooldown;
				TunnelSound.Play();
			}
		}

		TunnelLeft.gameObject.SetActive(CanTunnel);
		TunnelRight.gameObject.SetActive(CanTunnel);
	}

	void UpdateAudio()
	{
		MotorSound.pitch = Mathf.Lerp(MotorSound.pitch, Mathf.Pow(velocity.magnitude, 0.5f), Time.deltaTime);
	}

	void OnTriggerEnter2D()
	{
		DeathEffect.transform.parent = null;
		DeathEffect.Play();
		DeathSound.transform.parent = null;
		DeathSound.Play();
		LevelManager.Instance.Lose();
		Destroy(gameObject);
	}
}
