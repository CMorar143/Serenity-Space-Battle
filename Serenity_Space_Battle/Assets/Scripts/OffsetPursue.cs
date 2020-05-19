using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursue : SteeringBehaviour
{
	public Mothership badMain;
	public Vector3 offset;
	public Vector3 worldtarget;

	public GameObject startingPos;

	// Use this for initialization
	void Start()
	{
		offset = transform.position - badMain.transform.position;
		offset = Quaternion.Inverse(badMain.transform.rotation) * offset;
		startingPos = GameObject.FindGameObjectWithTag("GoodStartingPos");
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
		targetPos = leader.Arrive(targetPos);

		if (gameObject.tag == "GoodGuy")
		{
			if (targetPos.z <= 3 && startingPos.GetComponent<cameraBehaviour>().standoffOccurred)
			{
				gameObject.GetComponent<OffsetPursue>().enabled = false;
				Debug.Log("STANDOFF");
				return Vector3.zero;
			}
		}

		// Prevent movement in any other direction
		return new Vector3(0, 0, targetPos.z);
	}
}
