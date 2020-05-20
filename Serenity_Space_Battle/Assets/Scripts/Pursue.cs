using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : MonoBehaviour
{
	private AudioSource clip;
	public GameObject laserPrefab;
	public GameObject target;
	Vector3 targetPos;
	public Vector3 force = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
	public Vector3 velocity = Vector3.zero;
	public float mass = 1;
	public float maxSpeed = 10.0f;

	[Range(0.0f, 1.0f)]
	public float banking = 0.1f;

	public float damping = 0.01f;
	public bool pursueEnabled = false;
	public bool continueShooting = true;

	private void OnEnable()
	{
		StartCoroutine(Shooting(Random.Range(1f, 2f)));
		clip = GetComponent<AudioSource>();
	}
	
	IEnumerator Shooting(float interval)
	{
		while (continueShooting)
		{
			yield return new WaitForSeconds(interval);
			//GameObject laser = Instantiate(laserPrefab);
			//laser.GetComponent<Laser>().target = target.transform;
			//laser.GetComponent<Renderer>().material.color = Color.red;
			//laser.transform.LookAt(target.transform);
			clip.Play();
		}
	}

	public Vector3 Seek(Vector3 target)
	{
		Vector3 desired = target - transform.position;
		desired.Normalize();
		desired *= maxSpeed;
		return desired - velocity;
	}

	public Vector3 pursue()
	{
		float dist = Vector3.Distance(target.transform.position, transform.position);
		float time = dist / maxSpeed;

		targetPos = target.transform.position + (target.GetComponent<Mothership>().velocity * time);

		return Seek(targetPos);
	}

	public Vector3 CalculateForce()
	{
		Vector3 force = Vector3.zero;

		if (pursueEnabled)
		{
			force += pursue();
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
