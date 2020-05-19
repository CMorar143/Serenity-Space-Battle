using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBehaviour : MonoBehaviour
{
	public GameObject[] protagonists;
	public GameObject[] antagonists;
	private bool standoffOccurred = false;

	// Start is called before the first frame update
	void Start()
    {
		protagonists = GameObject.FindGameObjectsWithTag("GoodGuy");
		antagonists = GameObject.FindGameObjectsWithTag("BadGuy");
		StartCoroutine(CheckBattle());
    }

	IEnumerator CheckBattle()
	{
		while (true)
		{
			standoffOccurred =
				GameObject.FindGameObjectWithTag("GoodStartingPos").
				GetComponent<cameraBehaviour>().standoffOccurred;

			if (standoffOccurred)
			{
				Debug.Log("in check battle");
			}

			yield return new WaitForSeconds(2f);
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
