using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMotionController : MonoBehaviour
{

		public float Acceleration;
		public float Deceleration;
		public float WalkinSpeed;
		public float RunningSpeed;
		public float AirResistence;
		public float JumpForce;
		public float JumpDecay;
		public float ChangeSpeedInterval;
		public float BounceForce;
		public float BounceDecay;
		public int BounceDamage;
        public bool IsRunning;
		public bool IsGrounded;
		public bool UseStandardMotion;
		
		protected float m_MotionDirection;
		protected float m_MotionLeftTime;
		protected float m_MotionRightTime;

		protected float m_JumpForce;
		protected float m_JumpTime;
		protected float m_BounceForce;
		protected float m_MaxSpeed;
		
		protected Transform[] m_GroundChecks;
		

		protected bool m_FacingRight = true;

		// Use this for initialization
		void Start ()
		{    
			Transform checks = this.transform.FindChild ("GroundChecks");        
			m_GroundChecks = new Transform[checks.childCount];        
			for (int i=0; i < checks.childCount; i++)
				m_GroundChecks [i] = checks.GetChild (i);

			m_MaxSpeed = WalkinSpeed;
			m_MotionLeftTime = -1;
			m_MotionRightTime = -1;
			m_JumpTime = -1;
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (UseStandardMotion)
				StandardMotion ();

			if (m_MotionLeftTime >= 0)
				m_MotionLeftTime += Time.deltaTime;

			if (m_MotionRightTime >= 0)
				m_MotionRightTime += Time.deltaTime;

			if (m_JumpTime >= 0 && m_JumpTime < ChangeSpeedInterval)
			{
				m_JumpTime += Time.deltaTime;
				m_JumpForce += JumpForce * Time.deltaTime;		
			}

            IsRunning = m_MaxSpeed == RunningSpeed ? true : false;
		}

        public void SetMotionDirection()
        {

        }

		public void MoveLeft ()
		{
			if (m_FacingRight) 
					Flip ();			

			if(m_MotionLeftTime > 0 && m_MotionLeftTime < ChangeSpeedInterval)
				m_MaxSpeed = RunningSpeed;

			m_MotionDirection = -1;
		}

		public void MoveRight ()
		{
			if (!m_FacingRight) 
					Flip ();

			if(m_MotionRightTime > 0 && m_MotionRightTime < ChangeSpeedInterval)
				m_MaxSpeed = RunningSpeed;

			m_MotionDirection = 1;
		}

		public void Stop ()
		{
			if(m_MotionDirection == -1)
				m_MotionLeftTime = 0;
			
			if (m_MotionDirection == 1)
				m_MotionRightTime = 0;

			m_MaxSpeed = WalkinSpeed;
			m_MotionDirection = 0;			
		}
	
		public void BeginJump ()
		{
			if (IsGrounded) 
			{
				m_JumpTime = 0;				
				m_JumpForce = JumpForce;
			}
		}

		public void EndJump ()
		{
			m_JumpTime = -1;
		}

		void StandardMotion ()
		{
			if (Input.GetAxis ("Horizontal") < 0)
				MoveLeft ();
			else if (Input.GetAxis ("Horizontal") > 0)
				MoveRight ();
			else
				this.Stop ();
		
			if (Input.GetButtonDown ("Jump"))
				BeginJump ();
			
			if(Input.GetButtonUp("Jump"))
				EndJump ();
		}

		bool Grounded ()
		{
			for (int i=0; i < m_GroundChecks.Length; i++) 
			{
				Vector3 dir = (m_GroundChecks [i].position - this.transform.position).normalized;
				float distance = Vector3.Distance (this.transform.position, m_GroundChecks [i].position);
				RaycastHit2D[] hits = Physics2D.RaycastAll (this.transform.position, dir, distance);
				for (int  j=0; j < hits.Length; j++) {
                    if (hits[j].transform.gameObject != this.gameObject && hits[j].transform.tag != "Enemy")
					return true;
				}
			}
			return false;
		}

		void FixedUpdate ()
		{   
			IsGrounded = Grounded ();

			float xSpeed = this.GetComponent<Rigidbody2D> ().velocity.x;
			//float ySpeed = this.GetComponent<Rigidbody2D> ().velocity.y;

			float maxSpeed = IsGrounded ?  m_MaxSpeed : m_MaxSpeed * AirResistence;

			if (m_MotionDirection != 0 && Mathf.Abs (xSpeed) < maxSpeed)
				this.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * m_MotionDirection * Acceleration, ForceMode2D.Force);
			else
				this.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -xSpeed * Deceleration, ForceMode2D.Force);

			if (m_JumpForce >= 1) {
				m_JumpForce = Mathf.Lerp (m_JumpForce, 0, JumpDecay);
				this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * m_JumpForce, ForceMode2D.Force);
			}

			if (m_BounceForce >= 1) {
				m_BounceForce = Mathf.Lerp (m_BounceForce, 0, BounceDecay);
				this.transform.GetComponent<Rigidbody2D> ().AddForce (Vector3.up * m_BounceForce, ForceMode2D.Force);	
			}
		}

		void OnCollisionEnter2D (Collision2D hit)
		{
			if (hit.collider.tag == "Bounce") 
			{						
				if (hit.gameObject.GetComponent<HealthController> () != null)
					hit.gameObject.GetComponent<HealthController> ().SetDamage (BounceDamage, this.tag);
					
				this.transform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (this.transform.GetComponent<Rigidbody2D> ().velocity.x, 0);
				m_BounceForce = BounceForce;
			}
		}

		public void Flip ()
		{
			m_FacingRight = !m_FacingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

		public void DisableMotion (bool kinematic)
		{
			this.GetComponent<Rigidbody2D> ().isKinematic = kinematic;
			this.enabled = false;
		}
	
		public void EnableMotion ()
		{
			this.GetComponent<Rigidbody2D> ().isKinematic = false;
			this.enabled = true;
		}
}