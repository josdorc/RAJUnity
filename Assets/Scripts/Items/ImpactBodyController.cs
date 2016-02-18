using UnityEngine;
using System.Collections;


public class ImpactBodyController : MonoBehaviour {

	public float TimeLife = 0.5f;
	public GameObject ImpactPrefab;
	public float ImpactTime;
	public string[] IgnoreTags;
	public bool DestroyOnImpact;

	// Use this for initialization
	void Start () {
		DestroyObject (this.gameObject, TimeLife);
	}
	

	void OnTriggerEnter2D(Collider2D hit)
	{
		if(Utils.ExistStringInArray(IgnoreTags, hit.gameObject.tag))
		{
			if(ImpactPrefab != null)
				GameObject.Instantiate(ImpactPrefab, this.transform.position, Quaternion.identity);

			if(DestroyOnImpact)
				DestroyObject(this.gameObject);
		}
	}
}
