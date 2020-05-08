using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursue : MonoBehaviour
{
	public GameObject badMain;
	private Vector3 offset;
	private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
		offset = transform.position - badMain.transform.position;
		offset = Quaternion.Inverse(badMain.transform.rotation) * offset;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
