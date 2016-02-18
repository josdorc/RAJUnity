using UnityEngine;
using System.Collections;

public class DeathZoneController : MonoBehaviour {
    public GameObject DestroyPrefab;
    protected bool m_IsDestroyed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (!m_IsDestroyed && DestroyPrefab != null)
        {
            GameObject.Instantiate(DestroyPrefab, hit.transform.position, Quaternion.identity);
            m_IsDestroyed = true;
        }

        if (hit.gameObject.tag == "Player")
            hit.gameObject.GetComponent<HealthController>().CurrentHealth = 0;
        else
            DestroyObject(hit.gameObject);            
    }
}
