using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float LifeTime;
	public GameObject ImpactPrefab;
    public int ImpactDamage;
	public string[] IgnoreTags;
	protected Vector3 m_Speed = Vector3.zero;
    protected float m_AngularSpeed = 0;

	// Use this for initialization
	void Start () {	

        DestroyObject (this.gameObject, LifeTime);
	}
	
    void Update()
    {
        if(m_AngularSpeed != 0)
            this.transform.Rotate(Vector3.forward, m_AngularSpeed * Time.deltaTime);
    }

	// Update is called once per frame
	void FixedUpdate () 
	{
		this.GetComponent<Rigidbody2D> ().velocity = m_Speed;
	}
	
	void OnTriggerEnter2D(Collider2D hit)
	{
		if(!Utils.ExistStringInArray(IgnoreTags, hit.gameObject.tag))
		{
            if(hit.gameObject.GetComponent<HealthController>() != null)
			{
				hit.gameObject.GetComponent<HealthController>().SetDamage(ImpactDamage, this.gameObject.tag);
				m_Speed = Vector3.one; // force destroy
			}

			if(m_Speed != Vector3.zero)
			{
				GameObject.Instantiate(ImpactPrefab, this.transform.position, Quaternion.identity);
				DestroyObject(this.gameObject, Time.deltaTime);
			}
		}
	}

	public void Shoot(Vector3 speed)
	{
		m_Speed = speed;
	}

    public void Rotate(float AngularSpeed)
    {
        m_AngularSpeed = AngularSpeed;   
    }
}
