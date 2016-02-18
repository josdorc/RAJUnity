using UnityEngine;
using System.Collections;

public class BoxDestroyEffect : MonoBehaviour {

	public int NumItems;
	public float LifeTime;
	public float MinForce;
	public float MaxForce;
	public GameObject DestroyBrick;
	public GameObject DestroyEffect;

	// Use this for initialization
	void Start () {
		for(int i=0; i < NumItems; i++)
		{
			float x = Random.Range(MinForce, MaxForce);
			float y = Random.Range(MinForce, MaxForce);
			float r = Random.Range(0, 180);
			GameObject brick = GameObject.Instantiate(DestroyBrick, this.transform.position, Quaternion.Euler(0,0, r)) as GameObject;
			brick.transform.localScale = new Vector3(Random.Range(1,3),1,1);
			brick.AddComponent<Rigidbody2D>().AddForce(new Vector2(x,y), ForceMode2D.Impulse);
			DestroyObject(brick, LifeTime);
			
			GameObject.Instantiate(DestroyEffect, this.transform.position, Quaternion.identity);

			DestroyObject(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {	
	}
}
