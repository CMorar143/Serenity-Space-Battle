using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
	public float weight = 1.0f;
	public Vector3 force;

	[HideInInspector]
	public Mothership leader;
	public Protagonists protagonists;

	public void Awake()
	{
		leader = GetComponent<Mothership>();
	}

	public abstract Vector3 CalculateForces();
}
