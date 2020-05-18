﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagonists : MonoBehaviour
{
	public Vector3 force = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
	public Vector3 velocity = Vector3.zero;
	public float mass = 1;
	public float maxSpeed = 20.0f;
	public Transform target;
	public bool arriveEnabled = false;

	[Range(0.0f, 1.0f)]
	public float banking = 0.1f;

	public float damping = 0.01f;


	// Use this for initialization
	void Start()
	{

	}

	public Vector3 Seek(Vector3 target)
	{
		Vector3 desired = target - transform.position;
		desired.Normalize();
		desired *= maxSpeed;
		return desired - velocity;
	}

	public Vector3 Arrive(Vector3 target, float slowingDistance = 15.0f, float deceleration = 1.0f)
	{
		Vector3 toTarget = target - transform.position;

		float distance = toTarget.magnitude;
		if (distance < 7)
		{
			arriveEnabled = false;
			damping = 2.0f;

			return Vector3.zero;
		}
		float ramped = maxSpeed * (distance / (slowingDistance * deceleration));

		float clamped = Mathf.Min(ramped, maxSpeed);
		Vector3 desired = clamped * (toTarget / distance);

		return desired - velocity;
	}


	Vector3 CalculateForces()
	{
		force = Vector3.zero;

		if (arriveEnabled)
		{
			force += Arrive(target.position);
		}

		return force;
	}

	// Update is called once per frame
	void Update()
	{
		force = CalculateForces();
		Vector3 acceleration = force / mass;

		velocity += acceleration * Time.deltaTime;
		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

		if (velocity.magnitude > float.Epsilon)
		{
			Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
			transform.LookAt(transform.position + velocity, tempUp);
			transform.position += velocity * Time.deltaTime;
			velocity *= (1.0f - (damping * Time.deltaTime));
		}
	}
}
