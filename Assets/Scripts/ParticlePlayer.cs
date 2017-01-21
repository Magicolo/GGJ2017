using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
	public float Speed = 5f;
	public Wave Wave;
	public Rigidbody2D Body;

	void FixedUpdate()
	{
		UpdateMotion(Input.GetAxis("Horizontal"), Time.fixedDeltaTime);
	}

	void UpdateMotion(float input, float deltaTime)
	{
		var motion = new Vector2(input * Speed, 0f) * deltaTime;
		var position = Camera.main.WorldToViewportPoint(Body.position + motion);
		position.x = Mathf.Clamp01(position.x);
		position.y = Wave.Solve(position.x);
		position.z = -Camera.main.transform.position.z;
		Body.MovePosition(Camera.main.ViewportToWorldPoint(position));

		Debug.Log(motion + " | " + position + " | " + Body.position);
	}
}
