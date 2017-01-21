using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
	public float MoveSpeed = 10f;
	public Wave Wave;
	public Rigidbody2D Body;
	public ParticleSystem DeathParticles;

	void FixedUpdate()
	{
		UpdateInput(Input.GetAxis("Horizontal1"), Time.fixedDeltaTime);
	}

	void UpdateInput(float input, float deltaTime)
	{
		var motion = new Vector2(input * MoveSpeed, 0f) * deltaTime;
		var position = Camera.main.WorldToViewportPoint(Body.position + motion);
		position.x = Mathf.Clamp(position.x, LevelManager.Instance.PlayerBounds.x, LevelManager.Instance.PlayerBounds.y);
		position.y = Wave.Solve(position.x);
		position.z = -Camera.main.transform.position.z;
		Body.MovePosition(Camera.main.ViewportToWorldPoint(position));
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		DeathParticles.Play();
	}
}
