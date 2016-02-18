using UnityEngine;
using System.Collections;

public class AutoRotateController : MonoBehaviour {

    public float AngularSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.Rotate(Vector3.forward, AngularSpeed * Time.deltaTime);
	}
}
