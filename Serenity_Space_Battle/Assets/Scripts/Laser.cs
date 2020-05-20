using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	public float speed = 50f;
	public Transform target;
	public Vector3 startPos;
	private float startTime;
	private float journeyLength;

	// Use this for initialization
	void Start()
	{
		startTime = Time.time;
		startPos = transform.position;
		journeyLength = Vector3.Distance(transform.position, target.position);
		Invoke("KillMe", 1);
	}

	void KillMe()
	{
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		float distCovered = (Time.time - startTime) * speed;
		float fractionOfJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startPos, target.position, fractionOfJourney);
	}
}
