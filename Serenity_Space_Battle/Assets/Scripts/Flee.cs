using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehaviour
{
	public GameObject targetGameObject = null;
	public Vector3 target = Vector3.zero;

	public override Vector3 CalculateForces()
	{
		if (targetGameObject != null)
		{
			return -leader.Seek(target);
		}

		else
		{
			return Vector3.zero;
		}
	}

	public void Update()
	{
		if (targetGameObject != null)
		{
			target = targetGameObject.transform.position;
		}
	}
}
