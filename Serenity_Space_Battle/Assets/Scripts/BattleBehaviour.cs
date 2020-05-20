using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBehaviour : MonoBehaviour
{
	public GameObject[] protagonists;
	public GameObject[] antagonists;
	public AudioSource[] clips;
	private bool standoffOccurred = false;
	private bool continueCoroutine = true;
	public Camera mainCam;
	public Camera serenityCam;
	public Camera finalCam;
	public int counter = 0;
	private int audioCounter = 0;

	// Start is called before the first frame update
	void Start()
    {
		protagonists = GameObject.FindGameObjectsWithTag("GoodGuy");
		antagonists = GameObject.FindGameObjectsWithTag("BadGuy");
		clips = GetComponents<AudioSource>();
		StartCoroutine(CheckBattle());
	}

	IEnumerator SwitchCamera()
	{
		while (true && counter <= 3)
		{
			yield return new WaitForSeconds(4f);
			mainCam.enabled = !mainCam.enabled;
			serenityCam.enabled = !serenityCam.enabled;
			counter++;

			if (counter < 3 && audioCounter < clips.Length)
			{
				clips[audioCounter].Play();
				audioCounter++;
			}
		}

		if (audioCounter < clips.Length)
		{
			clips[audioCounter].Play();
			audioCounter++;
		}

		mainCam.enabled = false;
		serenityCam.enabled = false;
		finalCam.enabled = true;

		// Stop the shooting
		foreach (GameObject badGuy in antagonists)
		{
			badGuy.GetComponent<Pursue>().continueShooting = false;
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
				clips[audioCounter].Play();
				audioCounter++;

				for (int i = 0; i < antagonists.Length; i++)
				{
					antagonists[i].GetComponent<Mothership>().enabled = false;
					antagonists[i].GetComponent<Pursue>().enabled = true;
					antagonists[i].GetComponent<Pursue>().target = protagonists[Random.Range(1,3)];
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
		finalCam.transform.LookAt(serenityCam.transform);
    }
}
