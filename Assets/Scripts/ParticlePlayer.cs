using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
	public float MoveSpeed = 10f;
	public float OffsetSpeed = 3f;
	public SpriteRenderer Renderer;
	public Rigidbody2D Body;
	public ParticleSystem DeathParticles;

	void FixedUpdate()
	{
		UpdateInput(new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1")), Time.fixedDeltaTime);
	}

	void UpdateInput(Vector2 input, float deltaTime)
	{
		var wave = LevelManager.Instance.Wave;
		var motion = new Vector2(input.x * MoveSpeed, 0f) * deltaTime;
		var position = LevelManager.Instance.MainCamera.WorldToViewportPoint(Body.position + motion);
		position.x = Mathf.Clamp(position.x, LevelManager.Instance.PlayerBounds.x, LevelManager.Instance.PlayerBounds.y);
		position.y = wave.Solve(position.x);
		position.z = -LevelManager.Instance.MainCamera.transform.position.z;
		Body.MovePosition(LevelManager.Instance.MainCamera.ViewportToWorldPoint(position));

		wave.Offset = wave.Offset + input.y * OffsetSpeed * deltaTime;
		wave.Offset %= Mathf.PI * 2f;
	}

	void OnCollisionEnter2D()
	{
		DeathParticles.transform.parent = null;
		DeathParticles.Play();
		LevelManager.Instance.Lose();
		Destroy(gameObject);
	}
}
