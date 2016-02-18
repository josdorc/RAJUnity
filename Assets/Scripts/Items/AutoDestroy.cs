using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public float Time;

	// Use this for initialization
	void Start () {
		DestroyObject (this.gameObject, Time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
