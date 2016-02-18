using UnityEngine;
using System.Collections;

public class BombItemController : MonoBehaviour {
    
    public float DetonationTime;
    public float DestroyTime;
    public GameObject ExplosionPrefab;
    protected bool m_IsExploding;
    
	// Use this for initialization
	void Start () {

        Invoke("Explode", DetonationTime);
        DestroyObject(this.gameObject, DestroyTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Explode()
    {
        GameObject Explosion = GameObject.Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity) as GameObject;
        this.GetComponent<SpriteRenderer>().enabled = false;
        m_IsExploding = true;
    }

    void OnTriggerStay2D(Collider2D hit)
    {
        if (m_IsExploding && hit.transform.GetComponent<HealthController>() != null)
        {
            if(hit.transform.tag == "Player")
                hit.transform.GetComponent<HealthController>().Kill("Enemy");
            else
                hit.transform.GetComponent<HealthController>().Kill(this.tag);
        }
    }
}
