using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBehaviour : MonoBehaviour
{
	public GameObject[] protagonists;
	public GameObject[] antagonists;
	private bool standoffOccurred = false;
	private bool continueCoroutine = true;
	public Camera mainCam;
	public Camera serenityCam;
	private int counter = 0;

	// Start is called before the first frame update
	void Start()
    {
		protagonists = GameObject.FindGameObjectsWithTag("GoodGuy");
		antagonists = GameObject.FindGameObjectsWithTag("BadGuy");
		StartCoroutine(CheckBattle());
    }

	IEnumerator SwitchCamera()
	{
		while (true && counter <= 4)
		{
			yield return new WaitForSeconds(4f);
			mainCam.enabled = !mainCam.enabled;
			serenityCam.enabled = !serenityCam.enabled;
			counter++;
		}
	}

	IEnumerator CheckBattle()
	{
		while (continueCoroutine)
		{
			standoffOccurred =
				GameObject.FindGameObjectWithTag("GoodStartingPos").
				GetComponent<cameraBehaviour>().standoffOccurred;

			if (standoffOccurred)
			{
				for (int i = 0; i < antagonists.Length; i++)
				{
					antagonists[i].AddComponent<Pursue>().target = protagonists[Random.Range(1,3)];
					antagonists[i].GetComponent<Pursue>().pursueEnabled = true;
				}

				continueCoroutine = false;
				StartCoroutine(SwitchCamera());
			}

			yield return new WaitForSeconds(2f);
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
