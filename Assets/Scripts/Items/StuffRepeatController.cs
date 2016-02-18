using UnityEngine;
using System.Collections;

public class StuffRepeatController : MonoBehaviour {
    public GameObject Stuff;
    public float Num;
    public Vector3 Distance;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < Num; i++)
        {
            GameObject torch = GameObject.Instantiate(Stuff, this.transform.position + i * Distance, Quaternion.identity) as GameObject;
            torch.transform.parent = this.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
