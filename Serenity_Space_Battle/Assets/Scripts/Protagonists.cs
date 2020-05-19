using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagonists : MonoBehaviour
{
	public Vector3 force = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
	public Vector3 velocity = Vector3.zero;
	public float mass = 1;
	public float maxSpeed = 20.0f;

	public GameObject fleeTarget;
	public GameObject safeZone;
	public float fleeDistance = 10;
	public bool FleeEnabled = false;
	public bool SeekEnabled = false;

	[Range(0.0f, 1.0f)]
	public float banking = 0.1f;

	public float damping = 0.01f;

	public Vector3 Seek(Vector3 target)
	{
		Vector3 desired = target - transform.position;
		desired.Normalize();
		desired *= maxSpeed;
		return desired - velocity;
	}

	public Vector3 Flee(Vector3 target)
	{
		Vector3 desired = target - transform.position;
		desired.Normalize();
		desired *= maxSpeed;

		return velocity - desired;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "BadGuy")
		{
			SeekEnabled = false;
			FleeEnabled = true;
			fleeTarget = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "BadGuy")
		{
			if (other.gameObject == fleeTarget)
			{
				//gameObject.GetComponent<Protagonists>().fleeTarget = null;
				FleeEnabled = false;
				SeekEnabled = true;
			}
		}
	}

	public Vector3 CalculateForce()
	{
		Vector3 force = Vector3.zero;

		if (SeekEnabled)
		{
			force += Seek(safeZone.transform.position);
		}

		if (FleeEnabled)
		{
			force += Flee(fleeTarget.transform.position);
		}

		return force;
	}

	public void Update()
	{
		force = CalculateForce();
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
