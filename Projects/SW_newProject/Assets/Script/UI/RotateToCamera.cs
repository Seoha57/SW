using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour {
  //Privates
  Camera p_Camera;
  Vector3 p_Direction;


	// Use this for initialization
	void Start () {
    p_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
    p_Direction = p_Camera.transform.position - transform.position;
    p_Direction = p_Direction.normalized;

    float angle = Mathf.Atan2(p_Direction.x, p_Direction.z);
    angle *= Mathf.Rad2Deg;

    transform.eulerAngles = new Vector3(0, angle, 0);

	}
}
