using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehaviour : MonoBehaviour
{
	private GameObject cam;
	private Vector3 startingPos;
	private AudioSource clip;
	public float rotSpeed = 35.0f;
	public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		startingPos = cam.transform.position;
		clip = GetComponent<AudioSource>();
	}

	IEnumerator LerpCamera(Vector3 source, Vector3 target, float overTime)
	{
		if (!clip.isPlaying)
		{
			clip.Play();
		}

		yield return new WaitForSecondsRealtime(3f);
		float startTime = Time.time;

		while (Time.time < startTime + overTime)
		{
			cam.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
			cam.transform.rotation = Quaternion.RotateTowards
				(
					cam.transform.rotation,
					transform.rotation,
					rotSpeed * Time.deltaTime
				);
			yield return null;
		}
		cam.transform.position = target;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Mothership")
		{
			Vector3 target = transform.position + new Vector3(0, 10);
			StartCoroutine(LerpCamera(cam.transform.position, target, 3.0f));
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
