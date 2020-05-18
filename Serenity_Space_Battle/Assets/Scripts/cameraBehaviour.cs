using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehaviour : MonoBehaviour
{
	private GameObject cam;
	private Vector3 startingPos;
	private Quaternion startingRot;
	public GameObject mothership;
	private AudioSource clip;
	public float rotSpeed = 35.0f;
	public float speed = 1.0f;

	// For keeping track of coroutine
	private int counter = 0;
	//private int maxCounter = 3;
	private bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
		//maxCounter = GetComponents<AudioSource>().Length;
		//Debug.Log(maxCounter);
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		startingPos = cam.transform.position;
		startingRot = cam.transform.rotation;
		clip = GetComponent<AudioSource>();
	}

	IEnumerator LerpCamera(Vector3 source, Vector3 target, Quaternion rotDirection, float overTime)
	{
		isRunning = true;

		// Everyone in pos, something coming through the cloud
		if (!clip.isPlaying)
		{
			clip.Play();
		}

		yield return new WaitForSecondsRealtime(2f);
		float startTime = Time.time;

		while (Time.time < startTime + overTime)
		{
			cam.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
			cam.transform.rotation = Quaternion.RotateTowards
				(
					cam.transform.rotation,
					rotDirection,
					rotSpeed * Time.deltaTime
				);
			yield return null;
		}
		cam.transform.position = target;

		// AUDIO: he's mad
		// CAMERA: pans back to startPos
		// AUDIO: he's not even changing course

		counter++;

		if (counter == 2)
		{
			Debug.Log("counter is 2");
			Protagonists c = FindObjectOfType<Protagonists>();
			c.arriveEnabled = true;
		}

		isRunning = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Mothership")
		{
			Vector3 target = transform.position + new Vector3(0, 10);
			Quaternion rotDirection = transform.rotation;
			StartCoroutine(LerpCamera(cam.transform.position, target, rotDirection, 3.0f));
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Mothership")
		{
			if (!isRunning)
			{
				if (counter <= 1)
				{
					Debug.Log("its not running in trig stay");
					StartCoroutine(LerpCamera(cam.transform.position, startingPos, startingRot, 3.0f));
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
