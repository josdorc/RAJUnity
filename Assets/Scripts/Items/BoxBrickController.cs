using UnityEngine;
using System.Collections;

public class BoxBrickController : MonoBehaviour {

	public GameObject DestroyPrefab;
    public bool Destroy;

	// Use this for initialization
	void Start () {

	}
	
	void OnTriggerEnter2D(Collider2D hit)
	{
		if(hit.gameObject.tag == "Player")
		{	
            for (int i = -1; i <= 1; i++)
            {
                Vector3 pos = this.transform.position + Vector3.right * i * this.GetComponent<BoxCollider2D>().size.x / 3;
             
                RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(pos.x, pos.y), Vector2.up, this.GetComponent<BoxCollider2D>().size.y);
                for (int j = 0; j < hits.Length; j++)
                {
                    if (hits[j].transform.tag == "Enemy")
                        hits[j].transform.GetComponent<HealthController>().Kill("Player");
                }
            }
            
           Explode();
		}
	}

    public void Explode()
    {
        if (Destroy)
        {
            GameObject.Instantiate(DestroyPrefab, this.transform.position, Quaternion.identity);
            DestroyObject(this.gameObject);
        }
    }
}
