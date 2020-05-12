using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursue : SteeringBehaviour
{
	public Mothership badMain;
	private Vector3 offset;
	Vector3 worldtarget;

	// Use this for initialization
	void Start()
	{
		offset = transform.position - badMain.transform.position;
		offset = Quaternion.Inverse(badMain.transform.rotation) * offset;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public override Vector3 CalculateForces()
	{
		worldtarget = badMain.transform.TransformPoint(offset);
		float dist = Vector3.Distance(worldtarget, transform.position);
		float time = dist / leader.maxSpeed;

		Vector3 targetPos = worldtarget + badMain.velocity * time;
		targetPos = leader.Arrive(targetPos, 10);
		Debug.Log(worldtarget);
		// Prevent movement in any other direction
		return new Vector3(0, 0, targetPos.z);
	}
}
