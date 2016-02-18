using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

	protected int m_PreviousHealth = 0;
	protected float m_HitTime = 0;
    protected Transform m_Dust;

	// Use this for initialization
	void Start () {
        m_Dust = this.transform.FindChild("Dust");
        m_Dust.GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		float speed = Mathf.Abs (GetComponent<Rigidbody2D>().velocity.x);
		bool grounded = this.GetComponent<PlayerMotionController> ().IsGrounded;

		this.GetComponentInChildren<Animator>().SetFloat ("Speed", speed);
		this.GetComponentInChildren<Animator>().SetBool("Grounded", grounded);

        if (this.GetComponent<PlayerMotionController>().IsRunning && !m_Dust.GetComponent<SpriteRenderer>().enabled)
            m_Dust.GetComponent<SpriteRenderer>().enabled = true;        
        else
            m_Dust.GetComponent<SpriteRenderer>().enabled = false;

		if(m_PreviousHealth == 0)
			m_PreviousHealth = this.GetComponent<HealthController> ().CurrentHealth;

		int health = this.GetComponent<HealthController> ().CurrentHealth;

		if(m_PreviousHealth > health)
		{
			this.GetComponentInChildren<Animator>().SetTrigger("Hit");
			m_PreviousHealth = this.GetComponent<HealthController> ().CurrentHealth;
			m_HitTime = this.GetComponent<HealthController> ().HitTime;
		}

		if(m_HitTime > 0)
			this.SetHitEffect ();

		if(health == 0)
			this.GetComponentInChildren<Animator>().SetTrigger("Death");
	}

	void SetHitEffect()
	{
		m_HitTime -= Time.deltaTime;

		if(m_HitTime > 0)
			this.GetComponentInChildren<SpriteRenderer>().color = new Color(1,0,0,0.5f);
		else
			this.GetComponentInChildren<SpriteRenderer>().color = Color.white;
	}

	public void OnAttack()
	{
		this.GetComponentInChildren<Animator> ().SetTrigger ("Throw");
	}

	public void OnJump()
	{
		this.GetComponentInChildren<Animator> ().SetTrigger ("Jump");
	}

	public void OnGoal()
	{
		this.GetComponentInChildren<Animator>().SetTrigger("Goal");
	}

	public void OnFinish()
	{
		this.GetComponentInChildren<Animator>().Rebind();
	}

}
