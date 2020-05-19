using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBehaviour : MonoBehaviour
{
	public GameObject[] protagonists;
	public GameObject[] antagonists;

	// Start is called before the first frame update
	void Start()
    {
		protagonists = GameObject.FindGameObjectsWithTag("GoodGuy");
		antagonists = GameObject.FindGameObjectsWithTag("BadGuy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
