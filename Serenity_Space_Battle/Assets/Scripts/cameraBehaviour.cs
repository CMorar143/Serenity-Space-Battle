using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehaviour : MonoBehaviour
{
	private GameObject cam;
	public float rotSpeed = 20.0f;
	public float speed = 1.0f;
	private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		journeyLength = Vector3.Distance(cam.transform.position, this.transform.position);
    }

	IEnumerator LerpCamera(Vector3 source, Vector3 target, float overTime)
	{
		yield return new WaitForSecondsRealtime(5f);
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

	//private void LerpCamera()
	//{
		
	//}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Mothership")
		{
			StartCoroutine(LerpCamera(cam.transform.position, this.transform.position, 3.0f));
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
