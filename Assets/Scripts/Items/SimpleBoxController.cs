using UnityEngine;
using System.Collections;

public class SimpleBoxController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, Vector2.down, this.GetComponent<BoxCollider2D>().size.y);

        for(int i=0; i < hits.Length; i++)
        {
            if (hits[i].transform.tag == "Enemy")
                hits[i].transform.GetComponent<HealthController>().SetDamage(int.MaxValue, "Player");
        }
	}
}
