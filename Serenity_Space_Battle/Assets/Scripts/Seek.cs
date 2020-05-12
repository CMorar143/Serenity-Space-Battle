using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Seek : SteeringBehaviour
{
	public GameObject targetGameObject = null;
	public Vector3 target = Vector3.zero;

	public override Vector3 CalculateForces()
	{
		return leader.Seek(target);
	}

	public void Update()
	{
		if (targetGameObject != null)
		{
			target = targetGameObject.transform.position;
		}
	}
}