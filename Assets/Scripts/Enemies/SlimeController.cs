
using UnityEngine;
using System.Collections;

public class SlimeController : MonoBehaviour {

	public float Acceleration;
    public float Speed;
	public float DetectionRadio;

	protected GameObject m_Player;
    protected bool m_PlayerDetected = false;
	 
	// Use this for initialization
	void Start () {
		m_Player = GameObject.FindWithTag ("Player");
        
        float dir = this.transform.position.x > m_Player.transform.position.x ? 1 : -1;
        this.transform.localScale = new Vector3(dir, 1, 1);

		Utils.IgnoreCollisions (m_Player, this.gameObject);
    }

	void FixedUpdate()
	{
        if (m_PlayerDetected && Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.x) < Speed)
            this.GetComponent<Rigidbody2D>().AddForce(Vector3.right * this.transform.localScale.x * -Acceleration);       
	}

	void Update()
	{
        if(!m_PlayerDetected)
            m_PlayerDetected = Vector3.Distance(m_Player.transform.position, this.transform.position) < DetectionRadio;
	}
}

