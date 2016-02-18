using UnityEngine;
using System.Collections;

public class SnailShellController : MonoBehaviour {

    public float Acceleration;
	public float Speed;    
	public string[] UnbounceTags; 

	protected Vector3 m_StartPosition;
	protected GameObject m_Player;
	protected bool m_AllowBouncing;
	protected float m_Direction = 1;
	protected ESnailShellState m_State;

	// Use this for initialization
	void Start () {
		m_Player = GameObject.FindWithTag ("Player");
		Utils.IgnoreCollisions (m_Player, this.gameObject);

		this.GetComponent<ContactDamageController>().EnableDamage = false;
		m_State = ESnailShellState.Idle;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch(m_State)
		{
		case ESnailShellState.Motion:
            if (Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.x) < Speed)
                this.GetComponent<Rigidbody2D>().AddForce(Vector3.right * m_Direction * Acceleration, ForceMode2D.Force);

			this.GetComponent<ContactDamageController>().EnableDamage = true;
			break;
		case ESnailShellState.Idle:
			this.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			this.GetComponent<ContactDamageController>().EnableDamage = false;
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D hit)
	{
		if(m_State == ESnailShellState.Idle && hit.gameObject.tag == "Player")
		{
			m_State = ESnailShellState.Motion;
			m_Direction = this.transform.position.x < hit.transform.position.x ? -1 : 1;
		}

        if (m_State == ESnailShellState.Motion)
        {
            if (hit.gameObject.tag == "Box")
                hit.gameObject.GetComponent<BoxBrickController>().Explode();

            if (hit.gameObject.tag == "BoxItem")
                hit.gameObject.GetComponent<BoxItemController>().Open();
        }

        if (!Utils.ExistStringInArray(UnbounceTags, hit.gameObject.tag) && !hit.isTrigger)
             m_Direction = m_Direction * -1;
 	}
}

