using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class type_Transform : MonoBehaviour {
    public GameObject partA;
    public GameObject partB;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            partA.transform.RotateAround(partB.transform.position+new Vector3(0.5f,0,0), Vector3.up, 90);
        }
    }
}
