using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{
	public GameObject[] antagonists;
	public GameObject startingPos;
	public Vector3 target;
	public Vector3 force = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
	public Vector3 velocity = Vector3.zero;
	public float mass = 1;
	public float maxSpeed = 10.0f;
	public float slowingDistance = 10;
	public float damping = 0.01f;

	[Range(0.0f, 1.0f)]
	public float banking = 0.1f;

	public bool SeekEnabled = false;
	public bool ArriveEnabled = false;


	// Start is called before the first frame update
	void Start()
	{
		antagonists = GameObject.FindGameObjectsWithTag("BadGuy");

		foreach (GameObject badGuy in antagonists)
		{
			badGuy.AddComponent<OffsetPursue>().badMain = this.gameObject;
		}
	}

	public Vector3 Seek(Vector3 target)
	{
		Vector3 desired = target - transform.position;
		desired.Normalize();
		desired *= maxSpeed;
		return desired - velocity;
	}

	public Vector3 Arrive(Vector3 target)
	{
		Vector3 toTarget = target - transform.position;
		float dist = toTarget.magnitude;
		Vector3 desired;

		if (dist < 7)
		{
			ArriveEnabled = false;
			desired = Vector3.zero;
			damping = 2.0f;
		}

		else
		{
			float ramped = (dist / slowingDistance) * maxSpeed;
			float clamped = Mathf.Min(ramped, maxSpeed);
			desired = clamped * (toTarget / dist);
		}
		
		return desired - velocity;
	}

	Vector3 CalculateForces()
	{
		Vector3 force = Vector3.zero;
		if (startingPos != null)
		{
			target = startingPos.transform.position;
		}

		force = Vector3.zero;
		if (SeekEnabled)
		{
			force += Seek(target);
		}

		if (ArriveEnabled)
		{
			force += Arrive(target);
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
