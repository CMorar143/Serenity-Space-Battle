using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{
	public GameObject[] antagonists;

	// Start is called before the first frame update
	void Start()
	{
		antagonists = GameObject.FindGameObjectsWithTag("BadGuy");

		foreach (GameObject badGuy in antagonists)
		{
			badGuy.AddComponent<OffsetPursue>().badMain = this.gameObject;
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
