using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{
	public List<SteeringBehaviour> behaviours = new List<SteeringBehaviour>();

	public GameObject[] antagonists;
	public Vector3 force = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
	public Vector3 velocity = Vector3.zero;
	public float mass = 1;
	public float maxSpeed = 20.0f;

	[Range(0.0f, 1.0f)]
	public float banking = 0.1f;

	public float damping = 0.01f;

	// Use this for initialization
	void Start()
	{
		SteeringBehaviour[] behaviours = GetComponents<SteeringBehaviour>();

		foreach (SteeringBehaviour b in behaviours)
		{
			this.behaviours.Add(b);
		}

		if (gameObject.tag == "Mothership")
		{
			antagonists = GameObject.FindGameObjectsWithTag("BadGuy");
		}

		else if (gameObject.tag == "Serenity")
		{
			antagonists = GameObject.FindGameObjectsWithTag("GoodGuy");
		}

		if (antagonists != null)
		{
			foreach (GameObject badGuy in antagonists)
			{
				badGuy.AddComponent<Mothership>();
				badGuy.AddComponent<OffsetPursue>().badMain = this;
			}
		}
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
		if (distance < 7 && antagonists != null)
		{
			GetComponent<Arrive>().enabled = false;
			damping = 2.0f;

			foreach (GameObject badGuy in antagonists)
			{
				Mothership m = badGuy.GetComponent<Mothership>();
				m.damping = 0.7f;

				badGuy.GetComponent<OffsetPursue>().enabled = false;
			}

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

		foreach (SteeringBehaviour b in behaviours)
		{
			if (b.isActiveAndEnabled)
			{
				force += b.CalculateForces() * b.weight;
			}
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
