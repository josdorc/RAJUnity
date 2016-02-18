using UnityEngine;
using System.Collections;

public class ContactDamageController : MonoBehaviour {

	public int Damage;
	public bool EnableDamage = true;
	public string[] TargetTags;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D hit)
	{
		if(Utils.ExistStringInArray(TargetTags, hit.gameObject.tag) && EnableDamage)
			hit.gameObject.GetComponent<HealthController>().SetDamage(Damage, this.tag);
	}
}





