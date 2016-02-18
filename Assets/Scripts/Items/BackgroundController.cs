using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	public Transform NearTransform;
	public Transform FarTransform;
	public GameObject NearPrefab;
	public GameObject FarPrefab;
    public bool IsVertical;
    public bool Mirror;
    public int NearNum;
	public int FarNum;

	// Use this for initialization
	void Start () 
	{
        Vector3 dir = IsVertical ? Vector3.up : Vector3.right;

        float size = 0;
		for(int i=0; i < NearNum; i++)
		{
            size = NearPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
			GameObject near = GameObject.Instantiate(NearPrefab, NearTransform.position + dir * i * size, Quaternion.identity) as GameObject;
			near.transform.parent = NearTransform.transform;

            if (Mirror && i % 2 == 0)
                near.transform.localScale = IsVertical ? new Vector3(1, -1, 1) : new Vector3(-1, 1, 1);
		}

        for (int i = 0; i < FarNum; i++)
        {
            size = FarPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
            GameObject far = GameObject.Instantiate(FarPrefab, FarTransform.position + dir * i * size, Quaternion.identity) as GameObject;
            far.transform.parent = FarTransform.transform;

            if (Mirror && i % 2 == 0)
                far.transform.localScale = IsVertical ? new Vector3(1, -1, 1) : new Vector3(-1, 1, 1);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
